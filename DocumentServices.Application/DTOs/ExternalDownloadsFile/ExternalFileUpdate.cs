using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentServices.Application.DTOs.ExternalDownloadsFile
{
    public class ExternalFileUpdate
    {
        public bool IsCheckedOut { get; set; }
        public bool IsCheckedIn { get; set; }
        public bool IsFileUpdateed { get; set; }
        public string? Message { get; set; }
    }
}
