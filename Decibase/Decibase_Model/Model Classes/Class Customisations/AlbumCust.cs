using System;
using System.Collections.Generic;
using System.Text;

namespace Decibase_Model
{
    public partial class Album
    {
        public override bool Equals(object obj)
        {
            if (!(obj is Album)) return false;

            var album = (Album)obj;
            if (album.Title != this.Title) return false;
            if (album.Year != null && this.Year != null && album.Year != this.Year) return false;

            return true;
        }

        public override int GetHashCode()
        {
            return Int32.Parse(this.Year) + this.Title.GetHashCode();
        }

        public override string ToString()
        {
            return Year == null ? Title : $"{Title} ({Year})";
        }
    }
}
