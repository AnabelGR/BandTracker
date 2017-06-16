using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using BandTracker.Objects;

namespace BandTracker
{
  [Collection("BandTracker")]
  public class ConcertTest : IDisposable
  {
    public ConcertTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void GetAll_GetsConcerts_DatabaseEmpty()
    {
      //Arrange, Act
      int result = Concert.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }
    [Fact]
    public void Equals_ChecksObjectEquality_True()
    {
      //Arrange, Act
      Concert firstConcert = new Concert(4, new DateTime(1987, 01, 01));
      Concert secondConcert = new Concert(4, new DateTime(1987, 01, 01));
      //Assert
      Assert.Equal(firstConcert, secondConcert);
    }
    [Fact]
    public void Save_DoesSaveToDatabase_True()
    {
      //Arrange
      Concert testConcert = new Concert(4, new DateTime(1987, 01, 01));
      testConcert.Save();
      //Act
      List<Concert> result = Concert.GetAll();
      List<Concert> testList = new List<Concert>{testConcert};
      //Assert
      Assert.Equal(testList, result);
    }
    [Fact]
    public void Find_FindsConcertInDatabase_True()
    {
      Concert testConcert = new Concert(4, new DateTime(1987, 01, 01));
      testConcert.Save();

      Concert foundConcert = Concert.Find(testConcert.GetId());

      Assert.Equal(testConcert, foundConcert);
    }
    // [Fact]
    // public void TestBand_AddBandToConcert_True()
    // {
    //   //Arrange
    //   Concert testConcert = new Concert(new DateTime(1987, 01, 01), 4);
    //   testConcert.Save();
    //
    //   Band firstBand = new Band("Star Theater");
    //   Band secondBand = new Band("Fillmore");
    //   firstBand.Save();
    //   secondBand.Save();
    //   //Add
    //   testConcert.AddBand(firstBand);
    //   testConcert.AddBand(secondBand);
    //
    //
    //   List<Band> result = testConcert.GetBands();
    //   List<Band> testList = new List<Band> {firstBand, secondBand};
    //   //Assert
    //   Assert.Equal(testList, result);
    // }
    // [Fact]
    // public void GetBands_ReturnAllConcertsBands_True()
    // {
    //   //Arrange
    //   Concert testConcert = new Concert("Harvey");
    //   testConcert.Save();
    //
    //   Band firstBand = new Band("Paramount Theater");
    //   Band secondBand = new Band("Moda Center");
    //   firstBand.Save();
    //   secondBand.Save();
    //   //Act
    //   testConcert.AddBand(firstBand);
    //   List<Band> savedBands = testConcert.GetBands();
    //   List<Band> testList = new List<Band>{firstBand};
    //   //Assert
    //   Assert.Equal(testList, savedBands);
    // }
    // [Fact]
    // public void Delete_DeletesConcertAssociationsFromDatabase_ConcertList()
    // {
    //   //Arrange
    //   Band testBand = new Band("Roseland");
    //   testBand.Delete();
    //
    //   Concert testConcert = new Concert("GWAR");
    //   testConcert.Save();
    //
    //
    //   //Act
    //   testConcert.AddBand(testBand);
    //   testConcert.Delete();
    //
    //   List<Concert> result = testBand.GetConcerts();
    //   List<Concert> test = new List<Concert> {};
    //
    //   //Assert
    //   Assert.Equal(test, result);
    // }
    // [Fact]
    // public void Update_UpdatesConcertInDatabase_String()
    // {
    //   //Arrange
    //   string name = "ACDC";
    //   Concert testConcert = new Concert(name);
    //   testConcert.Save();
    //   string newName = "The The";
    //
    //   //Act
    //   testConcert.Update(newName);
    //
    //   string result = testConcert.GetName();
    //
    //   //Assert
    //   Assert.Equal(newName, result);
    // }
    public void Dispose()
    {
      Concert.DeleteAll();
      Band.DeleteAll();
      Venue.DeleteAll();
    }
  }
}
