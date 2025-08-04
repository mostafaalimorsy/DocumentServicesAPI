using DocumentService.DTOs;
using DocumentServices.Application.DTOs;
using DocumentServices.Application.DTOs.ExternalDownloadsFile;
using DocumentServices.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DocumentServices.Controllers
{
   


    [ApiController]
    [Route("api/[controller]")]
    public class DocumentFileGroupContrller : ControllerBase
    {
        private readonly IDocumentFileGroupService _fileService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DocumentFileGroupContrller(IDocumentFileGroupService fileService, IHttpContextAccessor httpContextAccessor)
        {
            _fileService = fileService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("document-files")]
        [ProducesResponseType(typeof(DocumentFileGroupDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetFiles([FromQuery] int documentId)
        {
            var userToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var result = await _fileService.GetFilesByDocumentIdAsync(documentId, userToken);
            return Ok(result);
        }
    }

}
