using System.ComponentModel.DataAnnotations;

namespace DocumentServices.DTOs.SaveFile
{
    public class SaveEditedFileRequest
    {
        [Required(ErrorMessage = "FileId is required.")]
        public string FileId { get; set; } = string.Empty;

        [Required(ErrorMessage = "FileDataBase64 is required.")]
        public string FileDataBase64 { get; set; } = string.Empty;

        public List<string> Annotations { get; set; } = new();
    }
}
