using DocumentService.Interface.FileService;
using DocumentServices.Application.DTOs;
using DocumentServices.Application.Interface;
using System.Net.Http.Headers;
using System.Text.Json;

namespace DocumentServices.Services
{
  
    public class DocumentFileGroupService : IDocumentFileGroupService
    {
        private readonly HttpClient _httpClient;

        public DocumentFileGroupService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<DocumentFileGroupDto>> GetFilesByDocumentIdAsync(int documentId, string userToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

            var requestUri = $"https://upgrade-portal.evergulf.com/File/ListByDocumentId?documentId={documentId}&delegationId=";
           
            var response = await _httpClient.GetAsync(requestUri);

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException($"API Error: {response.ReasonPhrase}");
            }

            var content = await response.Content.ReadAsStringAsync();

          

            if(content.Equals(""))
            {
                return new List<DocumentFileGroupDto>();
            }
            else
            {
                var result = JsonSerializer.Deserialize<List<DocumentFileGroupDto>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return result;
            }
          
        }
    }

}
