using DocumentService.DTOs;
using DocumentService.Interface;
using DocumentServices.Domain.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocumentServices.Controllers.Auth
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IIAMAuthService _iamauthService;

        public AuthController(IIAMAuthService iamauthService)
        {
            _iamauthService = iamauthService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
                return BadRequest(new ErrorResponse { Error = "MissingUsernameOrPassword" , Details = "one or both user name/ password is wrong"});

            try
            {
                var token = await _iamauthService.AuthenticateAsync(request.Username, request.Password);
                return Ok(token);
            }
            catch (ErrorException ex)
            {
                return Unauthorized(new ErrorResponse { Error = ex.ErrorResponse.Error, Details = ex.ErrorResponse.Details });
            }
        }
    }

}