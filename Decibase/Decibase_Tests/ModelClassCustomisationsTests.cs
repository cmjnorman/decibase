using NUnit.Framework;
using Decibase_Model;

namespace Decibase_Tests
{
    public class ModelClassCustomisationsTests
    {

        #region AlbumTests
        [Test]
        public void AlbumsWithSameNameAndYearAreEqual()
        {
            var album1 = new Album { Title = "10000 Days", Year = "2006" };
            var album2 = new Album { Title = "10000 Days", Year = "2006" };
            Assert.AreEqual(album1, album2);
        }

        [Test]
        public void AlbumsWithSameNameAndDifferentYearsAreNotEqual()
        {
            var album1 = new Album { Title = "10000 Days", Year = "2006" };
            var album2 = new Album { Title = "10000 Days", Year = "2007" };
            Assert.AreNotEqual(album1, album2);
        }

        [Test]
        public void AlbumsWithSameNameAndOneWithNullYearAreEqual()
        {
            var album1 = new Album { Title = "10000 Days", Year = "2006" };
            var album2 = new Album { Title = "10000 Days" };
            Assert.AreEqual(album1, album2);
        }

        [Test]
        public void AlbumsWithSameNameAndBothWithNullYearAreEqual()
        {
            var album1 = new Album { Title = "10000 Days" };
            var album2 = new Album { Title = "10000 Days" };
            Assert.AreEqual(album1, album2);
        }

        [Test]
        public void AlbumsDifferentNameAreNotEqual()
        {
            var album1 = new Album { Title = "10000 Days" };
            var album2 = new Album { Title = "Lateralus" };
            Assert.AreNotEqual(album1, album2);
        }
        #endregion

        #region ArtistTests

        [Test]
        public void ArtistsWithDifferentNameAreNotEqual()
        {
            var artist1 = new Artist { Name = "Noisia" };
            var artist2 = new Artist { Name = "Mefjus" };
            Assert.AreNotEqual(artist1, artist2);
        }
        #endregion

        #region TrackTests
        [Test]
        public void TracksWithSameNameAlbumAndArtistAreEqual()
        {
            var artist = new Artist { Name = "Caravan Palace" };
            var album = new Album { Title = "Panic", Year = "2012" };
            var track1 = new Track { Title = "Cotton Heads", Album = album };
            var track2 = new Track { Title = "Cotton Heads", Album = album };
            var trackArtistJunction1 = new TrackArtist_Junction { Track = track1, Artist = artist };
            var trackArtistJunction2 = new TrackArtist_Junction { Track = track2, Artist = artist };
            track1.Artists.Add(trackArtistJunction1);
            track2.Artists.Add(trackArtistJunction2);
            Assert.AreEqual(track1, track2);
        }

        [Test]
        public void TracksWithSameNameAlbumAndDifferentArtistAreNotEqual()
        {
            var artist1 = new Artist { Name = "Caravan Palace" };
            var artist2 = new Artist { Name = "Parov Stellar" };
            var album = new Album { Title = "Panic", Year = "2012" };
            var track1 = new Track { Title = "Cotton Heads", Album = album };
            var track2 = new Track { Title = "Cotton Heads", Album = album };
            var trackArtistJunction1 = new TrackArtist_Junction { Track = track1, Artist = artist1 };
            var trackArtistJunction2 = new TrackArtist_Junction { Track = track2, Artist = artist2 };
            track1.Artists.Add(trackArtistJunction1);
            track2.Artists.Add(trackArtistJunction2);
            Assert.AreNotEqual(track1, track2);
        }

        [Test]
        public void TracksWithSameNameArtistAndDifferentAlbumAreNotEqual()
        {
            var artist = new Artist { Name = "Caravan Palace" };
            var album1 = new Album { Title = "Panic", Year = "2012" };
            var album2 = new Album { Title = "Chronologic", Year = "2019" };
            var track1 = new Track { Title = "Cotton Heads", Album = album1 };
            var track2 = new Track { Title = "Cotton Heads", Album = album2 };
            var trackArtistJunction1 = new TrackArtist_Junction { Track = track1, Artist = artist };
            var trackArtistJunction2 = new TrackArtist_Junction { Track = track2, Artist = artist };
            track1.Artists.Add(trackArtistJunction1);
            track2.Artists.Add(trackArtistJunction2);
            Assert.AreNotEqual(track1, track2);
        }

        [Test]
        public void TracksWithSameNameAlbumAndNullArtistAreEqual()
        {
            var album = new Album { Title = "Panic", Year = "2012" };
            var track1 = new Track { Title = "Cotton Heads", Album = album };
            var track2 = new Track { Title = "Cotton Heads", Album = album };
            Assert.AreEqual(track1, track2);
        }

        [Test]
        public void TracksWithSameNameArtistAndNullAlbumAreEqual()
        {
            var artist = new Artist { Name = "Caravan Palace" };
            var track1 = new Track { Title = "Cotton Heads" };
            var track2 = new Track { Title = "Cotton Heads" };
            var trackArtistJunction1 = new TrackArtist_Junction { Track = track1, Artist = artist };
            var trackArtistJunction2 = new TrackArtist_Junction { Track = track2, Artist = artist };
            track1.Artists.Add(trackArtistJunction1);
            track2.Artists.Add(trackArtistJunction2);
            Assert.AreEqual(track1, track2);
        }

        [Test]
        public void TracksWithDifferentNameAreNotEqual()
        {
            var track1 = new Track { Title = "Clash" };
            var track2 = new Track { Title = "Cotton Heads" };
            Assert.AreNotEqual(track1, track2);
        }
        #endregion
    }
}