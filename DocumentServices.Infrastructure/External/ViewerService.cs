using DocumentService.DTOs;
using DocumentServices.Application.DTOs.ExternalDownloadsFile;
using DocumentServices.Application.Interface.ExternalDocumnetDowmloaded;
using DocumentServices.Domain.Helper;
using DocumentServices.Domain.Models;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace DocumentServices.Infrastructure.External
{
    public class ViewerService : IViewerService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ViewerService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task<string> CheckOutViewer(ChecksRequestDTO requestBody, string userToken)
        {
            var url = $"https://upgrade-viewer.evergulf.com/UVIEWER/api/document/{requestBody.ExternalFileId}/version/{requestBody.VersionNumber}/checkout";

            //using var request = new HttpRequestMessage(HttpMethod.Get, url);
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://upgrade-viewer.evergulf.com/UVIEWER/api/document/{requestBody.ExternalFileId}/version/{requestBody.VersionNumber}/checkout");
            //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
            request.Headers.Add("accept", "application/json, text/plain, */*");
            request.Headers.Add("accept-language", "en-US,en;q=0.8");
            request.Headers.Add("authorization", $"{userToken}");
            request.Headers.Add("priority", "u=1, i");
            request.Headers.Add("referer", "https://upgrade-viewer.evergulf.com/UVIEWER/");
            request.Headers.Add("sec-ch-ua", "\"Not)A;Brand\";v=\"8\", \"Chromium\";v=\"138\", \"Brave\";v=\"138\"");
            request.Headers.Add("sec-ch-ua-mobile", "?0");
            request.Headers.Add("sec-ch-ua-platform", "\"Windows\"");
            request.Headers.Add("sec-fetch-dest", "empty");
            request.Headers.Add("sec-fetch-mode", "cors");
            request.Headers.Add("sec-fetch-site", "same-origin");
            request.Headers.Add("sec-gpc", "1");
            request.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36");

            var response = await _httpClient.SendAsync(request); // Fixed method call from GetAsync to SendAsync for HttpRequestMessage.
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = new ErrorResponse
                {
                    Error = "checkout failed",
                    Details = $"{response.StatusCode}"

                };

                // Serialize the ErrorResponse object to a JSON string
                throw new ErrorException(errorResponse);
            }

            return responseContent;
        }

        public async Task<string> CheckInViewer(ChecksRequestDTO requestBody, string userToken)
        {
            //var url = $"https://upgrade-viewer.evergulf.com/UVIEWER/api/document/{requestBody.ExternalFileId}/version/{requestBody.VersionNumber}/checkin";
            var payload = new { message = "g" };
            var json = JsonSerializer.Serialize(payload);

            //using var request = new HttpRequestMessage(HttpMethod.Post, url)
            //{
            //    Content = new StringContent(json, Encoding.UTF8, "application/json")
            //};

            //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
            var request = new HttpRequestMessage(HttpMethod.Post, $"https://upgrade-viewer.evergulf.com/UVIEWER/api/document/{requestBody.ExternalFileId}/version/{requestBody.VersionNumber}/checkin");
            request.Headers.Add("accept", "application/json, text/plain, */*");
            //request.Headers.Add("content-type", "application/json");

            request.Headers.Add("accept-language", "en-US,en;q=0.8");
            request.Headers.Add("authorization", $"{userToken}");

            request.Headers.Add("origin", "https://upgrade-viewer.evergulf.com");
            request.Headers.Add("priority", "u=1, i");
            request.Headers.Add("referer", "https://upgrade-viewer.evergulf.com/UVIEWER/");
            request.Headers.Add("sec-ch-ua", "\"Not)A;Brand\";v=\"8\", \"Chromium\";v=\"138\", \"Brave\";v=\"138\"");
            request.Headers.Add("sec-ch-ua-mobile", "?0");
            request.Headers.Add("sec-ch-ua-platform", "\"Windows\"");
            request.Headers.Add("sec-fetch-dest", "empty");
            request.Headers.Add("sec-fetch-mode", "cors");
            request.Headers.Add("sec-fetch-site", "same-origin");
            request.Headers.Add("sec-gpc", "1");
            request.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36");
            var content = new StringContent("{\"message\":\"h\"}", null, "application/json");
            request.Content = content;
            var response = await _httpClient.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = new ErrorResponse
                {
                    Error = "checkIn failed",
                    Details = $"{response.StatusCode}"
                };

                // Serialize the ErrorResponse object to a JSON string
                throw new ErrorException(errorResponse);
            }

            return responseContent;
        }
        public async Task<string> SaveSignatureAsync(SignatureRequestDto requestBody,string userToken, string externalFileId, string versionNumber)
        {

            var url = $"https://upgrade-viewer.evergulf.com/UVIEWER/api/document/{externalFileId}/version/{versionNumber}/signature/save";
            var json = JsonSerializer.Serialize(requestBody);
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("accept", "application/json, text/plain, */*");
            request.Headers.Add("accept-language", "en-US,en;q=0.8");
            request.Headers.Add("authorization", $"{userToken}");
            //request.Headers.Add("content-type", "application/json");
            request.Headers.Add("origin", "https://upgrade-viewer.evergulf.com");
            request.Headers.Add("priority", "u=1, i");
            request.Headers.Add("referer", "https://upgrade-viewer.evergulf.com/UVIEWER/");
            request.Headers.Add("sec-ch-ua", "\"Not)A;Brand\";v=\"8\", \"Chromium\";v=\"138\", \"Brave\";v=\"138\"");
            request.Headers.Add("sec-ch-ua-mobile", "?0");
            request.Headers.Add("sec-ch-ua-platform", "\"Windows\"");
            request.Headers.Add("sec-fetch-dest", "empty");
            request.Headers.Add("sec-fetch-mode", "cors");
            request.Headers.Add("sec-fetch-site", "same-origin");
            request.Headers.Add("sec-gpc", "1");
 
            var content = new StringContent(json, Encoding.UTF8, "application/json");
          

            request.Content = content;
            var response = await _httpClient.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                if (!response.IsSuccessStatusCode)
                {
                    var errorResponse = new ErrorResponse
                    {
                        Error = "save signture faild",
                        Details = $"{response.StatusCode}"
                    };

                    // Serialize the ErrorResponse object to a JSON string
                    throw new ErrorException(errorResponse);
                }
            }

            return responseContent;
        }

        public async Task<string> SaveAnnotationAsync(AnnotationRequestDto requestBody ,string userToken, string externalFileId, string versionNumber)
        {
            var url = $"https://upgrade-viewer.evergulf.com/UVIEWER/api/document/{externalFileId}/version/{versionNumber}/annotation";
            var json = JsonSerializer.Serialize(requestBody);
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("accept", "application/json, text/plain, */*");
            request.Headers.Add("accept-language", "en-US,en;q=0.8");
            request.Headers.Add("authorization", $"{userToken}");
            //request.Headers.Add("content-type", "application/json");
            request.Headers.Add("origin", "https://upgrade-viewer.evergulf.com");
            request.Headers.Add("priority", "u=1, i");
            request.Headers.Add("referer", "https://upgrade-viewer.evergulf.com/UVIEWER/");
            request.Headers.Add("sec-ch-ua", "\"Not)A;Brand\";v=\"8\", \"Chromium\";v=\"138\", \"Brave\";v=\"138\"");
            request.Headers.Add("sec-ch-ua-mobile", "?0");
            request.Headers.Add("sec-ch-ua-platform", "\"Windows\"");
            request.Headers.Add("sec-fetch-dest", "empty");
            request.Headers.Add("sec-fetch-mode", "cors");
            request.Headers.Add("sec-fetch-site", "same-origin");
            request.Headers.Add("sec-gpc", "1");
            request.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/138.0.0.0 Safari/537.36");
            var content = new StringContent(json, null, "application/json");

            //using var request = new HttpRequestMessage(HttpMethod.Post, url)
            //{
            //    Content = content
            //};
            //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

            var response = await _httpClient.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                if (!response.IsSuccessStatusCode)
                {
                    var errorResponse = new ErrorResponse
                    {
                        Error = "save annotation falid",
                        Details = $"{response.StatusCode}"
                    };

                    // Serialize the ErrorResponse object to a JSON string
                    throw new ErrorException(errorResponse);
                }
            }

            return responseContent;
        }

        public async Task<string> ProcessViewerUpdate(
            string userToken,
            string externalFileId,
            string versionNumber,
            Func<string, Task<string>> actionToPerform)
        {

            string? version = null;
            // 1. Call CheckOutViewer
           
                try
                {

                    // 1. Call CheckOutViewer
                    var checkOutResponse = await CheckOutViewer(new ChecksRequestDTO
                    {
                        ExternalFileId = externalFileId,
                        VersionNumber = versionNumber // placeholder
                    }, userToken);

                    var jsonObject = JsonDocument.Parse(checkOutResponse);
                    version = jsonObject.RootElement.GetProperty("version").GetString();

                    if (string.IsNullOrWhiteSpace(version))
                    {
                        var errorResponse = new ErrorResponse
                        {
                            Error = "checkout failed",
                            Details = "Failed to retrieve version from CheckOutViewer response"
                        };

                        // Serialize the ErrorResponse object to a JSON string
                        throw new ErrorException(errorResponse);
                    }

                }
                catch (ErrorException ex)
                {
                    throw new ErrorException(ex.ErrorResponse);


                }
            
       
            // 2. Perform the requested action (signature or annotation)

            try
            {
                 var actionResult = await actionToPerform(version);
            }
            catch (ErrorException ex) {
                throw new ErrorException(ex.ErrorResponse);
            }
            // 3. Call CheckInViewer
            try
            {
                await CheckInViewer(new ChecksRequestDTO
                {
                    ExternalFileId = externalFileId,
                    VersionNumber = version
                }, userToken);

          
            }
            catch (ErrorException ex)
            {
                throw new ErrorException(ex.ErrorResponse);
            }

            // 4. Return the result of the action
            var successResponse = new ExternalFileUpdate
            {
                IsCheckedOut = true,
                IsCheckedIn = true,
                IsFileUpdateed = true,
                Message = "Viewer update was completed successfully.",
            };
            return JsonSerializer.Serialize(successResponse);
        }


    }

}
