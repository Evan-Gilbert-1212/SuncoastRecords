using System.Collections.Generic;

namespace SuncoastRecords.Models
{
  public class Band
  {
    public int ID { get; set; }
    public string Name { get; set; }
    public string CountryOfOrigin { get; set; }
    public int NumberOfMembers { get; set; }
    public string Website { get; set; }
    public bool IsSigned { get; set; } = true;
    public string ContactPerson { get; set; }
    public string ContactPhoneNumber { get; set; }

    public List<Album> Albums { get; set; } = new List<Album>();
    public List<BandStyle> BandStyles { get; set; } = new List<BandStyle>();
    public List<BandMusician> BandMusicians { get; set; } = new List<BandMusician>();

  }
}