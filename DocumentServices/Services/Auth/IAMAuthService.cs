using DocumentService.DTOs;
using DocumentService.Interface;
using DocumentService.Models;
using DocumentServices.Domain.Helper;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace DocumentService.Services
{
    public class IAMAuthService : IIAMAuthService
    {
        private readonly HttpClient _client;
        private readonly IAMSettings _iamSettings;
        private readonly AuthConfiguration _authSettings;

        public IAMAuthService(IHttpClientFactory httpClientFactory, Microsoft.Extensions.Options.IOptions<IAMSettings> iamOptions, IOptions<AuthConfiguration> authOptions)
        {
           
            _client = httpClientFactory.CreateClient();
            _iamSettings = iamOptions.Value;
            _authSettings = authOptions.Value;
        }

        public async Task<LoginResponse> AuthenticateAsync(string username, string password)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_iamSettings.Url}/connect/token");

            var loginInfo = new Dictionary<string, string>
        {
            { "client_id", _iamSettings.ClientId },
            { "client_secret", _iamSettings.ClientSecret },
            { "grant_type", _authSettings.GrantType },
            { "username", username },
            { "password", password }
        };

            request.Content = new FormUrlEncodedContent(loginInfo);
            var response = await _client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(error);
                var root = doc.RootElement;
                ErrorResponse errorResponse = new ErrorResponse
                {
                    Error = root.GetProperty("error").GetString(),
                    Details = root.GetProperty("error_description").GetString()
                };
                throw new ErrorException(errorResponse);
            }

            var json = await response.Content.ReadAsStringAsync();
            var token = JsonSerializer.Deserialize<LoginResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return token;
        }
    }

}
