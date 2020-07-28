using System;
using System.Collections.Generic;
using System.Linq;
using Decibase_Model;
using Microsoft.EntityFrameworkCore;

namespace Decibase_Control
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
        #region CREATE
        public void AddNewTrack(string title, string albumTitle)
        {
            using (var db = new DecibaseContext())
            {
                AddNewAlbum(albumTitle);
                var album = db.Albums.First(a => a.Title == albumTitle);
                var newTrack = new Track { Title = title, Album = album };
                if (!RetrieveAllTracks().Contains(newTrack))
                {
                    db.Add(newTrack);
                    db.SaveChanges();
                }
            }
        }

        public void AddNewAlbum(string title)
        {
            using (var db = new DecibaseContext())
            {
                var newAlbum = new Album { Title = title };
                if (!RetrieveAllAlbums().Contains(newAlbum))
                {
                    db.Add(newAlbum);
                    db.SaveChanges();
                }
            }
        }

        public void AddNewArtist(string name)
        {
            using (var db = new DecibaseContext())
            {
                var newArtist = new Artist { Name = name };
                if (!RetrieveAllArtists().Contains(newArtist))
                {
                    db.Add(newArtist);
                    db.SaveChanges();
                }
            }
        }
        #endregion

       
    }
}
