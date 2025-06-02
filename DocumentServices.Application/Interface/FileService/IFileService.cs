namespace DocumentService.Interface.FileService
{
    public interface IFileService
    {
        Task<(byte[] fileBytes, string contentType)> GetFileAsPdfAsync(string fileName);
    }

}
