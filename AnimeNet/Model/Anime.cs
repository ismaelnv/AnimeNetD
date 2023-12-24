using System;
using System.Collections.Generic;

namespace AnimeNet.Model
{
    public class Anime
    {
        
        public int Id { get; set; }
        public string name { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public DateTime uploadDate { get; set; }
        public DateTime updateDate { get; set; }
        public string imagePoster { get; set; } = string.Empty;
        public string imageCover { get; set; } = string.Empty;
        public bool state { get; set; }
        public virtual ICollection<Chapter> Chapters { get; } = new List<Chapter>();
        public virtual List<Image> Images { get; set; } = new List<Image>();
    }
}
