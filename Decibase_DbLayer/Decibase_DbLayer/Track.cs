using System;
using System.Collections.Generic;
using System.Text;

namespace Decibase_DbLayer
{ 
    public class Track
    {
        public int TrackId { get; set; }
        public string Title { get; set; }
        public int TrackNumber { get; set; }
        public int DiscNumber { get; set; }
        public string Genre { get; set; }

        public int AlbumId { get; set; }
        public Album Album { get; set; }

        public List<TrackArtist> Artists { get; set; }
    }
}
