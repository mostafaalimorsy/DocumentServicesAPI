namespace DocumentServices.DTOs.SendFile
{
    using System.ComponentModel.DataAnnotations;

    public class GetFileAsPdfRequest
    {
        [Required(ErrorMessage = "FileName is required")]
        public string FileName { get; set; }


    }

}
