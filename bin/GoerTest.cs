using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using BandTracker.Objects;

namespace BandTracker
{
  [Collection("BandTracker")]
  public class GoerTest : IDisposable
  {
    public GoerTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void GetAll_GetsGoers_DatabaseEmpty()
    {
      //Arrange, Act
      int result = Goer.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }
    [Fact]
    public void Equals_ChecksObjectEquality_True()
    {
      //Arrange, Act
      Goer firstGoer = new Goer("Eric");
      Goer secondGoer = new Goer("Eric");
      //Assert
      Assert.Equal(firstGoer, secondGoer);
    }
    [Fact]
    public void Save_DoesSaveToDatabase_True()
    {
      //Arrange
      Goer testGoer = new Goer("Tom");
      testGoer.Save();
      //Act
      List<Goer> result = Goer.GetAll();
      List<Goer> testList = new List<Goer>{testGoer};
      //Assert
      Assert.Equal(testList, result);
    }
    [Fact]
    public void Find_FindsGoerInDatabase_True()
    {
      Goer testGoer = new Goer("Mike");
      testGoer.Save();

      Goer foundGoer = Goer.Find(testGoer.GetId());

      Assert.Equal(testGoer, foundGoer);
    }
    [Fact]
    public void TestConcert_AddConcertToGoer_True()
    {
      //Arrange
      Goer testGoer = new Goer("Mike");
      testGoer.Save();

      Concert firstConcert = new Concert(4, new DateTime(1987, 01, 01));
      Concert secondConcert = new Concert(1, new DateTime(2007, 01, 01));
      firstConcert.Save();
      secondConcert.Save();
      //Add
      testGoer.AddConcert(firstConcert);
      testGoer.AddConcert(secondConcert);


      List<Concert> result = testGoer.GetConcerts();
      List<Concert> testList = new List<Concert> {firstConcert, secondConcert};
      //Assert
      Assert.Equal(testList, result);
    }
    [Fact]
    public void GetConcerts_ReturnAllGoersConcerts_True()
    {
      //Arrange
      Goer testGoer = new Goer("Harvey");
      testGoer.Save();

      Concert firstConcert = new Concert(4, new DateTime(1987, 01, 01));
      Concert secondConcert = new Concert(2, new DateTime(1997, 01, 01));
      firstConcert.Save();
      secondConcert.Save();
      //Act
      testGoer.AddConcert(firstConcert);
      List<Concert> savedConcerts = testGoer.GetConcerts();
      List<Concert> testList = new List<Concert>{firstConcert};
      //Assert
      Assert.Equal(testList, savedConcerts);
    }
    [Fact]
    public void Delete_DeletesGoerAssociationsFromDatabase_GoerList()
    {
      //Arrange
      Concert testConcert = new Concert(4, new DateTime(1987, 01, 01));
      testConcert.Delete();

      Goer testGoer = new Goer("James");
      testGoer.Save();


      //Act
      testGoer.AddConcert(testConcert);
      testGoer.Delete();

      List<Goer> result = testConcert.GetGoers();
      List<Goer> test = new List<Goer> {};

      //Assert
      Assert.Equal(test, result);
    }
    [Fact]
    public void Update_UpdatesGoerInDatabase_String()
    {
      //Arrange
      string name = "Greg";
      Goer testGoer = new Goer(name);
      testGoer.Save();
      string newName = "Theodore";

      //Act
      testGoer.Update(newName);

      string result = testGoer.GetName();

      //Assert
      Assert.Equal(newName, result);
    }
    public void Dispose()
    {
      Band.DeleteAll();
      Venue.DeleteAll();
      Concert.DeleteAll();
      Goer.DeleteAll();
    }
  }
}
