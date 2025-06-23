using DocumentServices.Application.DTOs.ExternalDonloadsFile;
using DocumentServices.Application.DTOs.ExternalDownloadsFile;
using DocumentServices.Application.Interface.ExternalDocumnetDowmloaded;
using DocumentServices.Domain.Models;
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
            //// get the file data from the external API
            //var url = "https://upgrade-portal.evergulf.com/File/ListByDocumentId?documentId=14";
            var url = "https://upgrade-viewer.evergulf.com/UVIEWER/api/document/11/version/12.0/details?&caseDocumentId=14&caseTaskId=45&delegationId=null&isDraft=false";

            var dataRequest = new HttpRequestMessage(HttpMethod.Get, url);
            dataRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

            var dataResponse = await _httpClient.SendAsync(dataRequest);
            var content = await dataResponse.Content.ReadAsStringAsync();

            if (!dataResponse.IsSuccessStatusCode)
                throw new Exception($"External API Error: {dataResponse.StatusCode}. {content}");

            ExternalFileAsPdfResponse fullResponse = JsonConvert.DeserializeObject<ExternalFileAsPdfResponse>(content);





            // get the base65 PDF file from the external API

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, "https://upgrade-portal.evergulf.com/File/Download?id=11&fromForm=true");

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
                FileName = fullResponse.FileName,
                Annotations = fullResponse.Annotations,
                Signatures = fullResponse.Signatures,
                Permissions = fullResponse.Permissions,
                Base64Pdf = Convert.ToBase64String(bytes) // Convert byte[] to Base64 string
            };
        }

    }

}
