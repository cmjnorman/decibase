using NUnit.Framework;
using Decibase_Model;
using Decibase_Control;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Decibase_Tests
{
    class CRUDManagerTests
    {
        private CRUDManager cm = new CRUDManager();

        #region CreateTests
        [Test]
        public void CallingAddNewTrackAddsTrackToDatabase()
        {
            var newTrack = new Track { Title = "Lush" };
            using (var db = new DecibaseContext())
            {
                //Delete pre-exisiting instance of track (if it exists)
                var dbTrack = db.Tracks.FirstOrDefault(a => a.Title == newTrack.Title);
                if (dbTrack != null)
                {
                    db.Tracks.Remove(dbTrack);
                    db.SaveChanges();
                }

                var numTracksBefore = db.Tracks.Count();
                cm.AddNewTrack("Lush", "New Energy");
                var numTracksAfter = db.Tracks.Count();
                Assert.IsTrue(numTracksAfter == numTracksBefore + 1);
            }
        }

        [Test]
        public void IfTrackAlreadyExisitsCallingAddNewTrackDoesNotAddTrackToDatabase()
        {
            using (var db = new DecibaseContext())
            {
                var numTracksBefore = db.Tracks.Count();
                cm.AddNewTrack("Lush", "New Energy");
                var numTracksAfter = db.Tracks.Count();
                Assert.IsTrue(numTracksAfter == numTracksBefore);
            }
        }

        [Test]
        public void CallingAddNewAlbumAddsAlbumToDatabase()
        {
            var newAlbum = new Album { Title = "New Energy" };
            using (var db = new DecibaseContext())
            {
                //Delete pre-exisiting instance of album (if it exists)
                var dbAlbum = db.Albums.FirstOrDefault(a => a.Title == newAlbum.Title);
                if (dbAlbum != null)
                {
                    db.Albums.Remove(dbAlbum);
                    db.SaveChanges();
                }

                var numAlbumsBefore = db.Albums.Count();
                cm.AddNewAlbum("New Energy");
                var numAlbumsAfter = db.Albums.Count();
                Assert.IsTrue(numAlbumsAfter == numAlbumsBefore + 1);
            } 
        }

        [Test]
        public void IfAlbumAlreadyExistsCallingAddNewAlbumDoesNotAddAlbumToDatabase()
        {
            using (var db = new DecibaseContext())
            {
                var numAlbumsBefore = db.Albums.Count();
                cm.AddNewAlbum("New Energy");
                var numAlbumsAfter = db.Albums.Count();
                Assert.IsTrue(numAlbumsAfter == numAlbumsBefore);
            }
        }

        [Test]
        public void CallingAddNewArtistAddsArtistToDatabase()
        {
            var newArtist = new Artist { Name = "Four Tet" };
            using (var db = new DecibaseContext())
            {
                //Delete pre-exisiting instance of artist (if it exists)
                var dbArtist = db.Artists.FirstOrDefault(a => a.Name == newArtist.Name);
                if (dbArtist != null)
                {
                    db.Artists.Remove(dbArtist);
                    db.SaveChanges();
                }

                var numArtistsBefore = db.Artists.Count();
                cm.AddNewArtist("Four Tet");
                var numArtistsAfter = db.Artists.Count();
                Assert.IsTrue(numArtistsAfter == numArtistsBefore + 1);
            }
        }

        [Test]
        public void IfArtistAlreadyExistsCallingAddNewArtistDoesNotAddArtistToDatabase()
        {
            using (var db = new DecibaseContext())
            {
                var numArtistsBefore = db.Artists.Count();
                cm.AddNewArtist("Four Tet");
                var numArtistsAfter = db.Artists.Count();
                Assert.IsTrue(numArtistsAfter == numArtistsBefore);
            }
        }
        #endregion

        #region ReadTests
        [Test]
        public void CallingRetrieveAlbumTracksReturnsCorrectList()
        {
            cm.AddNewAlbum("Bicep");
            cm.AddNewTrack("Opal", "Bicep");
            cm.AddNewTrack("Glue", "Bicep");
            cm.AddNewTrack("Aura", "Bicep");

            var album = cm.RetrieveAlbum("Bicep");
            var tracklist = new List<Track>() { new Track { Title = "Opal" }, new Track { Title = "Glue" }, new Track { Title = "Aura" } };

            Assert.AreEqual(cm.RetrieveAlbumTracks(album), tracklist);
        }

        [Test]
        public void CallingRetrieveArtistTracksReturnsCorrectList()
        {
            cm.AddNewArtist("Bicep");
            var artist = cm.RetrieveArtist("Bicep");
            cm.JoinTrackToArtist(cm.RetrieveTrack("Opal"), artist);
            cm.JoinTrackToArtist(cm.RetrieveTrack("Glue"), artist);
            cm.JoinTrackToArtist(cm.RetrieveTrack("Aura"), artist);

            var trackTitles = new List<string>() { "Opal", "Glue", "Aura"  };

            var artistTracks = cm.RetrieveArtistTracks(artist);
            var artistTrackTitles = new List<string>();
            foreach(var track in artistTracks)
            {
                artistTrackTitles.Add(track.Title);
            }
            Assert.AreEqual(artistTrackTitles, trackTitles);
        }
        #endregion

        #region UpdateTests

        [Test]
        public void CallingTrackSetTitleSetsTitle()
        {
            cm.AddNewTrack("Do I Wanna Know", "FM");
            var track = cm.RetrieveTrack("Do I Wanna Know");
            cm.TrackSetTitle(track, "Do I Wanna Know?");
            Assert.AreEqual(track.Title, "Do I Wanna Know?");
        }

        [Test]
        public void CallingTrackSetTrackNumberSetsTrackNumber()
        {
            cm.AddNewTrack("Do I Wanna Know?", "FM");
            var track = cm.RetrieveTrack("Do I Wanna Know?");
            cm.TrackSetTrackNumber(track, 1);
            Assert.AreEqual(track.TrackNumber, 1);
        }

        [Test]
        public void CallingTrackSetDiscNumberSetsDiscNumber()
        {
            cm.AddNewTrack("Do I Wanna Know?", "FM");
            var track = cm.RetrieveTrack("Do I Wanna Know?");
            cm.TrackSetDiscNumber(track, 1);
            Assert.AreEqual(track.DiscNumber, 1);
        }

        [Test]
        public void CallingTrackSetGenreSetsGenre()
        {
            cm.AddNewTrack("Do I Wanna Know?", "FM");
            var track = cm.RetrieveTrack("Do I Wanna Know?");
            cm.TrackSetGenre(track, "Indie Rock");
            Assert.AreEqual(track.Genre, "Indie Rock");
        }

        [Test]
        public void CallingTrackSetAlbumSetsAlbum()
        {
            cm.AddNewTrack("Do I Wanna Know?", "FM");
            cm.AddNewAlbum("AM");
            var track = cm.RetrieveTrack("Do I Wanna Know?");
            var album = cm.RetrieveAlbum("AM");
            cm.TrackSetAlbum(track, album);
            Assert.AreEqual(track.Album, album);
        }

        [Test]
        public void CallingJoinTrackToArtistJoinsTrackToArtist()
        {
            cm.AddNewTrack("Do I Wanna Know?", "AM");
            cm.AddNewArtist("Arctic Monkeys");
            var track = cm.RetrieveTrack("Do I Wanna Know?");
            var artist = cm.RetrieveArtist("Arctic Monkeys");
            cm.JoinTrackToArtist(track, artist);
            Assert.AreEqual(cm.RetrieveTrackArtists(track)[0], artist);
        }

        [Test]
        public void CallingUnjoinTrackFromArtistUnjoinsTrackFromArtist()
        {
            var track = cm.RetrieveTrack("Do I Wanna Know?");
            var artist = cm.RetrieveArtist("Arctic Monkeys");
            var artistCountBefore = cm.RetrieveTrackArtists(track).Count;
            cm.UnjoinTrackFromArtist(track, artist);
            var artistCountAfter = cm.RetrieveTrackArtists(track).Count;
            Assert.IsTrue(artistCountBefore == artistCountAfter + 1);
        }

        [Test]
        public void CallingAlbumSetTitleSetsTitle()
        {
            cm.AddNewAlbum("Homework");
            var album = cm.RetrieveAlbum("Homework");
            cm.AlbumSetTitle(album, "Discovery");
            Assert.AreEqual(album.Title, "Discovery");
        }

        [Test]
        public void CallingAlbumSetTotalTracksSetsTotalTracks()
        {
            cm.AddNewAlbum("Discovery");
            var album = cm.RetrieveAlbum("Discovery");
            cm.AlbumSetTotalTracks(album, 14);
            Assert.AreEqual(album.TotalTracks, 14);
        }

        [Test]
        public void CallingAlbumSetTotalDiscsSetsTotalDiscs()
        {
            cm.AddNewAlbum("Discovery");
            var album = cm.RetrieveAlbum("Discovery");
            cm.AlbumSetTotalDiscs(album, 1);
            Assert.AreEqual(album.TotalDiscs, 1);
        }

        [Test]
        public void CallingAlbumSetYearSetsYear()
        {
            cm.AddNewAlbum("Discovery");
            var album = cm.RetrieveAlbum("Discovery");
            cm.AlbumSetYear(album, "2001");
            Assert.AreEqual(album.Year, "2001");
        }

        [Test]
        public void CallingArtistSetNameSetsName()
        {
            var artist = cm.RetrieveArtist("Arctic Monkeys");
            cm.ArtistSetName(artist, "Antartic Monkeys");
            Assert.AreEqual(artist.Name, "Antartic Monkeys");
        }

        [Test]
        public void CallingArtistSetNationalitySetsNationality()
        {
            var artist = cm.RetrieveArtist("Arctic Monkeys");
            cm.ArtistSetNationality(artist, "British");
            Assert.AreEqual(artist.Nationality, "British");
        }
        #endregion

        #region DeleteTests
        [Test]
        public void CallingDeleteTrackDeletesTrack()
        {
            cm.AddNewTrack("Tondo", "Ecstacy");
            var track = cm.RetrieveTrack("Tondo");
            cm.DeleteTrack(track);
            Assert.IsNull(cm.RetrieveTrack("Tondo"));
        }

        [Test]
        public void CallingDeleteAlbumDeletesAlbum()
        {
            cm.AddNewAlbum("Ecstacy");
            var album = cm.RetrieveAlbum("Ecstacy");
            cm.DeleteAlbum(album);
            Assert.IsNull(cm.RetrieveAlbum("Ecstacy"));
        }

        [Test]
        public void CallingDeleteArtistDeletesArtist()
        {
            cm.AddNewArtist("Disclosure");
            var artist = cm.RetrieveArtist("Disclosure");
            cm.DeleteArtist(artist);
            Assert.IsNull(cm.RetrieveArtist("Disclosure"));
        }

        #endregion
    }
}
