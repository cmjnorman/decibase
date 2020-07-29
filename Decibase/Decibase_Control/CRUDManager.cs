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

        #region READ
        public Track RetrieveTrack(string title)
        {
            using (var db = new DecibaseContext()) return db.Tracks.First(t => t.Title == title);
        }

        public Album RetrieveAlbum(string title)
        {
            using (var db = new DecibaseContext()) return db.Albums.First(a => a.Title == title);
        }

        public Artist RetrieveArtist(string name)
        {
            using (var db = new DecibaseContext()) return db.Artists.First(t => t.Name == name);
        }

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

        public List<Track> RetrieveAlbumTracks(Album album)
        {
            using (var db = new DecibaseContext()) return db.Tracks.Where(t => t.AlbumId == album.AlbumId).ToList();
        }

        public List<Track> RetrieveArtistTracks(Artist artist)
        {
            using (var db = new DecibaseContext())
            {
                var artistIncludingTracks = db.Artists.Include(a => a.Tracks).ThenInclude(ta => ta.Track).First(a => a.ArtistId == artist.ArtistId);
                return artistIncludingTracks.Tracks.Select(ta => ta.Track).ToList();
            }
        }

        #endregion

       
    }
}
