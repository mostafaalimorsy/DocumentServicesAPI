using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentServices.Domain.Models
{

    public class Annotation
    {
        public string Id { get; set; }
        public string type { get; set; }
        public int PageNumber { get; set; }
        public int opacity { get; set; }
        public int annotationNumber { get; set; }
        public double posX { get; set; }
        public double posY { get; set; }
        public double posZ { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double borderWidth { get; set; }
        public string backgroundColor { get; set; }
        public string borderColor { get; set; }
        public string guid { get; set; }
    }

 

    public class AnnotationRequestDto
    {
        public List<Annotation> Annotations { get; set; }
    }
}
