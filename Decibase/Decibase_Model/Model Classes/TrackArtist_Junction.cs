using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Decibase_Model
{
    public partial class TrackArtist_Junction
    {
        public int TrackId { get; set; }
        public int ArtistId { get; set; }
        public Track Track { get; set; }
        public Artist Artist { get; set; }
    }
}
