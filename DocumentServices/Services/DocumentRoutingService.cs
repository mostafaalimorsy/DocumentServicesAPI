using Aspose.Words;
using DocumentServices.Application.DTOs;
using DocumentServices.Application.Interface;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text.Json;

namespace DocumentServices.Services
{
    public class DocumentRoutingService : IDocumentRoutingService
    {
        private readonly HttpClient _httpClient;

        public DocumentRoutingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<RemoteListResponseDto> GetListByIdAsync(int id, string userToken)
        {
            string? endpoint = id switch
            {
                1 => "Document/ListDraft",
                2 => "Task/ListInbox",
                3 => "Task/ListCompleted",
                6 => "Document/ListClosed",
                _ => null
            };

            if (endpoint == null)
                throw new BadHttpRequestException($"NodeId {id} is not handled yet.");

            
           


            var formData = new Dictionary<string, string>
                {
                    { "draw", "1" },
                    { "start", "0" },
                    { "length", "20" },
                    { "NodeId", $"{id}" },
                    { "__RequestVerificationToken", userToken }
                };
            var request = new HttpRequestMessage(HttpMethod.Post, $"https://upgrade-portal.evergulf.com/{endpoint}")
            {
                Content = new FormUrlEncodedContent(formData)
            };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new ApplicationException($"Failed to fetch documents. Status: {response.StatusCode}, Content: {errorContent}");
            }

            var responseString = await response.Content.ReadAsStringAsync();

            // Response contains a wrapper: { draw, recordsTotal, ..., data: [...] }
            var wrapper = JsonSerializer.Deserialize<RemoteListResponseDto>(responseString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if(wrapper.Data.Equals(""))
            {
                return new RemoteListResponseDto
                {
                    Draw = wrapper.Draw,
                    RecordsTotal = wrapper.RecordsTotal,
                    RecordsFiltered = wrapper.RecordsFiltered,
                    Data = new List<RemoteDocumentItem>(),

                };
            }
            else
            {
                return wrapper;

            }

        }
    }

}
