using System;
using System.Collections.Generic;

namespace SuncoastRecords.Models
{
  public class Album
  {
    public int ID { get; set; }
    public string Title { get; set; }
    public bool IsExplicit { get; set; }
    public DateTime ReleaseDate { get; set; } = DateTime.Now;
    public int BandID { get; set; }

    public List<Song> Songs { get; set; } = new List<Song>();

  }
}