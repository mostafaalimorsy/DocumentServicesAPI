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
        public async Task<IActionResult> SaveSignature([FromBody] SignatureRequestDto request)
        {
            var userToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var result = await _manager.SaveSignature(request, userToken);
            return Ok(result);
        }

        [HttpPost("annotation")]
        public async Task<IActionResult> SaveAnnotation([FromBody] AnnotationRequestDto request)
        {
            var userToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var result = await _manager.SaveAnnotation(request, userToken);
            return Ok(result);
        }
    }

}
