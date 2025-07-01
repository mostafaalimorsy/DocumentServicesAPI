using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentServices.Application.DTOs.ExternalDownloadsFile
{

    public class SignatureDto
    {
        //public int Id { get; set; }
        public int PageNumber { get; set; }
        //public string Type { get; set; }
        public double PosX { get; set; }
        public double PosY { get; set; }
        public double PosZ { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public string? ImageSource { get; set; } // customize if you know signature structure
        public string? Language { get; set; } // customize if you know signature structure
        public bool IsDelegated { get; set; } // customize if you know signature structure
        public string? DelegatedUser { get; set; } // customize if you know signature structure
    }
    public class SignatureRequestDto
    {
        public string PinCode { get; set; } = "-1";
        public List<SignatureDto>? Signatures { get; set; }
    }
}
