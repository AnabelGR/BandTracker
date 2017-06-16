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
    public void Dispose()
    {
      Concert.DeleteAll();
      Band.DeleteAll();
      Venue.DeleteAll();
    }
  }
}
