using DocumentServices.Application.DTOs.ExternalDownloadsFile;
using DocumentServices.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace DocumentServices.Application.DTOs.ExternalDonloadsFile
{
    public class ExternalFileAsPdfResponse
    {
        public string? FileName { get; set; }
        public string? Base64Pdf { get; set; }

        
        public List<Annotation>? Annotations { get; set; }
        public List<SignatureDto>? Signatures { get; set; }
        public PermissionsDto? Permissions { get; set; }
    }
}
