using DocumentService.DTOs;
using DocumentServices.Application.DTOs;
using DocumentServices.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DocumentServices.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentRouterController : ControllerBase
    {
        private readonly IDocumentRoutingService _routingService;

        public DocumentRouterController(IDocumentRoutingService routingService)
        {
            _routingService = routingService;
        }

        [HttpGet("fetch")]
        [ProducesResponseType(typeof(RemoteListResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> FetchByType([FromQuery] int id)
        {
            var userToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrWhiteSpace(userToken))
            {
                return Unauthorized(new ErrorResponse
                {
                    Error = "unauthorized_client",
                    Details = "Authorization token is missing."
                });
            }

            try
            {
                var result = await _routingService.GetListByIdAsync(id, userToken);
                return Ok(result);
            }
            catch (NotSupportedException ex)
            {
                return BadRequest(new ErrorResponse
                {
                    Error = "not_supported",
                    Details = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse
                {
                    Error = "internal_error",
                    Details = ex.Message
                });
            }
        }
    }

}
