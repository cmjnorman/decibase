using System;
using System.Collections.Generic;
using System.Text;

namespace Decibase_Model
{
    public partial class Album
    {
        public int AlbumId { get; set; }
        public string Title { get; set; }
        public int TotalTracks { get; set; }
        public int TotalDiscs { get; set; }
        public string Year { get; set; }
        public List<Track> Tracks { get; set; } = new List<Track>();
    }
}
