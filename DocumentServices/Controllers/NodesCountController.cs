using DocumentService.DTOs;
using DocumentServices.Application.DTOs;
using DocumentServices.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DocumentServices.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DraftController : ControllerBase
    {
        private readonly IDraftService _draftService;

        public DraftController(IDraftService draftService)
        {
            _draftService = draftService;
        }

        [HttpGet("count")]
        [ProducesResponseType(typeof(DraftCountDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetDraftCounts([FromQuery] int nodeId)
        {
            var userToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrWhiteSpace(userToken))
            {
                return Unauthorized(new ErrorResponse
                {
                    Error = "unauthorized_client",
                    Details = "Authorization header is missing or invalid."
                });
            }

            try
            {
                var draftCount = await _draftService.GetDraftCountsAsync(nodeId, userToken);
                return Ok(draftCount);
            }
            catch (Exception ex)
            {
                return StatusCode(400, new ErrorResponse
                {
                    Error = "draft_count_error",
                    Details = ex.Message
                });
            }
        }
    }

}
