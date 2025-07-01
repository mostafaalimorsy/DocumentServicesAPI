using Aspose.Imaging.Xmp.Types.Complex.Version;
using Aspose.Pdf.Operators;
using Aspose.Words;
using DocumentService.DTOs;
using DocumentServices.Application.DTOs.ExternalDownloadsFile;
using DocumentServices.Application.Interface.ExternalDocumnetDowmloaded;
using DocumentServices.Domain.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
            //request.Headers.Add("authorization", "Bearer eyJhbGciOiJSUzI1NiIsImtpZCI6IjgyMUI4QzVCNzEyRTU5NjhERDE4OTc2NjFDMkM4RENBIiwidHlwIjoiYXQrand0In0.eyJuYmYiOjE3NTEzNzA2NzUsImV4cCI6MTc1MTQwNjY3NSwiaXNzIjoiaHR0cHM6Ly91cGdyYWRlLWlhbS5ldmVyZ3VsZi5jb20iLCJhdWQiOlsiSWRlbnRpdHlTZXJ2ZXJBcGkiLCJvZmZsaW5lX2FjY2VzcyJdLCJjbGllbnRfaWQiOiIwYjEwM2U2ZS04NWM0LTQ1YjEtOTNkNS02OTQwYzY3OGVmYzkiLCJzdWIiOiIzIiwiYXV0aF90aW1lIjoxNzUxMzcwNjcyLCJpZHAiOiJsb2NhbCIsIkRpc3BsYXlOYW1lIjoi2KfZhNmF2K_ZitixINin2YTYudin2YUiLCJMb2dpblByb3ZpZGVyVHlwZSI6MSwiRW1haWwiOiJNYW5hZ2VtZW50UmVzcG9uc2libGVATWFuYWdlbWVudFJlc3BvbnNpYmxlLmNvbSIsIklkIjozLCJGaXJzdE5hbWUiOiLYp9mE2YXYr9mK2LEiLCJMYXN0TmFtZSI6Itin2YTYudin2YUiLCJVc2VybmFtZSI6Ik1hbmFnZW1lbnRSZXNwb25zaWJsZSIsIk1pZGRsZU5hbWUiOiIiLCJTdHJ1Y3R1cmVJZCI6IjUiLCJNYW5hZ2VySWQiOiIiLCJTdHJ1Y3R1cmVJZHMiOiI1IiwiR3JvdXBJZHMiOiIyIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiRGVwYXJ0bWVudCBNYW5hZ2VyIiwiQXBwbGljYXRpb25Sb2xlSWQiOiI2IiwiVXNlclR5cGVJZCI6IjEiLCJDbGllbnRzIjpbIntcIlJvbGVJZFwiOjYsXCJSb2xlXCI6XCJEZXBhcnRtZW50IE1hbmFnZXJcIixcIkNsaWVudElkXCI6XCIwYjEwM2U2ZS04NWM0LTQ1YjEtOTNkNS02OTQwYzY3OGVmYzlcIn0iLCJ7XCJSb2xlSWRcIjoyLFwiUm9sZVwiOlwiRnVsbCBDb250cm9sXCIsXCJDbGllbnRJZFwiOlwiMWNiYWFlYWQtYzA3Yi00M2Q5LTk1NDEtNjNkNTE0ZGYzZWE3XCJ9Iiwie1wiUm9sZUlkXCI6NCxcIlJvbGVcIjpcIlJlYWRcIixcIkNsaWVudElkXCI6XCI3NjE2ODkwOS01Njc5LTQ2NzktYTBkNi0yYTBkZjY1ZTNjOGJcIn0iLCJ7XCJSb2xlSWRcIjozLFwiUm9sZVwiOlwiQ29udHJpYnV0ZVwiLFwiQ2xpZW50SWRcIjpcImFjYTZkMDFjLTdlOWEtNGUwZC1iYmY5LThlMzg0ZTE0MTQ4Y1wifSJdLCJqdGkiOiI5OTQwNzE4RUIwRUNDRTAwMEU3NEEzRUQ5MENERThCMiIsInNpZCI6IkY3ODY1Mzg0OTIxN0U2NTMxRTY2NkM5NEYyNTU4RERCIiwiaWF0IjoxNzUxMzcwNjc1LCJzY29wZSI6WyJvcGVuaWQiLCJJZGVudGl0eVNlcnZlckFwaSIsIm9mZmxpbmVfYWNjZXNzIl0sImFtciI6WyJwd2QiXX0.nqiwKSi61N1xAApEbdUtpFtewckerqmpYzQtSuPdO8YyD4mfv0vSoTyjqMLyI7OahgXHHkej_8sf1rqFRgVXsJ_0Vv1ltWE19Mj-MRKz8Uwi1iU6iRUw8n9oNVx_hqjyWwxIM8pNH5-3oAs2gqyraNCMOduiKR8vwmNFh0rwOHkbPMvYVmTzKshM_NCULU0d_yW7CUCO8aZXleavAHq6kDG7PQRsc9TL7lMxgy4BrUzFdWEpN4hq6XyzNJTuL5kBoruDu_IvYGXkaVbC0Ij4fKOf-fwKslaVCZm3hmkAWg9m7hz2CdLik_wVVi1Yx_X84YRwmsDzoNvWyBbOYYI8qw");
            request.Headers.Add("authorization", $"Bearer {userToken}");
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
                throw new Exception($"Checkout failed: {response.StatusCode}");

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
            //request.Headers.Add("authorization", "Bearer eyJhbGciOiJSUzI1NiIsImtpZCI6IjgyMUI4QzVCNzEyRTU5NjhERDE4OTc2NjFDMkM4RENBIiwidHlwIjoiYXQrand0In0.eyJuYmYiOjE3NTEzNzA2NzUsImV4cCI6MTc1MTQwNjY3NSwiaXNzIjoiaHR0cHM6Ly91cGdyYWRlLWlhbS5ldmVyZ3VsZi5jb20iLCJhdWQiOlsiSWRlbnRpdHlTZXJ2ZXJBcGkiLCJvZmZsaW5lX2FjY2VzcyJdLCJjbGllbnRfaWQiOiIwYjEwM2U2ZS04NWM0LTQ1YjEtOTNkNS02OTQwYzY3OGVmYzkiLCJzdWIiOiIzIiwiYXV0aF90aW1lIjoxNzUxMzcwNjcyLCJpZHAiOiJsb2NhbCIsIkRpc3BsYXlOYW1lIjoi2KfZhNmF2K_ZitixINin2YTYudin2YUiLCJMb2dpblByb3ZpZGVyVHlwZSI6MSwiRW1haWwiOiJNYW5hZ2VtZW50UmVzcG9uc2libGVATWFuYWdlbWVudFJlc3BvbnNpYmxlLmNvbSIsIklkIjozLCJGaXJzdE5hbWUiOiLYp9mE2YXYr9mK2LEiLCJMYXN0TmFtZSI6Itin2YTYudin2YUiLCJVc2VybmFtZSI6Ik1hbmFnZW1lbnRSZXNwb25zaWJsZSIsIk1pZGRsZU5hbWUiOiIiLCJTdHJ1Y3R1cmVJZCI6IjUiLCJNYW5hZ2VySWQiOiIiLCJTdHJ1Y3R1cmVJZHMiOiI1IiwiR3JvdXBJZHMiOiIyIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiRGVwYXJ0bWVudCBNYW5hZ2VyIiwiQXBwbGljYXRpb25Sb2xlSWQiOiI2IiwiVXNlclR5cGVJZCI6IjEiLCJDbGllbnRzIjpbIntcIlJvbGVJZFwiOjYsXCJSb2xlXCI6XCJEZXBhcnRtZW50IE1hbmFnZXJcIixcIkNsaWVudElkXCI6XCIwYjEwM2U2ZS04NWM0LTQ1YjEtOTNkNS02OTQwYzY3OGVmYzlcIn0iLCJ7XCJSb2xlSWRcIjoyLFwiUm9sZVwiOlwiRnVsbCBDb250cm9sXCIsXCJDbGllbnRJZFwiOlwiMWNiYWFlYWQtYzA3Yi00M2Q5LTk1NDEtNjNkNTE0ZGYzZWE3XCJ9Iiwie1wiUm9sZUlkXCI6NCxcIlJvbGVcIjpcIlJlYWRcIixcIkNsaWVudElkXCI6XCI3NjE2ODkwOS01Njc5LTQ2NzktYTBkNi0yYTBkZjY1ZTNjOGJcIn0iLCJ7XCJSb2xlSWRcIjozLFwiUm9sZVwiOlwiQ29udHJpYnV0ZVwiLFwiQ2xpZW50SWRcIjpcImFjYTZkMDFjLTdlOWEtNGUwZC1iYmY5LThlMzg0ZTE0MTQ4Y1wifSJdLCJqdGkiOiI5OTQwNzE4RUIwRUNDRTAwMEU3NEEzRUQ5MENERThCMiIsInNpZCI6IkY3ODY1Mzg0OTIxN0U2NTMxRTY2NkM5NEYyNTU4RERCIiwiaWF0IjoxNzUxMzcwNjc1LCJzY29wZSI6WyJvcGVuaWQiLCJJZGVudGl0eVNlcnZlckFwaSIsIm9mZmxpbmVfYWNjZXNzIl0sImFtciI6WyJwd2QiXX0.nqiwKSi61N1xAApEbdUtpFtewckerqmpYzQtSuPdO8YyD4mfv0vSoTyjqMLyI7OahgXHHkej_8sf1rqFRgVXsJ_0Vv1ltWE19Mj-MRKz8Uwi1iU6iRUw8n9oNVx_hqjyWwxIM8pNH5-3oAs2gqyraNCMOduiKR8vwmNFh0rwOHkbPMvYVmTzKshM_NCULU0d_yW7CUCO8aZXleavAHq6kDG7PQRsc9TL7lMxgy4BrUzFdWEpN4hq6XyzNJTuL5kBoruDu_IvYGXkaVbC0Ij4fKOf-fwKslaVCZm3hmkAWg9m7hz2CdLik_wVVi1Yx_X84YRwmsDzoNvWyBbOYYI8qw");
            request.Headers.Add("authorization", $"Bearer {userToken}");

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
                throw new Exception($"Checkin failed: {response.StatusCode} - {response.Content}");

            return responseContent;
        }
        public async Task<string> SaveSignatureAsync(SignatureRequestDto requestBody,string userToken, string externalFileId, string versionNumber)
        {
            var url = $"https://upgrade-viewer.evergulf.com/UVIEWER/api/document/{externalFileId}/version/{versionNumber}/signature/save";
            var json = JsonSerializer.Serialize(requestBody);
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("accept", "application/json, text/plain, */*");
            request.Headers.Add("accept-language", "en-US,en;q=0.8");
            request.Headers.Add("authorization", $"Bearer {userToken}");
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
            request.Headers.Add("Cookie", "lang=en");
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
                throw new Exception($"Save request failed: {response.StatusCode}");
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
            request.Headers.Add("authorization", $"Bearer {userToken}");
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
                throw new Exception($"Save request failed: {response.StatusCode}");
            }

            return responseContent;
        }

    

    }

}
