using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentServices.Application.DTOs
{
 

    public class RemoteListResponseDto
    {
        public int? Draw { get; set; }
        public int? RecordsTotal { get; set; }
        public int? RecordsFiltered { get; set; }
        public List<RemoteDocumentItem> Data { get; set; }

    }

    public class RemoteDocumentItem
    {
        public int? Id { get; set; }
        public int? DocumentId { get; set; }
        public int? DocumentTypeId { get; set; }
        public string DocumentType { get; set; }
        public string ReferenceNumber { get; set; }
        public string TaskDate { get; set; }
        public string CreatedDate { get; set; }
        public string DueDate { get; set; }
        public int? Status { get; set; }
        public int? OwnerUserId { get; set; }
        public int? OwnerDelegatedUserId { get; set; }
        public bool IsOverDue { get; set; }
        public bool IsAssigned { get; set; }
        public bool IsTransferred { get; set; }
        public bool IsRead { get; set; }
        public bool IsLocked { get; set; }
        public string LockedBy { get; set; }
        public string LockedByDelegatedUser { get; set; }
        public string LockedDate { get; set; }
        public bool SentToUser { get; set; }
        public bool SentToGroup { get; set; }
        public bool SentToStructure { get; set; }
        public string OpenedDate { get; set; }
        public string ClosedDate { get; set; }
        public string CreatedByUser { get; set; }
        public string FormData { get; set; }
        public string DocumentFormData { get; set; }
    }

}
