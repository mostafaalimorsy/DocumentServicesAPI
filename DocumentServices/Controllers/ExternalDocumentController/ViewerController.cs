using DocumentService.DTOs;
using DocumentServices.Application.DTOs.ExternalDownloadsFile;
using DocumentServices.Domain.Models;
using DocumentServices.Services;
using Microsoft.AspNetCore.Mvc;

namespace DocumentServices.Controllers.ExternalDocumentController
{
    [ApiController]
    [Route("api/[controller]")]
    public class ViewerController : ControllerBase
    {
        private readonly ViewerManager _manager;

        public ViewerController(ViewerManager manager)
        {
            _manager = manager;
        }

        [HttpPost("signature")]
        public async Task<IActionResult> SaveSignature([FromBody] SignatureRequestDto request, string externalFileId, string versionNumber)
        {
            var userToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var result = await _manager.SaveSignature(request, userToken, externalFileId, versionNumber);
            return Ok(result);
        }
        [HttpPost("checkout")]
        public async Task<IActionResult> CheckOutViewer([FromBody] ChecksRequestDTO request)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer", "");

                if (string.IsNullOrWhiteSpace(token))
                    return Unauthorized("Missing or invalid Bearer token");

                var result = await _manager.CheckOutViewer(request, token);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest( new ErrorResponse { Error = "Checkout failed", Details = ex.Message });
            }
        }

        [HttpPost("checkin")]
        public async Task<IActionResult> CheckinViewer([FromBody] ChecksRequestDTO request)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer", "");

                if (string.IsNullOrWhiteSpace(token))
                    return Unauthorized("Missing or invalid Bearer token");

                var result = await _manager.CheckinViewer(request, token);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse { Error = "Checkin failed", Details = ex.Message });
            }
        }

        [HttpPost("annotation")]
        public async Task<IActionResult> SaveAnnotation([FromBody] AnnotationRequestDto request, string externalFileId, string versionNumber)
        {
            var userToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var result = await _manager.SaveAnnotation(request, userToken, externalFileId, versionNumber);
            return Ok(result);
        }
    }

}
