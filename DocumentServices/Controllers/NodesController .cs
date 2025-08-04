using DocumentService.DTOs;
using DocumentServices.Application.DTOs;
using DocumentServices.Application.DTOs.ExternalDonloadsFile;
using DocumentServices.Application.Interface;
using DocumentServices.Services;
using Microsoft.AspNetCore.Mvc;

namespace DocumentServices.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NodesController : ControllerBase
    {
        private readonly INodeService _nodeService;


        public NodesController(INodeService nodeService)
        {
            _nodeService = nodeService;
        }

        [HttpGet("list")]
        [ProducesResponseType(typeof(NodeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetNodeList()
        {
            var userToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrWhiteSpace(userToken))
                return Unauthorized(new ErrorResponse
                {
                    Error = "unauthorized_client",
                    Details = "Authorization header is missing or invalid."
                });

            try
            {
                var nodes = await _nodeService.GetNodesAsync(userToken);
                return Ok(nodes);
            }
            catch (Exception ex)
            {
                return StatusCode(400, new ErrorResponse
                {
                    Error = "server_error",
                    Details = ex.Message
                });
            }
        }
    }

}
