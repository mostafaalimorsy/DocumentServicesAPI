


using DocumentServices.DTOs.SaveFile;
using DocumentServices.Interface.GetFIleServiceInterface;

namespace DocumentService.Service
{
    public class AnnotatedFileService : IAnnotatedFileService
    {
        private readonly IWebHostEnvironment _env;

        public AnnotatedFileService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public SaveEditedFileResponse SaveAnnotatedFile(SaveEditedFileRequest request)
        {
            var fileName = $"{request.FileId}_{DateTime.UtcNow.Ticks}.pdf";
            var wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files");

            if (!Directory.Exists(wwwrootPath))
                Directory.CreateDirectory(wwwrootPath);

            var filePath = Path.Combine(wwwrootPath, fileName);
            var fileBytes = Convert.FromBase64String(request.FileDataBase64);
            File.WriteAllBytes(filePath, fileBytes);

            return new SaveEditedFileResponse
            {
                FileId = request.FileId,
                FileName = fileName,
                FileUrl = $"files/{fileName}" // We'll prepend base URL in controller
            };
        }

    }
}
