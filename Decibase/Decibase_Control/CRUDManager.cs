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
            using (var db = new DecibaseContext()) return db.Tracks.FirstOrDefault(t => t.Title == title);
        }

        public Album RetrieveAlbum(string title)
        {
            using (var db = new DecibaseContext()) return db.Albums.FirstOrDefault(a => a.Title == title);
        }

        public Artist RetrieveArtist(string name)
        {
            using (var db = new DecibaseContext()) return db.Artists.FirstOrDefault(t => t.Name == name);
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

        public List<Artist> RetrieveTrackArtists(Track track)
        {
            using (var db = new DecibaseContext())
            {
                var trackIncludingArtists = db.Tracks.Include(t => t.Artists).ThenInclude(ta => ta.Artist).First(t => t.TrackId == track.TrackId);
                return trackIncludingArtists.Artists.Select(ta => ta.Artist).ToList();
            }
        }

        #endregion

        #region UPDATE
        public void TrackSetTitle(Track track, string title)
        {
            track.Title = title;
        }
        public void TrackSetTrackNumber(Track track, int number)
        {
            track.TrackNumber = number;
        }

        public void TrackSetDiscNumber(Track track, int number)
        {
            track.DiscNumber = number;
        }

        public void TrackSetGenre(Track track, string genre)
        {
            track.Genre = genre;
        }

        public void TrackSetAlbum(Track track, Album album)
        {
            track.Album = album;
        }

        public void JoinTrackToArtist(Track track, Artist artist)
        {
            using (var db = new DecibaseContext())
            {
                if (db.TrackArtists.FirstOrDefault(ta => ta.TrackId == track.TrackId && ta.ArtistId == artist.ArtistId) == null)
                {
                    var ta = new TrackArtist_Junction() { TrackId = track.TrackId, ArtistId = artist.ArtistId };
                    db.Add(ta);
                    db.SaveChanges();
                }
            }
        }

        public void UnjoinTrackFromArtist(Track track, Artist artist)
        {
            using (var db = new DecibaseContext())
            {
                var ta = db.TrackArtists.First(ta => ta.TrackId == track.TrackId && ta.ArtistId == artist.ArtistId);
                db.Remove(ta);
                db.SaveChanges();
            }
        }

        public void AlbumSetTitle(Album album, string title)
        {
            album.Title = title;
        }
        public void AlbumSetTotalTracks(Album album, int number)
        {
            album.TotalTracks = number;
        }

        public void AlbumSetTotalDiscs(Album album, int number)
        {
            album.TotalDiscs = number;
        }

        public void AlbumSetYear(Album album, string year)
        {
            album.Year = year;
        }

        public void ArtistSetName(Artist artist, string name)
        {
            artist.Name = name;
        }

        public void ArtistSetNationality(Artist artist, string nationality)
        {
            artist.Nationality = nationality;
        }
        #endregion

        #region DELETE
        public void DeleteTrack(Track track)
        {
            using (var db = new DecibaseContext())
            {
                var trackArtists = db.TrackArtists.Where(ta => ta.TrackId == track.TrackId).ToList();
                if (trackArtists.Count > 0) db.RemoveRange(trackArtists);
                db.Remove(track);
                db.SaveChanges();
            }
        }

        public void DeleteAlbum(Album album)
        {
            using (var db = new DecibaseContext())
            {
                db.Remove(album);
                db.SaveChanges();
            }
        }

        public void DeleteArtist(Artist artist)
        {
            using (var db = new DecibaseContext())
            {
                var artistTracks = db.TrackArtists.Where(ta => ta.ArtistId == artist.ArtistId).ToList();
                if(artistTracks.Count > 0) db.Remove(artistTracks);
                db.Remove(artist);
                db.SaveChanges();
            }
        }
        #endregion

    }
}
