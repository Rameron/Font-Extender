namespace Font_Extender.Models
{
    public class TTGlyph
    {
        public string Name { get; set; }
        public int XMin { get; set; }
        public int YMin { get; set; }
        public int XMax { get; set; }
        public int YMax { get; set; }

        public Contour[] Contours { get; set; }
    }
}
