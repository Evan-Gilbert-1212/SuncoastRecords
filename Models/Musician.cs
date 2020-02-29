using System;
using System.Collections.Generic;

namespace SuncoastRecords.Models
{
  public class Musician
  {
    public int ID { get; set; }
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }

    public List<BandMusician> BandMusicians { get; set; } = new List<BandMusician>();
  }
}