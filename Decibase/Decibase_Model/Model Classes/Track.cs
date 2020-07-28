using System;
using System.Collections.Generic;
using System.Text;

namespace Decibase_Model
{ 
    public partial class Track
    {
        public int TrackId { get; set; }
        public string Title { get; set; }
        public int TrackNumber { get; set; }
        public int DiscNumber { get; set; }
        public string Genre { get; set; }

        public int AlbumId { get; set; }
        public Album Album { get; set; }

        public List<TrackArtist_Junction> Artists { get; set; } = new List<TrackArtist_Junction>();
        
        //removed for now - unsure how to like many juncion tables of the same type.

        //public List<TrackArtist_Junction> Features { get; set; } = new List<TrackArtist_Junction>();
        //public List<TrackArtist_Junction> Remixers { get; set; } = new List<TrackArtist_Junction>();
    }
}
