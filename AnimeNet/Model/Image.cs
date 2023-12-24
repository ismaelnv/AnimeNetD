using System;

namespace AnimeNet.Model
{
    public class Image
    {

        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public DateTime uploadDate { get; set; }
        public DateTime updateDate { get; set; }
        public bool state { get; set; }
        public int? animeId { get; set; }
        public int imageType { get; set; }
        public int imageCategory { get; set; }
        public int? chapterId { get; set; }
    }
}
