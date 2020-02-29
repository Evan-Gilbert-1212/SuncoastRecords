using SuncoastRecords.Models;
using System.Collections.Generic;
using System.Linq;

namespace SuncoastRecords
{
  public class DatabaseService
  {
    public void AddBand(string bandName, string countryOfOrigin, int numberOfMembers, string bandWebsite,
                        List<BandStyle> bandStyles, string bandContact, string contactPhone)
    {
      var bandToAdd = new Band()
      {
        Name = bandName,
        CountryOfOrigin = countryOfOrigin,
        NumberOfMembers = numberOfMembers,
        Website = bandWebsite,
        ContactPerson = bandContact,
        ContactPhoneNumber = contactPhone
      };

      bandToAdd.BandStyles = bandStyles;

      var suncoastDb = new DatabaseContext();

      suncoastDb.Bands.Add(bandToAdd);
      suncoastDb.SaveChanges();
    }

    public void CreateAlbum(int bandID, string albumName, bool explicitLyrics, List<Song> songsToAdd)
    {
      var suncoastDb = new DatabaseContext();

      //Produce an album (add an album, and add a few songs to that album)
      var bandToUpdate = suncoastDb.Bands.FirstOrDefault(band => band.ID == bandID);

      var newAlbum = new Album()
      {
        Title = albumName,
        IsExplicit = explicitLyrics
      };

      bandToUpdate.Albums.Add(newAlbum);

      var albumToAddSongs = bandToUpdate.Albums.First(album => album.Title == albumName);

      foreach (var song in songsToAdd)
        albumToAddSongs.Songs.Add(song);

      suncoastDb.SaveChanges();
    }

    public void DropBand(int bandIDToDrop)
    {
      //Let go a band (update isSigned to false)
      var suncoastDb = new DatabaseContext();

      suncoastDb.Bands.First(b => b.ID == bandIDToDrop).IsSigned = false;

      suncoastDb.SaveChanges();
    }

    public void ResignBand(int bandIDToResign)
    {
      //Resign a band (update isSigned to true)
      var suncoastDb = new DatabaseContext();

      suncoastDb.Bands.First(b => b.ID == bandIDToResign).IsSigned = true;

      suncoastDb.SaveChanges();
    }
  }
}