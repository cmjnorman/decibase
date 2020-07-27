using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Decibase_DbLayer
{
    public class DecibaseContext : DbContext
    {
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Decibase;");

    }

    public class Track
    {
        public int TrackId { get; set; }
        public string Title { get; set; }
        public int TrackNumber { get; set; }
        public int DiscNumber { get; set; }
        public string Genre { get; set; }

        public int AlbumId { get; set; }
        public Album Album { get; set; }
        
        public virtual ICollection<Artist> Features { get; } = new List<Artist>();
        public virtual ICollection<Artist> Artists { get; } = new List<Artist>();
    }

    public class Album
    {
        public int AlbumId { get; set; }
        public string Name { get; set; }
        public int TotalTracks { get; set; }
        public int TotalDiscs { get; set; }
        public string Year { get; set; }
        public virtual ICollection<Track> Tracks { get; } = new List<Track>();
    }

    public class Artist
    {
        public int ArtistId { get; set; }
        public string Name { get; set; }
        public string Nationality { get; set; }
        public virtual ICollection<Track> Tracks { get; } = new List<Track>();
    }
}
