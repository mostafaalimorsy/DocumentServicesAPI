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
        public string AnnotationType { get; set; }
        public int PageNumber { get; set; }
        public Position Position { get; set; }
        public Size Size { get; set; }
        public string Color { get; set; }
        public string Author { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class Position
    {
        public double X { get; set; }
        public double Y { get; set; }
    }

    public class Size
    {
        public double Width { get; set; }
        public double Height { get; set; }
    }

    public class AnnotationRequest
    {
        public List<Annotation> Annotations { get; set; }
    }
}
