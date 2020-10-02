using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Decibase_Model;
using Microsoft.EntityFrameworkCore;

namespace Decibase_Control
{
    public class CRUDManager
    {
        public string SelectedTrack = "";
        public string SelectedAlbum = "";
        public string SelectedArtist = "";

        public void SelectItem(string selection, string type)
        {
            if (type == "track")
            {
                SelectedTrack = selection;
            }
            else if (type == "album")
            {
                SelectedAlbum = selection;
                SelectedTrack = "";
            }
            else if (type == "artist")
            {
                SelectedArtist = selection;
                SelectedAlbum = "";
                SelectedTrack = "";
            }
        }

        public void DeleteSelection()
        {
            if(SelectedTrack != "")
            {
                DeleteTrack(SelectedTrack);
            }
            else if(SelectedAlbum != "")
            {
                DeleteAlbum(SelectedAlbum);
            }
            else if(SelectedArtist != "")
            {
                DeleteArtist(SelectedArtist);
            }

        }

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

        public string GetTrackNumber(string trackTitle)
        {
            using (var db = new DecibaseContext())
            {
                var track = db.Tracks.FirstOrDefault(t => t.Title == trackTitle);
                return track.TrackNumber;
            }
        }

        public string GetDiscNumber(string trackTitle)
        {
            using (var db = new DecibaseContext())
            {
                var track = db.Tracks.FirstOrDefault(t => t.Title == trackTitle);
                return track.DiscNumber;
            }
        }

        public string GetTrackGenre(string trackTitle)
        {
            using (var db = new DecibaseContext())
            {
                var track = db.Tracks.FirstOrDefault(t => t.Title == trackTitle);
                return track.Genre;
            }
        }

        public string GetTrackAlbum(string trackTitle)
        {
            using (var db = new DecibaseContext())
            {
                var track = db.Tracks.Include(t => t.Album).FirstOrDefault(t => t.Title == trackTitle);
                return track.Album.Title;
            }
        }

        public string GetTotalTracks(string albumTitle)
        {
            using (var db = new DecibaseContext())
            {
                var album = db.Albums.FirstOrDefault(a => a.Title == albumTitle);
                return album.TotalTracks;
            }
        }

        public string GetTotalDiscs(string albumTitle)
        {
            using (var db = new DecibaseContext())
            {
                var album = db.Albums.FirstOrDefault(a => a.Title == albumTitle);
                return album.TotalDiscs;
            }
        }

        public string GetAlbumYear(string albumTitle)
        {
            using (var db = new DecibaseContext())
            {
                var album = db.Albums.FirstOrDefault(a => a.Title == albumTitle);
                return album.Year;
            }
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

        public List<string> RetrieveAlbumTrackTitles(string albumTitle)
        {
            using (var db = new DecibaseContext())
            {
                var albumTracks = db.Tracks.Include(t => t.Album).Where(t => t.Album.Title == albumTitle).ToList();
                var albumTrackTitles = new List<string>();
                foreach (var track in albumTracks)
                {
                    albumTrackTitles.Add(track.Title);
                }
                return albumTrackTitles;
            }
        }

        public List<string> RetrieveArtistTrackTitles(string artistName)
        {
            using (var db = new DecibaseContext())
            {
                var artistTracks = db.Artists.Include(a => a.Tracks).ThenInclude(ta => ta.Track).First(a => a.Name == artistName).Tracks.Select(ta => ta.Track).ToList();
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

        public List<string> RetrieveArtistAlbumTitles(string artistName)
        {
            using (var db = new DecibaseContext())
            {
                var artistTracks = db.Artists.Include(a => a.Tracks).ThenInclude(ta => ta.Track).ThenInclude(t => t.Album).First(a => a.Name == artistName).Tracks.Select(ta => ta.Track).ToList();
                var artistAlbumTitles = new List<string>();
                foreach (var track in artistTracks)
                {
                    if (!artistAlbumTitles.Contains(track.Album.Title)) artistAlbumTitles.Add(track.Album.Title);
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

        public List<string> RetrieveTrackArtistNames(string trackTitle)
        {
            using (var db = new DecibaseContext())
            {
                var trackArtists = db.Tracks.Include(t => t.Artists).ThenInclude(ta => ta.Artist).First(t => t.Title == trackTitle).Artists.Select(ta => ta.Artist).ToList();
                var trackArtistNames = new List<string>();
                foreach(var artist in trackArtists)
                {
                    trackArtistNames.Add(artist.Name);
                }
                return trackArtistNames;
            }
        }

        public List<string> RetrieveAlbumArtistNames(string albumTitle)
        {
            using (var db = new DecibaseContext())
            {
                var album = db.Albums.Include(a => a.Tracks).First(a => a.Title == albumTitle);
                var albumArtists = new List<string>();
                foreach(var track in album.Tracks)
                {
                    var trackArtists = RetrieveTrackArtistNames(track.Title);
                    foreach (var artist in trackArtists)
                    {
                        if (!albumArtists.Contains(artist)) albumArtists.Add(artist);
                    }
                }
                return albumArtists;
            }
        }
        public string ListToString(List<string> list)
        {
            var sb = new StringBuilder();
            if (list.Count > 0) sb.Append(list[0]);
            for (int i = 1; i < list.Count; i++)
            {
                sb.Append($"; {list[i]}");
            }
            return sb.ToString();
        }
        #endregion

        #region UPDATE
        public void TrackSetTitle(string currentTitle, string newTitle)
        {
            using (var db = new DecibaseContext())
            {
                var track = db.Tracks.First(t => t.Title == currentTitle);
                track.Title = newTitle;
                db.SaveChanges();
            }
        }

        public void TrackSetTrackNumber(string trackTitle, string number)
        {
            using (var db = new DecibaseContext())
            {
                var track = db.Tracks.First(t => t.Title == trackTitle);
                track.TrackNumber = number;
                db.SaveChanges();
            }
        }

        public void TrackSetDiscNumber(string trackTitle, string number)
        {
            using (var db = new DecibaseContext())
            {
                var track = db.Tracks.First(t => t.Title == trackTitle);
                track.DiscNumber = number;
                db.SaveChanges();
            }
        }

        public void TrackSetGenre(string trackTitle, string genre)
        {
            using (var db = new DecibaseContext())
            {
                var track = db.Tracks.First(t => t.Title == trackTitle);
                track.Genre = genre;
                db.SaveChanges();
            }
        }

        public void TrackSetAlbum(string trackTitle, string albumTitle)
        {
            using (var db = new DecibaseContext())
            {
                var track = db.Tracks.First(t => t.Title == trackTitle);
                var album = db.Albums.First(a => a.Title == albumTitle);
                track.Album = album;
                db.SaveChanges();
            }
        }

        public void JoinTrackToArtist(string trackTitle, string artistName)
        {
            using (var db = new DecibaseContext())
            {
                var track = db.Tracks.First(t => t.Title == trackTitle);
                var artist = db.Artists.First(a => a.Name == artistName);

                if (db.TrackArtists.FirstOrDefault(ta => ta.TrackId == track.TrackId && ta.ArtistId == artist.ArtistId) == null)
                {
                    var ta = new TrackArtist_Junction() { TrackId = track.TrackId, ArtistId = artist.ArtistId };
                    db.Add(ta);
                    db.SaveChanges();
                }
            }
        }

        public void UnjoinTrackFromArtist(string trackTitle, string artistName)
        {
            using (var db = new DecibaseContext())
            {
                var track = db.Tracks.First(t => t.Title == trackTitle);
                var artist = db.Artists.First(a => a.Name == artistName);

                if (db.TrackArtists.FirstOrDefault(ta => ta.TrackId == track.TrackId && ta.ArtistId == artist.ArtistId) != null)
                {
                    var ta = db.TrackArtists.First(ta => ta.TrackId == track.TrackId && ta.ArtistId == artist.ArtistId);
                    db.Remove(ta);
                    db.SaveChanges();
                    if (RetrieveArtistTrackTitles(artistName).Count == 0)
                    {
                        db.Remove(artist);
                        if(SelectedArtist == artist.Name) SelectedArtist = "";
                    }
                }
            }
        }

        public void AlbumSetTitle(string currentTitle, string newTitle)
        {
            using (var db = new DecibaseContext())
            {
                var album = db.Albums.First(a => a.Title == currentTitle);
                album.Title = newTitle;
                db.SaveChanges();
            }
        }
        public void AlbumSetTotalTracks(string albumTitle, string number)
        {
            using (var db = new DecibaseContext())
            {
                var album = db.Albums.First(a => a.Title == albumTitle);
                album.TotalTracks = number;
                db.SaveChanges();
            }
        }

        public void AlbumSetTotalDiscs(string albumTitle, string number)
        {
            using (var db = new DecibaseContext())
            {
                var album = db.Albums.First(a => a.Title == albumTitle);
                album.TotalDiscs = number;
                db.SaveChanges();
            }
        }

        public void AlbumSetYear(string albumTitle, string year)
        {
            using (var db = new DecibaseContext())
            {
                var album = db.Albums.First(a => a.Title == albumTitle);
                album.Year = year;
                db.SaveChanges();
            }
        }

        public void ArtistSetName(string currentName, string newName)
        {
            using (var db = new DecibaseContext())
            {
                var artist = db.Artists.First(a => a.Name == currentName);
                artist.Name = newName;
                db.SaveChanges();
            }
        }

        #endregion

        #region DELETE
        public void DeleteTrack(string trackTitle)
        {
            using (var db = new DecibaseContext())
            {
                var track = db.Tracks.First(t => t.Title == trackTitle);
                var albumTitle = GetTrackAlbum(trackTitle);
                var album = db.Albums.Include(a => a.Tracks).First(a => a.Title == albumTitle);
                var artistNames = RetrieveTrackArtistNames(trackTitle);
                foreach(var artistName in artistNames)
                {
                    UnjoinTrackFromArtist(trackTitle, artistName);
                }
                db.Remove(track);
                db.SaveChanges();
                if (album.Tracks.Count == 0)
                {
                    db.Remove(album);
                    SelectedAlbum = "";
                }
                db.SaveChanges();
            }
            SelectedTrack = "";
        }

        public void DeleteAlbum(string albumTitle)
        {
            using (var db = new DecibaseContext())
            {
                var albumTracks = RetrieveAlbumTrackTitles(albumTitle);
                foreach(var track in albumTracks)
                {
                    DeleteTrack(track);
                }
                if (albumTracks.Count == 0)
                {
                    var album = db.Albums.First(a => a.Title == albumTitle);
                    db.Remove(album);
                    db.SaveChanges();
                }
            }
            SelectedAlbum = "";
        }

        public void DeleteArtist(string artistName)
        {
            using (var db = new DecibaseContext())
            {
                var artistTracks = RetrieveArtistTrackTitles(artistName);
                foreach (var track in artistTracks)
                {
                    DeleteTrack(track);
                }
                if (artistTracks.Count == 0)
                {
                    var artist = db.Artists.First(a => a.Name == artistName);
                    db.Remove(artist);
                    db.SaveChanges();
                    if (SelectedArtist == artist.Name) SelectedArtist = "";
                }
            }
        }

        public void DeleteUnusedArtists()
        {
            using (var db = new DecibaseContext())
            {
                var artists = RetrieveAllArtistNames();
                var emptyArtists = new List<string>();
                foreach(var artist in artists)
                {
                    if (RetrieveArtistTrackTitles(artist).Count == 0) emptyArtists.Add(artist);
                }
                foreach(var artist in emptyArtists)
                {
                    DeleteArtist(artist);
                }
            }
        }
        #endregion

    }
}
