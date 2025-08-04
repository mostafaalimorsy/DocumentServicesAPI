using DocumentServices.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentServices.Application.Interface
{
    public interface IDraftService
    {
        Task<DraftCountDto> GetDraftCountsAsync(int nodeId, string userToken);
    }

}
