
using DocumentService.DTOs;
using DocumentService.Interface.FileService;
using DocumentServices.DTOs.SendFile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocumentService.Controllers.FileService
{
    [Authorize(AuthenticationSchemes = "IamScheme")]
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }
        [Authorize(AuthenticationSchemes = "IamScheme")]
        [HttpGet("pdf")]
        [ProducesResponseType(typeof(FileAsPdfResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
      

        public async Task<IActionResult> GetFileAsPdf([FromQuery] GetFileAsPdfRequest request)
        {

            if (!ModelState.IsValid)
                return BadRequest(new ErrorResponse { Error = "ValidationError", Details = "the token is invalid" });
            var authHeader = HttpContext.Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(authHeader))
            {
                return BadRequest(new ErrorResponse { Error = "ValidationError", Details = "the token is invalid" });
            }

            try
            {
                var (fileBytes, contentType) = await _fileService.GetFileAsPdfAsync(request.FileName);
                string outputFileName = Path.GetFileNameWithoutExtension(request.FileName) + ".pdf";
                string outputPath = Path.Combine("wwwroot", "files", outputFileName);
                System.IO.File.WriteAllBytes(outputPath, fileBytes);

                var fileUrl = $"{Request.Scheme}://{Request.Host}/files/{outputFileName}";
                var response = new FileAsPdfResponse
                {
                    OriginalFileName = request.FileName,
                    ConvertedFileName = Path.GetFileNameWithoutExtension(request.FileName) + ".pdf",
                    ContentType = contentType,
                    FileBytes = Convert.ToBase64String(fileBytes),
                    FileUrl = fileUrl,
                    Annotation = new List<string> {}
                };

                return Ok(response);
            }
            catch (FileNotFoundException)
            {
                return NotFound(new ErrorResponse { Error = "FIle Not Found", Details = "FileNotFound" });
            }
            catch (NotSupportedException ex)
            {
                return BadRequest(new ErrorResponse { Error = "File Extenission not supported", Details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse { Error = "Server Error", Details = ex.Message });
            }
        }
    }

}
