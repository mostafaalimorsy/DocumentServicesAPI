using DocumentServices.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentServices.Application.DTOs.ExternalDownloadsFile
{
    public class ViewerDetailsResponse
    {

        public string FileName { get; set; }
        public List<Annotation> Annotations { get; set; }
        public List<Signature> Signatures { get; set; }
        public Dictionary<string, bool> Permissions { get; set; }
    }
}
