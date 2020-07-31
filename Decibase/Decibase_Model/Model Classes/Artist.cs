using System;
using System.Collections.Generic;
using System.Text;

namespace Decibase_Model
{
    public partial class Artist
    {
        public int ArtistId { get; set; }
        public string Name { get; set; }
        public List<TrackArtist_Junction> Tracks { get; set; } = new List<TrackArtist_Junction>();
    }
}
