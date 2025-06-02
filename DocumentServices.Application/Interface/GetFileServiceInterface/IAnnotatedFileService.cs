using DocumentServices.DTOs.SaveFile;

namespace DocumentServices.Interface.GetFIleServiceInterface
{
    public interface IAnnotatedFileService
    {
        SaveEditedFileResponse SaveAnnotatedFile(SaveEditedFileRequest request);
    }
}
