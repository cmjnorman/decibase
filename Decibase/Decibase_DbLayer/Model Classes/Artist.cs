using System;
using System.Collections.Generic;
using System.Text;

namespace Decibase_DbLayer
{
    public partial class Artist
    {
        public int ArtistId { get; set; }
        public string Name { get; set; }
        public string Nationality { get; set; }
        public List<TrackArtist_JunctionTable> Tracks { get; set; }
    }
}
