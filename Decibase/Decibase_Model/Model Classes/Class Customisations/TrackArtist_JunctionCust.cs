using System;
using System.Collections.Generic;
using System.Text;

namespace Decibase_Model
{
    public partial class TrackArtist_Junction
    {
        public override string ToString()
        {
            return $"{Track.Title}-{Artist.Name} Junction";
        }
    }
}
