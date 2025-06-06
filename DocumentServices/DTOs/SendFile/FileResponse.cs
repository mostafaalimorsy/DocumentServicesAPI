﻿namespace DocumentServices.DTOs.SendFile
{
    public class FileAsPdfResponse
    {
        public string? OriginalFileName { get; set; }
        public string? ConvertedFileName { get; set; }
        public string? ContentType { get; set; }
        public string? FileUrl{ get; set; }
        public string? FileBytes { get; set; }
        public List<string> Annotation { get; set; } = new();

    }

}
