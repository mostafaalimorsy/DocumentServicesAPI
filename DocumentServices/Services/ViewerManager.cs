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

        public Task<string> SaveSignature(SignatureRequestDto request,string userToken)
            => _viewerService.SaveSignatureAsync(request, userToken);

        public Task<string> SaveAnnotation(AnnotationRequestDto request,string userToken)
            => _viewerService.SaveAnnotationAsync(request, userToken);
    }

}
