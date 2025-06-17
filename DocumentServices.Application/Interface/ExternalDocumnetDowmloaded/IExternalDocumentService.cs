using DocumentServices.Application.DTOs.ExternalDonloadsFile;


namespace DocumentServices.Application.Interface.ExternalDocumnetDowmloaded
{
    public interface IExternalDocumentService
    {
        Task<ExternalFileAsPdfResponse> DownloadFileFromExternalApiAsync(ExternalFileDownloadRequest request, string userToken);

    }
}
