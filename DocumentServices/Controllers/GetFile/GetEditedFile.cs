﻿using DocumentService.Domain.Helpers;
using DocumentService.DTOs;
using DocumentServices.DTOs.SaveFile;
using DocumentServices.Interface.GetFIleServiceInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocumentService.Controllers
{
    [Authorize(AuthenticationSchemes = "IamScheme")]
    [ApiController]
    [Route("api/[controller]")]
    public class GetEditedFile : ControllerBase
    {
        private readonly IAnnotatedFileService _annotatedFileService;

        public GetEditedFile(IAnnotatedFileService annotatedFileService)
        {
            _annotatedFileService = annotatedFileService;
        }

        [Authorize(AuthenticationSchemes = "IamScheme")]
        [HttpPost("save-annotated-file")]
        [ProducesResponseType(typeof(SaveEditedFileResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        public IActionResult SaveAnnotatedFile([FromBody] SaveEditedFileRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest( new ErrorResponse{ Error = "Validation failed",  Details = $"errors" });
            }
            bool isPdf = FileValidator.IsPdf(request.FileDataBase64);
            if (!isPdf)
            {
                return BadRequest(new ErrorResponse { Error = "Content unspported", Details = "the file that use is not pdf " });

            }
            else
            {
                try
                {
                    var response = _annotatedFileService.SaveAnnotatedFile(request);
                    var baseUrl = $"{Request.Scheme}://{Request.Host}";
                    response.FileUrl = $"{baseUrl}/{response.FileUrl}"; // prepend scheme + host
                    return Ok(response);
                }
                catch (ArgumentException ex)
                {
                    return BadRequest(new ErrorResponse { Error = "one or more fileds is wrong or empty", Details = ex.Message });
                }
                catch (Exception ex)
                {
                    return BadRequest(new ErrorResponse { Error = "An Issue happend", Details = ex.Message });

                }

            }

               
        }


    }
}
