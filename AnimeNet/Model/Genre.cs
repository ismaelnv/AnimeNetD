using System;
using System.Collections.Generic;

namespace AnimeNet.Model
{
    public class Genre
    {

        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public DateTime uploadDate { get; set; }
        public DateTime updateDate { get; set; }
        public bool state { get; set; }
        public virtual List<Anime> animes { get; set; } 
    }
}
