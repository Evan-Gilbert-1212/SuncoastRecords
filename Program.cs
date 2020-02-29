using System;
using SuncoastRecords.Models;
using ConsoleTools;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SuncoastRecords
{
  class Program
  {
    static void Main(string[] args)
    {
      new ConsoleMenu()
          .Add("Add A Band", () => AddBand())
          .Add("Drop A Band", () => DropBand())
          .Add("Re-Sign A Band", () => ResignBand())
          .Add("Add An Album", () => CreateAlbum())
          .Add("View A Bands Album List", () => ViewBandsAlbums())
          .Add("View All Albums On Label", () => ViewAllAlbums())
          .Add("View Songs On An Album", () => ViewAlbumsSongs())
          .Add("View Signed Bands", () => ViewBands(true, true))
          .Add("View Unsigned Bands", () => ViewBands(false, true))
          .Add("Close", ConsoleMenu.Close)
          .Configure(config =>
          {
            config.Selector = "--> ";
            config.EnableWriteTitle = true;
            config.Title = "+------------------------------+\n"
                         + "| Welcome to Suncoast Records! |\n"
                         + "+------------------------------+\n";
            config.WriteHeaderAction = () => Console.WriteLine("What would you like to do today?");
          })
          .Show();

      Console.WriteLine("+----------------------------+");
      Console.WriteLine("| Have a great day! Goodbye! |");
      Console.WriteLine("+----------------------------+");
    }

    static void AddBand()
    {
      Console.WriteLine("What is the name of the band?");
      var bandName = Console.ReadLine();

      Console.WriteLine("What is the bands country of origin?");
      var countryOfOrigin = Console.ReadLine();

      Console.WriteLine("How many members are in the band?");
      var numberOfMembers = Console.ReadLine();
      var intNumOfMembers = 0;

      while (!Int32.TryParse(numberOfMembers, out intNumOfMembers))
      {
        Console.WriteLine("Please enter a valid number of members. Entry should be a whole number.");
        numberOfMembers = Console.ReadLine().ToLower();
      }

      Console.WriteLine("What is the bands website?");
      var bandWebsite = Console.ReadLine();

      Console.WriteLine("Please enter the style(s) of the band. Type (DONE) when finished adding.");

      var listOfStyles = new List<BandStyle>();

      var newStyle = "";

      while (newStyle != "done")
      {
        if (newStyle != "" && newStyle != "done")
        {
          var styleToAdd = new BandStyle()
          {
            Style = newStyle
          };

          listOfStyles.Add(styleToAdd);
        }

        newStyle = Console.ReadLine();
      }

      Console.WriteLine("Who is the bands contact person?");
      var bandContact = Console.ReadLine();

      Console.WriteLine("What is that persons contact phone number?");
      var contactPhone = Console.ReadLine();

      var suncoastDs = new DatabaseService();

      suncoastDs.AddBand(bandName, countryOfOrigin, intNumOfMembers, bandWebsite, listOfStyles, bandContact, contactPhone);

      Console.WriteLine("New band added successfully!");
      Console.WriteLine();

      Thread.Sleep(3000);
    }

    static void DropBand()
    {
      //Let go a band (update isSigned to false) - DropBand(Band bandToDrop)
      ViewBands(true, false);

      Console.WriteLine("What band do you want to drop? Enter the Band ID.");
      var bandID = Int32.Parse(Console.ReadLine());

      var suncoastDs = new DatabaseService();

      suncoastDs.DropBand(bandID);

      Console.WriteLine("Band has been dropped successfully!");
      Console.WriteLine();

      Thread.Sleep(3000);
    }

    static void ResignBand()
    {
      //Resign a band (update isSigned to true) - ResignBand(Band bandToResign)
      ViewBands(false, false);

      Console.WriteLine("What band do you want to Re-sign? Enter the Band ID.");
      var bandID = Int32.Parse(Console.ReadLine());

      var suncoastDs = new DatabaseService();

      suncoastDs.ResignBand(bandID);

      Console.WriteLine("Band has been re-signed successfully!");
      Console.WriteLine();

      Thread.Sleep(3000);
    }

    static void CreateAlbum()
    {
      ViewAllBands();

      //Produce an album (add an album, and add a few songs to that album) - CreateAlbum(Album albumToCreate, List<Song> songsToAdd)
      Console.WriteLine("What band are you adding an album for? Enter the Band ID.");
      var bandID = Int32.Parse(Console.ReadLine());

      Console.WriteLine("What is the title of the album?");
      var albumTitle = Console.ReadLine();

      Console.WriteLine("Does this album have explicit language? (YES) or (NO)");
      var isExplicit = Console.ReadLine().ToLower();

      while (isExplicit != "yes" && isExplicit != "no")
      {
        Console.WriteLine("Please enter a valid response. Valid responses are (YES) or (NO).");
        isExplicit = Console.ReadLine();
      }

      var boolExplicit = isExplicit == "yes";

      var addASong = true;

      var albumSongs = new List<Song>();

      Console.WriteLine("Adding songs to the album...");
      Console.WriteLine();

      while (addASong)
      {
        Console.WriteLine("What is the name of the song?");
        var songName = Console.ReadLine();

        Console.WriteLine("What are the lyrics to the song?");
        var songLyrics = Console.ReadLine();

        Console.WriteLine("What is the duration of the song?");
        var songDuration = Console.ReadLine();

        var newSong = new Song()
        {
          Title = songName,
          Lyrics = songLyrics,
          Length = songDuration
        };

        Console.WriteLine("Please enter the genre(s) of the song. Type (DONE) when finished adding.");

        var newGenre = "";

        while (newGenre != "done")
        {
          if (newGenre != "" && newGenre != "done")
          {
            var genreToAdd = new SongGenre()
            {
              Genre = newGenre
            };

            newSong.SongGenres.Add(genreToAdd);
          }

          newGenre = Console.ReadLine();
        }

        albumSongs.Add(newSong);

        Console.WriteLine("Would you like to add another song? (YES) or (NO).");

        var addAnotherSong = Console.ReadLine().ToLower();

        while (addAnotherSong != "yes" && addAnotherSong != "no")
        {
          Console.WriteLine("Please enter a valid response. Valid responses are (YES) or (NO).");
          addAnotherSong = Console.ReadLine();
        }

        if (addAnotherSong == "no")
          addASong = false;
      }

      var suncoastDs = new DatabaseService();

      suncoastDs.CreateAlbum(bandID, albumTitle, boolExplicit, albumSongs);

      Console.WriteLine("New album added successfully!");
      Console.WriteLine();

      Thread.Sleep(3000);
    }

    static void ViewBandsAlbums()
    {
      //View all albums for a band - ViewBandsAlbums(Band bandToView)
      var suncoastDb = new DatabaseContext();

      ViewAllBands();

      Console.WriteLine("What band would you like to view albums for? Please enter the Band ID.");
      var bandID = Int32.Parse(Console.ReadLine());

      //var bandToDisplay = suncoastDb.Bands.First(b => b.ID == bandID);
      var bandToDisplay = suncoastDb.Bands.Include(band => band.Albums).First(b => b.ID == bandID);

      Console.WriteLine($"Current albums for {bandToDisplay.Name}:");
      Console.WriteLine("+-----------------------------------------------------------------+");
      Console.WriteLine("| Album Name             | Explicit Lyrics | Release Date         |");
      Console.WriteLine("+-----------------------------------------------------------------+");

      foreach (var album in bandToDisplay.Albums)
      {
        var formatAlbumName = String.Format("{0,-22}", album.Title);

        var formatExplicit = "";

        if (album.IsExplicit)
          formatExplicit = String.Format("{0,-15}", "YES");
        else
          formatExplicit = String.Format("{0,-15}", "NO");

        var formatReleaseDate = String.Format("{0,-20}", album.ReleaseDate);

        Console.WriteLine($"| {formatAlbumName} | {formatExplicit} | {formatReleaseDate} |");
      }

      Console.WriteLine("+-----------------------------------------------------------------+");
      Console.WriteLine();

      Console.WriteLine("Press (ENTER) to return to the main menu.");
      Console.ReadLine();
    }

    static void ViewAllAlbums()
    {
      //View all albums, ordered by ReleaseDate - ViewAllAlbums()
      var suncoastDb = new DatabaseContext();

      Console.WriteLine($"Current albums in the system:");
      Console.WriteLine("+------------------------------------------------------------------------------------------+");
      Console.WriteLine("| Band Name              | Album Name             | Explicit Lyrics | Release Date         |");
      Console.WriteLine("+------------------------------------------------------------------------------------------+");

      var bandsToDisplay = suncoastDb.Bands.Include(band => band.Albums);

      foreach (var band in bandsToDisplay)
      {
        foreach (var album in band.Albums.OrderBy(album => album.ReleaseDate))
        {
          var formatBandName = String.Format("{0,-22}", band.Name);

          var formatAlbumName = String.Format("{0,-22}", album.Title);

          var formatExplicit = "";

          if (album.IsExplicit)
            formatExplicit = String.Format("{0,-15}", "YES");
          else
            formatExplicit = String.Format("{0,-15}", "NO");

          var formatReleaseDate = String.Format("{0,-20}", album.ReleaseDate);

          Console.WriteLine($"| {formatBandName} | {formatAlbumName} | {formatExplicit} | {formatReleaseDate} |");
        }
      }

      Console.WriteLine("+------------------------------------------------------------------------------------------+");
      Console.WriteLine();

      Console.WriteLine("Press (ENTER) to return to the main menu.");
      Console.ReadLine();
    }

    static void ViewAlbumsSongs()
    {
      //View an albums songs - ViewAlbumsSongs(Album albumToView)
      ViewBandsAndAlbums();

      Console.WriteLine("What album would you like to view songs for? Please enter the Album ID.");
      var albumID = Int32.Parse(Console.ReadLine());

      var suncoastDb = new DatabaseContext();

      Console.WriteLine($"Current songs in the album:");
      Console.WriteLine("+--------------------------------------------------------------------------+");
      Console.WriteLine("| Band Name              | Album Name             | Song                   |");
      Console.WriteLine("+--------------------------------------------------------------------------+");

      var bandsToDisplay = suncoastDb.Bands
                            .Include(band => band.Albums)
                            .ThenInclude(album => album.Songs);

      foreach (var band in bandsToDisplay)
      {
        foreach (var album in band.Albums.Where(album => album.ID == albumID))
        {
          foreach (var song in album.Songs)
          {
            var formatBandName = String.Format("{0,-22}", band.Name);

            var formatAlbumName = String.Format("{0,-22}", album.Title);

            var formatSongName = String.Format("{0,-22}", song.Title);

            Console.WriteLine($"| {formatBandName} | {formatAlbumName} | {formatSongName} |");
          }
        }
      }

      Console.WriteLine("+--------------------------------------------------------------------------+");
      Console.WriteLine();

      Console.WriteLine("Press (ENTER) to return to the main menu.");
      Console.ReadLine();
    }

    static void ViewBands(bool isSigned, bool pause)
    {
      var suncoastDb = new DatabaseContext();

      if (isSigned)
        Console.WriteLine("Signed Bands in the system:");
      else
        Console.WriteLine("Un-signed Bands in the system:");

      Console.WriteLine("+-------------------------------------------------------+");
      Console.WriteLine("| Band ID | Band Name             | Country of Origin   |");
      Console.WriteLine("+-------------------------------------------------------+");

      var bandsToDisplay = suncoastDb.Bands.Where(s => s.IsSigned == isSigned);

      foreach (var band in bandsToDisplay)
      {
        var formatBandID = String.Format("{0,-7}", band.ID);
        var formatBandName = String.Format("{0,-21}", band.Name);
        var formatCountry = String.Format("{0,-19}", band.CountryOfOrigin);

        Console.WriteLine($"| {formatBandID} | {formatBandName} | {formatCountry} |");
      }

      Console.WriteLine("+-------------------------------------------------------+");
      Console.WriteLine();

      if (pause)
      {
        Console.WriteLine("Press (ENTER) to return to the main menu.");
        Console.ReadLine();
      }
    }

    static void ViewAllBands()
    {
      var suncoastDb = new DatabaseContext();

      Console.WriteLine("Current Bands in the system:");
      Console.WriteLine("+-------------------------------------------------------+");
      Console.WriteLine("| Band ID | Band Name             | Country of Origin   |");
      Console.WriteLine("+-------------------------------------------------------+");

      foreach (var band in suncoastDb.Bands)
      {
        var formatBandID = String.Format("{0,-7}", band.ID);
        var formatBandName = String.Format("{0,-21}", band.Name);
        var formatCountry = String.Format("{0,-19}", band.CountryOfOrigin);

        Console.WriteLine($"| {formatBandID} | {formatBandName} | {formatCountry} |");
      }

      Console.WriteLine("+-------------------------------------------------------+");
      Console.WriteLine();
    }

    static void ViewBandsAndAlbums()
    {
      var suncoastDb = new DatabaseContext();

      Console.WriteLine($"Current albums in the system:");
      Console.WriteLine("+------------------------------------------------------------+");
      Console.WriteLine("| Band Name              | Album Name             | Album ID |");
      Console.WriteLine("+------------------------------------------------------------+");

      var bandsToDisplay = suncoastDb.Bands.Include(band => band.Albums);

      foreach (var band in bandsToDisplay)
      {
        foreach (var album in band.Albums)
        {
          var formatBandName = String.Format("{0,-22}", band.Name);

          var formatAlbumName = String.Format("{0,-22}", album.Title);

          var formatAlbumID = String.Format("{0,-8}", album.ID);

          Console.WriteLine($"| {formatBandName} | {formatAlbumName} | {formatAlbumID} |");
        }
      }

      Console.WriteLine("+------------------------------------------------------------+");
      Console.WriteLine();
    }
  }
}
