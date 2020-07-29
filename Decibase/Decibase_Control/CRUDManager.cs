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
        private Artist selectedArtist;
        private Album selectedAlbum;
        private Track selectedTrack;

        #region Selection
        public void SelectArtist(string artistName)
        {
            using(var db = new DecibaseContext())
            {
                selectedArtist = GetArtist(artistName);
            }
        }

        public void SelectAlbum(string albumTitle)
        {
            using (var db = new DecibaseContext())
            {
                selectedAlbum = GetAlbum(albumTitle);
            }
        }

        public void SelectTrack(string trackTitle)
        {
            using (var db = new DecibaseContext())
            {
                selectedTrack = GetTrack(trackTitle);
            }
        }

        #endregion

        #region CREATE
        public void AddNewTrack(string title, string albumTitle)
        {
            using (var db = new DecibaseContext())
            {
                AddNewAlbum(albumTitle);
                var album = db.Albums.First(a => a.Title == albumTitle);
                var newTrack = new Track { Title = title, Album = album };
                if (!db.Tracks.ToList().Contains(newTrack))
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
                if (!db.Albums.ToList().Contains(newAlbum))
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
                if (!db.Artists.ToList().Contains(newArtist))
                {
                    db.Add(newArtist);
                    db.SaveChanges();
                }
            }
        }
        #endregion

        #region READ
        public Track GetTrack(string title)
        {
            using (var db = new DecibaseContext()) return db.Tracks.Include(t => t.Album).FirstOrDefault(t => t.Title == title);
        }

        public string GetTrackNumber(string trackTitle)
        {
            var track = GetTrack(trackTitle);
            return track.TrackNumber.ToString();
        }

        public string GetDiscNumber(string trackTitle)
        {
            var track = GetTrack(trackTitle);
            return track.DiscNumber.ToString();
        }

        public string GetTrackGenre(string trackTitle)
        {
            var track = GetTrack(trackTitle);
            return track.Genre;
        }

        public string GetTrackAlbum(string trackTitle)
        {
            var track = GetTrack(trackTitle);
            return track.Album.Title;
        }

        public Album GetAlbum(string title)
        {
            using (var db = new DecibaseContext()) return db.Albums.FirstOrDefault(a => a.Title == title);
        }


        public string GetTotalTracks(string albumTitle)
        {
            var album = GetAlbum(albumTitle);
            return album.TotalTracks.ToString();
        }

        public string GetTotalDiscs(string albumTitle)
        {
            var album = GetAlbum(albumTitle);
            return album.TotalDiscs.ToString();
        }

        public string GetAlbumYear(string albumTitle)
        {
            var album = GetAlbum(albumTitle);
            return album.Year;
        }

        public Artist GetArtist(string name)
        {
            using (var db = new DecibaseContext()) return db.Artists.FirstOrDefault(t => t.Name == name);
        }

       
        public string GetArtistNationality(string artistName)
        {
            var artist = GetArtist(artistName);
            return artist.Nationality;
        }

        public List<string> RetrieveAllTrackTitles()
        {
            using (var db = new DecibaseContext())
            {
                var tracks = db.Tracks.ToList();
                var trackTitles = new List<String>();
                foreach(var track in tracks)
                {
                    trackTitles.Add(track.Title);
                }
                return trackTitles;
            }
        }
        public List<Track> RetrieveAlbumTracks(Album album)
        {
            using (var db = new DecibaseContext()) return db.Tracks.Where(t => t.AlbumId == album.AlbumId).ToList();
        }

        public List<string> RetrieveAlbumTrackTitles(string albumTitle)
        {
            using (var db = new DecibaseContext())
            {
                var albumTracks = RetrieveAlbumTracks(GetAlbum(albumTitle));
                var albumTrackTitles = new List<string>();
                foreach (var track in albumTracks)
                {
                    albumTrackTitles.Add(track.Title);
                }
                return albumTrackTitles;
            }
        }

        public List<Track> RetrieveArtistTracks(Artist artist)
        {
            using (var db = new DecibaseContext())
            {
                var artistIncludingTracks = db.Artists.Include(a => a.Tracks).ThenInclude(ta => ta.Track).ThenInclude(t => t.Album).First(a => a.ArtistId == artist.ArtistId);
                return artistIncludingTracks.Tracks.Select(ta => ta.Track).ToList();
            }
        }

        public List<string> RetrieveArtistTrackTitles(string artistName)
        {
            using (var db = new DecibaseContext())
            {
                var artistTracks = RetrieveArtistTracks(GetArtist(artistName));
                var artistTrackTitles = new List<string>();
                foreach (var track in artistTracks)
                {
                    artistTrackTitles.Add(track.Title);
                }
                return artistTrackTitles;
            }
        }

        public List<string> RetrieveAllAlbumTitles()
        {
            using (var db = new DecibaseContext())
            {
                var albums = db.Albums.ToList();
                var albumTitles = new List<String>();
                foreach (var album in albums)
                {
                    albumTitles.Add(album.Title);
                }
                return albumTitles;
            }
        }

        public List<Album> RetrieveArtistAlbums(Artist artist)
        {
            using (var db = new DecibaseContext())
            {
                var artistAlbums = new List<Album>();
                foreach (var track in RetrieveArtistTracks(artist))
                {
                    if (!artistAlbums.Contains(track.Album)) artistAlbums.Add(track.Album);
                }
                return artistAlbums;
            }
        }

        public List<string> RetrieveArtistAlbumTitles(string artistName)
        {
            using (var db = new DecibaseContext())
            {
                var artistAlbums = RetrieveArtistAlbums(GetArtist(artistName));
                var artistAlbumTitles = new List<string>();
                foreach (var track in artistAlbums)
                {
                    artistAlbumTitles.Add(track.Title);
                }
                return artistAlbumTitles;
            }
        }

        public List<string> RetrieveAllArtistNames()
        {
            using(var db = new DecibaseContext())
            {
                var artists = db.Artists.ToList();
                var artistNames = new List<String>();
                foreach (var artist in artists)
                {
                    artistNames.Add(artist.Name);
                }
                return artistNames;
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

        public List<string> RetrieveTrackArtistsNames(string trackTitle)
        {
            using (var db = new DecibaseContext())
            {
                var trackArtists = RetrieveTrackArtists(GetTrack(trackTitle));
                var trackArtistsnames = new List<string>();
                foreach (var track in trackArtists)
                {
                    trackArtistsnames.Add(track.Name);
                }
                return trackArtistsnames;
            }

        }

        public List<Artist> RetrieveAlbumArtists(Album album)
        {
            using (var db = new DecibaseContext())
            {
                var albumArtists = new List<Artist>();
                foreach(var track in album.Tracks)
                {
                    foreach(var artist in RetrieveTrackArtists(track))
                    {
                        if (!albumArtists.Contains(artist)) albumArtists.Add(artist);
                    }
                }
                return albumArtists;
            }
        }

        public List<string> RetrieveAlbumArtistsNames(string albumTitle)
        {
            using (var db = new DecibaseContext())
            {
                var albumArtists = RetrieveAlbumArtists(GetAlbum(albumTitle));
                var albumArtistsnames = new List<string>();
                foreach (var artist in albumArtists)
                {
                    albumArtistsnames.Add(artist.Name);
                }
                return albumArtistsnames;
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
