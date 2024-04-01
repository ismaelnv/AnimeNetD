using System.Collections.Generic;
using System;

namespace AnimeNet.Model
{
    public class Chapter
    {

        public int id { get; set; }
        public string title { get; set; } = string.Empty;
        public int episode { get; set; }
        public string description { get; set; } = string.Empty;
        public DateTime uploadDate { get; set; }
        public DateTime updateDate { get; set; }
        public int animeId { get; set; }
        public bool state { get; set; }
        public string imageCover { get; set; } = string.Empty;
        public virtual Anime AnimeModel { get; set; }
        public List<Image> Images { get; set; } = new List<Image>();
    }
}
