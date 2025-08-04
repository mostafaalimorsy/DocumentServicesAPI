using DocumentServices.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentServices.Application.Interface
{
    public interface IDocumentRoutingService
    {
        Task<RemoteListResponseDto> GetListByIdAsync(int id, string userToken);
    }

}
