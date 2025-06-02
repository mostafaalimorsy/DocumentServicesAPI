namespace DocumentServices.DTOs.SendFile
{
    using System.ComponentModel.DataAnnotations;

    public class GetFileAsPdfRequest
    {
        [Required(ErrorMessage = "FileName is required")]
        public string FileName { get; set; }

        [Required (ErrorMessage = "USer Token is Requird")]
        public string UserToken { get; set; }

    }

}
