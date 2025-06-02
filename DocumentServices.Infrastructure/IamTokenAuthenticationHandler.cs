using DocumentService.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;


//using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace DocumentService.Infrastructure.Authentication
{
    public class IamTokenAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public IamTokenAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        { }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Authorization header missing.");
            }

            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token))
            {
                return AuthenticateResult.Fail("Invalid token.");
            }

            // TODO: Validate IAM token here (call IAM endpoint, etc.)
            var isValidToken = await ValidateTokenAsync(token);
            if (!isValidToken)
            {
                return AuthenticateResult.Fail("Unauthorized - Invalid IAM token.");
            }

            var claims = new[] { new Claim(ClaimTypes.Name, "IAMUser") };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }

        private async Task<bool> ValidateTokenAsync(string token)
        {
            // Simulated validation — replace with actual IAM token validation
            return await Task.FromResult(!string.IsNullOrWhiteSpace(token));
        }

        protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Response.StatusCode = StatusCodes.Status401Unauthorized;
            Response.ContentType = "application/json";

            var error = new ErrorResponse
            {
                Details = "Unauthorized - Token is missing or invalid.",
                Error =  "UnauthorizedAccess" 
            };

            var json = JsonSerializer.Serialize(error);
            await Response.WriteAsync(json);
        }
    }
}
