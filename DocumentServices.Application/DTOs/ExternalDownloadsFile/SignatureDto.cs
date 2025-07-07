using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DocumentServices.Application.DTOs.ExternalDownloadsFile
{

    public class SignatureRequestDto
    {
        [JsonPropertyName("signatures")]
        public List<SignatureItemDto> Signatures { get; set; }

        [JsonPropertyName("pinCode")]
        public string PinCode { get; set; } = "-1";
    }

    public class SignatureItemDto
    {
        [JsonPropertyName("pageNumber")]
        public int PageNumber { get; set; }

        [JsonPropertyName("posX")]
        public double PosX { get; set; }

        [JsonPropertyName("posY")]
        public double PosY { get; set; }

        [JsonPropertyName("posZ")]
        public int PosZ { get; set; }

        [JsonPropertyName("width")]
        public double Width { get; set; }

        [JsonPropertyName("height")]
        public double Height { get; set; }

        [JsonPropertyName("imageSource")]
        public string ImageSource { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; } = "en";
    }

}
