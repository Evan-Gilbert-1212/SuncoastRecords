using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SuncoastRecords.Models
{
  public partial class DatabaseContext : DbContext
  {
    public DbSet<Band> Bands { get; set; }

    public DbSet<Album> Albums { get; set; }

    public DbSet<Song> Songs { get; set; }

    public DbSet<BandStyle> BandStyles { get; set; }

    public DbSet<SongGenre> SongGenres { get; set; }

    public DbSet<Musician> Musicians { get; set; }

    public DbSet<BandMusician> BandMusicians { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        optionsBuilder.UseNpgsql("server=localhost;database=SuncoastRecords");
      }
    }
  }
}
