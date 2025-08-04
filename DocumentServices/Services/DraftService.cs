using DocumentServices.Application.DTOs;
using DocumentServices.Application.Interface;
using System.Net.Http.Headers;
using System.Text.Json;

namespace DocumentServices.Services
{
    public class DraftService : IDraftService
    {
        private readonly HttpClient _httpClient;

        public DraftService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DraftCountDto> GetDraftCountsAsync(int nodeId, string userToken)
        {
            var url = $"https://upgrade-portal.evergulf.com/Task/GetInboxCounts?nodeId={nodeId}";

        
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            if (!string.IsNullOrWhiteSpace(userToken))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
            }

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Draft count fetch failed: {response.StatusCode} - {error}");
            }

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<DraftCountDto>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result!;
        }
    }

}
