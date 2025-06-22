using DocumentServices.Application.DTOs.ExternalDownloadsFile;
using DocumentServices.Application.Interface.ExternalDocumnetDowmloaded;
using DocumentServices.Domain.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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

        public async Task<string> SaveSignatureAsync(SignatureRequestDto request,string userToken)
        {
            var url = "https://upgrade-viewer.evergulf.com/UVIEWER/api/document/11/version/T_7.0/signature/save";
            return await PostToViewerApi(url, request , userToken);
        }

        public async Task<string> SaveAnnotationAsync(AnnotationRequestDto request ,string userToken)
        {
            var url = "https://upgrade-viewer.evergulf.com/UVIEWER/api/document/11/version/T_7.0/annotation";
            return await PostToViewerApi(url, request , userToken);
        }

        private async Task<string> PostToViewerApi(string url, object body ,string  userToken)
        {
           
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

            var json = JsonSerializer.Serialize(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Failed to call Viewer API: {response.StatusCode} - {responseContent}");

            return responseContent;
        }
    }

}
