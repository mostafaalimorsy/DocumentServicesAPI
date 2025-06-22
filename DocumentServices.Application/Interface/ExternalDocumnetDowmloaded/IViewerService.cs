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
        Task<string> SaveSignatureAsync(SignatureRequestDto request, String userToken);
        Task<string> SaveAnnotationAsync(AnnotationRequestDto request,String userToken);
    }
}
