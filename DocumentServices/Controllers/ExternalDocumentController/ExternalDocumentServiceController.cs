using DocumentService.DTOs;
using DocumentServices.Application.DTOs.ExternalDonloadsFile;
using DocumentServices.Application.DTOs.ExternalDownloadsFile;
using DocumentServices.Application.Interface.ExternalDocumnetDowmloaded;
using DocumentServices.Domain.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocumentServices.Controllers.ExternalDocumentController
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentsController : ControllerBase
    {
        private readonly IExternalDocumentService _externalDocumentService;

        public DocumentsController(IExternalDocumentService externalDocumentService)
        {
            _externalDocumentService = externalDocumentService;
        }

        [HttpPost("external-download")]
        [ProducesResponseType(typeof(ExternalFileAsPdfResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DownloadExternal([FromBody] ExternalFileDownloadRequest request)
        {
            if (!ModelState.IsValid)
                return Unauthorized(new ErrorResponse { Error = "UnAuthroized", Details = "after conntect with IAM your credintionl is wrong" });

            try
            {
                var userToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

                if (string.IsNullOrEmpty(userToken))
                    return Unauthorized(new ErrorResponse { Error = "UnAuthroized", Details = "after conntect with IAM your credintionl is missing" });

                var response = await _externalDocumentService.DownloadFileFromExternalApiAsync(request, userToken);

                return Ok(response);
            }
            catch (ErrorException ex)
            {
                return BadRequest(new ErrorResponse { Error = ex.ErrorResponse.Error, Details = ex.ErrorResponse.Details });
            }
           
        }
    }

}
