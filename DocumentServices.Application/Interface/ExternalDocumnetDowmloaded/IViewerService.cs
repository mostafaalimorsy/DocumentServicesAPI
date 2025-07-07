using DocumentServices.Application.DTOs.ExternalDownloadsFile;
using DocumentServices.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentServices.Application.Interface.ExternalDocumnetDowmloaded
{
    public interface IViewerService
    {
        Task<string> SaveSignatureAsync(SignatureRequestDto request, string userToken, string externalFileId, string versionNumber);
        Task<string> SaveAnnotationAsync(AnnotationRequestDto request,string userToken, string externalFileId, string versionNumber);
        Task<string> CheckOutViewer(ChecksRequestDTO request,string userToken);
        Task<string> CheckInViewer(ChecksRequestDTO request,string userToken);
        Task<string> ProcessViewerUpdate(string userToken, string externalFileId,string versionNumber,Func<string, Task<string>> actionToPerform);
    }
}
