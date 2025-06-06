﻿using DocumentService.Interface.FileService;

namespace DocumentService.Services.FileService
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _env;

        public FileService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<(byte[] fileBytes, string contentType)> GetFileAsPdfAsync(string fileName)
        {
            var filePath = Path.Combine(_env.ContentRootPath, "Files", fileName);

            if (!File.Exists(filePath))
                throw new FileNotFoundException("File not found");

            var ext = Path.GetExtension(filePath).ToLowerInvariant();
            byte[] pdfBytes;

            switch (ext)
            {
                case ".pdf":
                    pdfBytes = await File.ReadAllBytesAsync(filePath);
                    return (pdfBytes, "application/pdf");

                case ".docx":
                case ".doc":
                case ".txt":
                    var doc = new Aspose.Words.Document(filePath);
                    using (var ms = new MemoryStream())
                    {
                        doc.Save(ms, Aspose.Words.SaveFormat.Pdf);
                        return (ms.ToArray(), "application/pdf");
                    }

                case ".xlsx":
                case ".xls":
                    var workbook = new Aspose.Cells.Workbook(filePath);
                    using (var ms = new MemoryStream())
                    {
                        workbook.Save(ms, Aspose.Cells.SaveFormat.Pdf);
                        return (ms.ToArray(), "application/pdf");
                    }
                case ".jpg":
                case ".jpeg":
                case ".png":
                case ".bmp":
                    using (var image = Aspose.Imaging.Image.Load(filePath))
                    {
                        var options = new Aspose.Imaging.ImageOptions.PdfOptions();
                        using var imgStream = new MemoryStream();
                        image.Save(imgStream, options);
                        pdfBytes = imgStream.ToArray();
                    }
                    return (pdfBytes, "application/pdf");

                default:
                    throw new NotSupportedException("Unsupported file type");
            }
        }
    }


}
