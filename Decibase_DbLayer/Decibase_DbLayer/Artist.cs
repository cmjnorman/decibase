using System;
using System.Collections.Generic;
using System.Text;

namespace Decibase_DbLayer
{
    public class Artist
    {
        public int ArtistId { get; set; }
        public string Name { get; set; }
        public string Nationality { get; set; }
        public List<TrackArtist> Tracks { get; set; }
    }
}
