using DocumentService.DTOs;
using DocumentServices.Application.DTOs.ExternalDownloadsFile;
using DocumentServices.Application.Interface.ExternalDocumnetDowmloaded;
using DocumentServices.Domain.Models;
using DocumentServices.Infrastructure.External;
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
        [ProducesResponseType(typeof(ExternalFileUpdate), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> SaveSignature([FromBody] SignatureRequestDto request, string externalFileId, string versionNumber)
        {
            var userToken = Request.Headers["Authorization"].ToString();

            var result =
            await _manager.ProcessViewerUpdate(userToken, externalFileId, versionNumber, async (version) =>
            {
                return await _manager.SaveSignature(request, userToken, externalFileId, version);
            });

            return Ok(result);
        }
        //[HttpPost("checkout")]
        //public async Task<IActionResult> CheckOutViewer([FromBody] ChecksRequestDTO request)
        //{
        //    try
        //    {
        //        var token = Request.Headers["Authorization"].ToString().Replace("Bearer", "");

        //        if (string.IsNullOrWhiteSpace(token))
        //            return Unauthorized("Missing or invalid Bearer token");

        //        var result = await _manager.CheckOutViewer(request, token);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest( new ErrorResponse { Error = "Checkout failed", Details = ex.Message });
        //    }
        //}

        //[HttpPost("checkin")]
        //public async Task<IActionResult> CheckinViewer([FromBody] ChecksRequestDTO request)
        //{
        //    try
        //    {
        //        var token = Request.Headers["Authorization"].ToString().Replace("Bearer", "");

        //        if (string.IsNullOrWhiteSpace(token))
        //            return Unauthorized("Missing or invalid Bearer token");

        //        var result = await _manager.CheckinViewer(request, token);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new ErrorResponse { Error = "Checkin failed", Details = ex.Message });
        //    }
        //}

        [HttpPost("annotation")]
        [ProducesResponseType(typeof(ExternalFileUpdate), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> SaveAnnotation([FromBody] AnnotationRequestDto request, string externalFileId, string versionNumber)
        {
            var userToken = Request.Headers["Authorization"].ToString();
            var result = 
            await _manager.ProcessViewerUpdate(userToken, externalFileId, versionNumber, async (version) =>
            {
                return await _manager.SaveAnnotation(request, userToken, externalFileId, version);
            });

            return Ok(result);
        }
    }

}
