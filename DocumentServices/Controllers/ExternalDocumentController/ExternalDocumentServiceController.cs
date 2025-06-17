using DocumentService.DTOs;
using DocumentServices.Application.DTOs.ExternalDonloadsFile;
using DocumentServices.Application.Interface.ExternalDocumnetDowmloaded;
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
            catch (Exception ex)
            {
                return Unauthorized(new ErrorResponse { Error = "Error", Details =ex.Message });
            }
           
        }
    }

}
