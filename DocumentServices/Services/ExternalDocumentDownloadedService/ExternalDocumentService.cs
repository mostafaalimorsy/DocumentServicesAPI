using DocumentServices.Application.DTOs.ExternalDonloadsFile;
using DocumentServices.Application.Interface.ExternalDocumnetDowmloaded;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace DocumentServices.Services.ExternalDocumentDownloaded
{
    public class ExternalDocumentService : IExternalDocumentService
    {
        private readonly HttpClient _httpClient;

        public ExternalDocumentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ExternalFileAsPdfResponse> DownloadFileFromExternalApiAsync(ExternalFileDownloadRequest request, string userToken)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                "https://upgrade-viewer.evergulf.com/UVIEWER/api/document/11/version/T_7.0/view");

            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

            var response = await _httpClient.SendAsync(httpRequest);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Failed to fetch file from external service. Status: {response.StatusCode}. Response: {error}");
            }

            var bytes = await response.Content.ReadAsByteArrayAsync(); // since it returns a PDF
            return new ExternalFileAsPdfResponse
            {
                Base64Pdf = Convert.ToBase64String(bytes) // Convert byte[] to Base64 string
            };
        }

    }

}
