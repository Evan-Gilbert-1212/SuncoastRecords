using System.Collections.Generic;

namespace SuncoastRecords.Models
{
  public class Song
  {
    public int ID { get; set; }
    public string Title { get; set; }
    public string Lyrics { get; set; }
    public string Length { get; set; }
    public int AlbumID { get; set; }

    public List<SongGenre> SongGenres { get; set; } = new List<SongGenre>();

  }
}