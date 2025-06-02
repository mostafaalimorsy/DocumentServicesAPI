namespace DocumentServices.DTOs.SaveFile
{
    public class SaveEditedFileResponse
    {
        public string FileName { get; set; } = string.Empty;
        public string FileId { get; set; } = string.Empty;
        public string FileUrl { get; set; } = string.Empty;
        public List<string> Annotations { get; set; } = new();
        public string Message { get; set; } = string.Empty;
    }
}
