using NUnit.Framework;
using Decibase_Model;
using Decibase_Control;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Decibase_Tests
{
    class CRUDManagerTests
    {
        private CRUDManager _CRUDManager = new CRUDManager();

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
                _CRUDManager.AddNewTrack("Lush", "New Energy");
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
                _CRUDManager.AddNewTrack("Lush", "New Energy");
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
                _CRUDManager.AddNewAlbum("New Energy");
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
                _CRUDManager.AddNewAlbum("New Energy");
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
                _CRUDManager.AddNewArtist("Four Tet");
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
                _CRUDManager.AddNewArtist("Four Tet");
                var numArtistsAfter = db.Artists.Count();
                Assert.IsTrue(numArtistsAfter == numArtistsBefore);
            }
        }
        #endregion

        
    }
}
