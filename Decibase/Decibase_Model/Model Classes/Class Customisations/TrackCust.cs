using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Decibase_Model
{
    public partial class Track
    {
        public override bool Equals(object obj)
        {
            if (!(obj is Track)) return false;

            var track = (Track)obj;
            if (track.Title != this.Title) return false;
            if (track.Album != null && this.Album != null && track.Album != this.Album) return false;
            if (track.Artists.Count != this.Artists.Count) return false;
            for(int i = 0; i < track.Artists.Count; i++)
            {
                if (track.Artists.ToList()[i].Artist != this.Artists.ToList()[i].Artist) return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            var sb = new StringBuilder();
            foreach(var artist in this.Artists)
            {
                sb.Append(artist.GetHashCode());
            }
            return this.Title.GetHashCode() + Int32.Parse(sb.ToString()) + this.Album.GetHashCode();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (Artists.Count > 0)
            {
                sb.Append(Artists.ToList()[0].Artist);
                for (int i = 1; i < Artists.Count; i++)
                {
                    sb.Append("; " + Artists.ToList()[i].Artist);
                }
            }
            return $"{sb.ToString()} - {Title}";
        }
    }
}
