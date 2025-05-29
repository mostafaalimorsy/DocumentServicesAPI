/*
 * using DocumentService.DTOs;
using DocumentService.Interface.FileService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocumentService.Controllers.FileService
{

    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpGet("pdf")]
        [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
      

        public async Task<IActionResult> GetFileAsPdf([FromBody] GetFileAsPdfRequest request)
        {

            if (!ModelState.IsValid)
                return BadRequest(new ErrorResponse { Error = "ValidationError", Details = "" });


            try
            {
                var (fileBytes, contentType) = await _fileService.GetFileAsPdfAsync(request.FileName);

                var response = new FileAsPdfResponse
                {
                    OriginalFileName = request.FileName,
                    ConvertedFileName = Path.GetFileNameWithoutExtension(request.FileName) + ".pdf",
                    ContentType = contentType,
                   
                };

                return Ok(response);
            }
            catch (FileNotFoundException)
            {
                return NotFound(new ErrorResponse { Error = "FileNotFound" });
            }
            catch (NotSupportedException ex)
            {
                return BadRequest(new ErrorResponse { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse { Error = "ServerError", Details = ex.Message });
            }
        }
    }

}
*/