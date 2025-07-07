using DocumentServices.Application.DTOs.ExternalDownloadsFile;
using DocumentServices.Application.Interface.ExternalDocumnetDowmloaded;
using DocumentServices.Domain.Models;

namespace DocumentServices.Services
{
    public class ViewerManager
    {
        private readonly IViewerService _viewerService;

        public ViewerManager(IViewerService viewerService)
        {
            _viewerService = viewerService;
        }

        public Task<string> SaveSignature(SignatureRequestDto request,string userToken, string externalFileId, string versionNumber)
            => _viewerService.SaveSignatureAsync(request, userToken,  externalFileId,  versionNumber);

        public Task<string> SaveAnnotation(AnnotationRequestDto request,string userToken, string externalFileId, string versionNumber)
            => _viewerService.SaveAnnotationAsync(request, userToken,  externalFileId,  versionNumber);
        public Task<string> CheckOutViewer(ChecksRequestDTO request, string userToken)
            => _viewerService.CheckOutViewer( request,userToken);
        public Task<string> CheckinViewer(ChecksRequestDTO request, string userToken)
            => _viewerService.CheckInViewer(request, userToken);

        public Task<string> ProcessViewerUpdate(string userToken,
        string externalFileId,
        string versionNumber,
        Func<string, Task<string>> actionToPerform)
             => _viewerService.ProcessViewerUpdate(
         userToken,
         externalFileId,
         versionNumber,
       actionToPerform);
    }

}
