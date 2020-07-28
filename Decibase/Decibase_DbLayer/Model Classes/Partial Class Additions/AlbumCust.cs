using System;
using System.Collections.Generic;
using System.Text;

namespace Decibase_DbLayer.Model_Classes
{
    public partial class Album
    {
        public override bool Equals(object obj)
        {
            if (!(obj is Album)) return false;
            var album = (Album)obj;

            return true;
        }
    }
}
