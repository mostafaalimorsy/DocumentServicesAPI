using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentServices.Application.DTOs.ExternalDonloadsFile
{
    public class ExternalFileAsPdfResponse
    {
        public string? FileName { get; set; }
        public string? Base64Pdf { get; set; }
    }
}
