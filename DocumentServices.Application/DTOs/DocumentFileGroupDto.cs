using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentServices.Application.DTOs
{
    public class FileItemDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public string Extension { get; set; }
        public string ContentType { get; set; }
        public bool IsLocked { get; set; }
        public int? TaskId { get; set; }
        public bool? IsSigned { get; set; }
    }

    public class DocumentFileGroupDto
    {
        public string Name { get; set; }
        public List<FileItemDto> Files { get; set; }
    }

}
