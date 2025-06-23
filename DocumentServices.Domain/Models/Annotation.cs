using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentServices.Domain.Models
{
    public class Annotation
    {



        public string BackgroundColor { get; set; }
        public string BorderColor { get; set; }
        public int PageNumber { get; set; }
        public string AddedByUser { get; set; }
        public string Type { get; set; }
        public DateTime DateAdded { get; set; }
        public float PosX { get; set; }
        public float PosY { get; set; }
        public float PosZ { get; set; }
        public float BorderWidth { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public int Id { get; set; }
        public string Guid { get; set; }
        public float Opacity { get; set; }
        public bool Locked { get; set; }
        public string Text { get; set; }
        public string ImageSource { get; set; }
        public string AnnotationTextFormat { get; set; }
        public string AddedByUserName { get; set; }

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
