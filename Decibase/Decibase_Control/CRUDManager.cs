using System;
using System.Collections.Generic;
using System.Linq;
using Decibase_Model;

namespace Decibase_BusinessLayer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    public class CRUDManager
    {
        public List<Track> RetrieveAllTracks()
        {
            using (var db = new DecibaseContext()) return db.Tracks.ToList();
        }

        public List<Album> RetrieveAllAlbums()
        {
            using (var db = new DecibaseContext()) return db.Albums.ToList();
        }

        public List<Artist> RetrieveAllArtists()
        {
            using (var db = new DecibaseContext()) return db.Artists.ToList();
        }

        //public void AddAlbum(string title)
        //{
        //    using (var db = new DecibaseContext()) db.Add(new Album);
        //}

        //public void AddTrack(string title, string artist, string albumName, int trackNumber, int diskNumber, string genre)
        //{
        //    using (var db = new DecibaseContext())
        //    {
        //        if (db.Albums.Where(a => a.Name == albumName).FirstOrDefault() == null)
        //        {
        //            db.Albums.Add(new Album { Name = albumName });
        //        }
        //        var album = db.Albums.Where(a => a.Name == albumName).FirstOrDefault();
        //        db.Tracks.Add(new Track { Title = title, });
        //    }
        //}
    }
}
