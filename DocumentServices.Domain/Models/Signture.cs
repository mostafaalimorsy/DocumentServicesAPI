using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentServices.Domain.Models
{
    public class Signature
    {
        public int PageNumber { get; set; }
        public double PosX { get; set; }
        public double PosY { get; set; }
        public int PosZ { get; set; }
        public string ImageSource { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public string Language { get; set; }
    }

    public class SignatureRequest
    {
        public string PinCode { get; set; } = "-1";
        public List<Signature> Signatures { get; set; }
    }

}
