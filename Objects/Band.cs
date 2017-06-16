using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using BandTracker;

namespace BandTracker.Objects
{
  public class Band
  {
    private int _id;
    private string _name;

  public Band(string Name, int Id = 0)
  {
    _name = Name;
    _id = Id;
  }

  public override bool Equals(System.Object otherBand)
  {
    if(!(otherBand is Band))
    {
      return false;
    }
    else
    {
      Band newBand = (Band) otherBand;
      bool idEquality = (this.GetId() == newBand.GetId());
      bool nameEquality = (this.GetName() == newBand.GetName());
      return (idEquality && nameEquality);
    }
  }

  public int GetId()
  {
    return _id;
  }

  public string GetName()
  {
    return _name;
  }

  public void SetId(int Id)
  {
    _id = Id;
  }
  public void SetName(string Name)
  {
    _name = Name;
  }


  public static List<Band> GetAll()
  {
    List<Band> allBands = new List<Band>{};

    SqlConnection conn = DB.Connection();
    conn.Open();

    SqlCommand cmd = new SqlCommand("SELECT * FROM bands", conn);
    SqlDataReader rdr = cmd.ExecuteReader();

    while(rdr.Read())
    {
      int courseId = rdr.GetInt32(0);
      string courseName = rdr.GetString(1);

      Band newBand = new Band(courseName, courseId);
      allBands.Add(newBand);
    }

    if (rdr != null)
        {
          rdr.Close();
        }
        if (conn != null)
        {
          conn.Close();
        }
        return allBands;
    }
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO bands (name) OUTPUT INSERTED.id VALUES (@BandName);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@BandName";
      nameParameter.Value = this.GetName();

      cmd.Parameters.Add(nameParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {

        this._id = rdr.GetInt32(0);
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }
    public static Band Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM bands WHERE id = @BandId;", conn);
      SqlParameter BandIdParameter = new SqlParameter();
      BandIdParameter.ParameterName = "@BandId";
      BandIdParameter.Value = id.ToString();
      cmd.Parameters.Add(BandIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundBandId = 0;
      string foundBandName = null;

      while(rdr.Read())
      {
        foundBandId = rdr.GetInt32(0);
        foundBandName = rdr.GetString(1);
      }
      Band foundBand = new Band(foundBandName, foundBandId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundBand;
      }
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("Delete FROM bands;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
