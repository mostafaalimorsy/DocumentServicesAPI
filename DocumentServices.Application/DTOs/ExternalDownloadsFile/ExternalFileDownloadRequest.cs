using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentServices.Application.DTOs.ExternalDonloadsFile
{
    public class ExternalFileDownloadRequest
    {
        [Required(ErrorMessage = "FileId is required")]
        public string? ExternalFileId { get; set; }
        [Required(ErrorMessage = "FileId is required")]

        public string? DocumentId { get; set; }

    }
}
