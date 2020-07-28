using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Decibase_Model
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new DecibaseContext())
            {
                ////CREATE

                ////db.Add(new Album { Name = "Album 1" });
                ////db.SaveChanges();

                ////var album = db.Albums.Where(a => a.Name == "Album 1").FirstOrDefault();

                ////db.Add(new Track { Title = "Song 1", Album = album });
                ////db.Add(new Track { Title = "Song 2", Album = album });
                ////db.Add(new Artist { Name = "Artist 1" });
                ////db.Add(new Artist { Name = "Artist 2" });
                ////db.SaveChanges();

                ////var track1 = db.Tracks.Where(t => t.Title == "song 1").FirstOrDefault();
                ////var track2 = db.Tracks.Where(t => t.Title == "song 2").FirstOrDefault();
                ////var artist1 = db.Artists.Where(a => a.Name == "artist 1").FirstOrDefault();
                ////var artist2 = db.Artists.Where(a => a.Name == "Artist 2").FirstOrDefault();

                ////db.Add(new TrackArtist_Junction { Artist = artist1, Track = track1 });
                ////db.Add(new TrackArtist_Junction { Artist = artist2, Track = track1 });
                ////db.Add(new TrackArtist_Junction { Artist = artist1, Track = track2 });
                ////db.SaveChanges();

                //////READ

                ////var trackIncludingArtists = db.Tracks.Include(t => t.Artists).ThenInclude(ta => ta.Artist).Where(t => t.Title == "Song 1").FirstOrDefault();
                ////var trackArtists = trackIncludingArtists.Artists.Select(ta => ta.Artist);

                ////foreach (var artist in trackArtists)
                ////{
                ////    Console.WriteLine(artist.Name);
                ////}

                ////var artistIncludingTracks = db.Artists.Include(a => a.Tracks).ThenInclude(ta => ta.Track).Where(a => a.Name == "Artist 1").FirstOrDefault();
                ////var artistTracks = artistIncludingTracks.Tracks.Select(ta => ta.Track);

                ////foreach (var track in artistTracks)
                ////{
                ////    Console.WriteLine(track.Title);
                ////}

                ////////UPDATE

                ////track1.TrackNumber = 1;
                ////artist1.Nationality = "French";

                ////Console.WriteLine();


                ////DELETE

                ////db.Albums.Remove(album);
                ////db.Tracks.Remove(track1);
                ////db.Tracks.Remove(track2);
                ////db.Artists.Remove(artist1);
                ////db.Artists.Remove(artist2);

                ////db.TrackArtists.RemoveRange(trackIncludingArtists.Artists);
                ////db.SaveChanges();

                ////db.SaveChanges();
            }


        }
    }
}
