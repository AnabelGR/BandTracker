using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using BandTracker.Objects;

namespace BandTracker
{
  [Collection("BandTracker")]
  public class BandTest : IDisposable
  {
    public BandTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void GetAll_GetsBands_DatabaseEmpty()
    {
      //Arrange, Act
      int result = Band.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }
    public void Dispose()
    {
      Band.DeleteAll();
    }
    [Fact]
    public void Equals_ChecksObjectEquality_True()
    {
      //Arrange, Act
      Band firstBand = new Band("Roseland");
      Band secondBand = new Band("Roseland");
      //Assert
      Assert.Equal(firstBand, secondBand);
    }
    [Fact]
    public void Save_DoesSaveToDatabase_True()
    {
      //Arrange
      Band testBand = new Band("Roseland");
      testBand.Save();
      //Act
      List<Band> result = Band.GetAll();
      List<Band> testList = new List<Band>{testBand};
      //Assert
      Assert.Equal(testList, result);
    }
    [Fact]
    public void Find_FindsBandInDatabase_True()
    {
      Band testBand = new Band("Star Theater");
      testBand.Save();

      Band foundBand = Band.Find(testBand.GetId());

      Assert.Equal(testBand, foundBand);
    }
  }
}
