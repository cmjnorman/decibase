using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Decibase_DbLayer
{
    public class DecibaseContext : DbContext
    {
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<TrackArtist> TrackArtists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Decibase;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TrackArtist>()
                .HasKey(ta => new { ta.TrackId, ta.ArtistId });
            modelBuilder.Entity<TrackArtist>()
                .HasOne(ta => ta.Track)
                .WithMany(t => t.Artists)
                .HasForeignKey(ta => ta.TrackId);
            modelBuilder.Entity<TrackArtist>()
                .HasOne(ta => ta.Artist)
                .WithMany(a => a.Tracks)
                .HasForeignKey(ta => ta.ArtistId);
        }
    }
}