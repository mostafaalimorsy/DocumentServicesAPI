using DocumentService.DTOs;
using DocumentService.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DocumentService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
       // private readonly IAuthService _authService;
        private readonly IIAMAuthService _iamauthService;

        public AuthController(IIAMAuthService iamauthService)
        {
            //_authService = authService;
            _iamauthService = iamauthService;

        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new ErrorResponse
                {
                    Error = "MissingParameters",
                    Details = "Username and password are required."
                });
            }

            try
            {
                var token = await _iamauthService.AuthenticateAsync(request.Username, request.Password);
                return Ok(token);
            }
            catch (Exception)
            {
                return Unauthorized(new ErrorResponse { Error = "UNAUthroized", Details = "one or both  username/password is wrong" });
            }
        }
    }

}
