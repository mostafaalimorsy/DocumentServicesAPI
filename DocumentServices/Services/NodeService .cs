using DocumentServices.Application.DTOs;
using DocumentServices.Application.Interface;
using System.Net.Http.Headers;
using System.Text.Json;

namespace DocumentServices.Services
{
    public class NodeService : INodeService
    {
        private readonly HttpClient _httpClient;

        public NodeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<NodeDto>> GetNodesAsync(string userToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://upgrade-portal.evergulf.com/Node/ListNodes");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Failed to fetch nodes: {(int)response.StatusCode} - {errorContent}");
            }

            var content = await response.Content.ReadAsStringAsync();
            var nodes = JsonSerializer.Deserialize<List<NodeDto>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return nodes!;
        }
    }

}
