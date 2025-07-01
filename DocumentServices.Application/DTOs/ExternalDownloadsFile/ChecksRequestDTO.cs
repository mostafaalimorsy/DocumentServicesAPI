using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentServices.Application.DTOs.ExternalDownloadsFile
{
    public class ChecksRequestDTO
    {
        public string ExternalFileId { get; set; }
        public string VersionNumber { get; set; }
      
    }
}
