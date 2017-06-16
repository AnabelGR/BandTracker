using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using BandTracker;

namespace BandTracker.Objects
{
  public class Goer
  {
    private int _id;
    private string _name;

  public Goer(string Name, int Id = 0)
  {
    _name = Name;
    _id = Id;
  }

  public override bool Equals(System.Object otherGoer)
  {
    if(!(otherGoer is Goer))
    {
      return false;
    }
    else
    {
      Goer newGoer = (Goer) otherGoer;
      bool idEquality = (this.GetId() == newGoer.GetId());
      bool nameEquality = (this.GetName() == newGoer.GetName());
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

  public static void DeleteAll()
  {
    SqlConnection conn = DB.Connection();
    conn.Open();
    SqlCommand cmd = new SqlCommand("Delete FROM goers;", conn);
    cmd.ExecuteNonQuery();
    conn.Close();
  }
  public static List<Goer> GetAll()
  {
    List<Goer> allGoers = new List<Goer>{};

    SqlConnection conn = DB.Connection();
    conn.Open();

    SqlCommand cmd = new SqlCommand("SELECT * FROM goers", conn);
    SqlDataReader rdr = cmd.ExecuteReader();

    while(rdr.Read())
    {
      int goerId = rdr.GetInt32(0);
      string goerName = rdr.GetString(1);

      Goer newGoer = new Goer(goerName, goerId);
      allGoers.Add(newGoer);
    }

    if (rdr != null)
        {
          rdr.Close();
        }
        if (conn != null)
        {
          conn.Close();
        }
        return allGoers;
    }
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO goers (name) OUTPUT INSERTED.id VALUES (@GoerName);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@GoerName";
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
    public static Goer Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM goers WHERE id = @GoerId;", conn);
      SqlParameter GoerIdParameter = new SqlParameter();
      GoerIdParameter.ParameterName = "@GoerId";
      GoerIdParameter.Value = id.ToString();
      cmd.Parameters.Add(GoerIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundGoerId = 0;
      string foundGoerName = null;

      while(rdr.Read())
      {
        foundGoerId = rdr.GetInt32(0);
        foundGoerName = rdr.GetString(1);
      }
      Goer foundGoer = new Goer(foundGoerName, foundGoerId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundGoer;
      }
    public void AddConcert(Concert newConcert)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO concerts_goers (concerts_id, goers_id) VALUES (@ConcertId, @GoerId);", conn);

      SqlParameter ConcertIdParameter = new SqlParameter();
      ConcertIdParameter.ParameterName = "@ConcertId";
      ConcertIdParameter.Value = newConcert.GetId();
      cmd.Parameters.Add(ConcertIdParameter);

      SqlParameter GoerIdParameter = new SqlParameter();
      GoerIdParameter.ParameterName = "@GoerId";
      GoerIdParameter.Value = this.GetId();
      cmd.Parameters.Add(GoerIdParameter);

      cmd.ExecuteNonQuery();

      if(conn != null)
      {
        conn.Close();
      }
    }

    public List<Concert> GetConcerts()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT concerts.* FROM goers JOIN concerts_goers ON (goers.id = concerts_goers.goers_id) JOIN concerts ON (concerts_goers.concerts_id = concerts.id) WHERE goers.id = @GoerId;", conn);

      SqlParameter GoerIdParameter = new SqlParameter();
      GoerIdParameter.ParameterName = "@GoerId";
      GoerIdParameter.Value = this.GetId().ToString();

      cmd.Parameters.Add(GoerIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      List<Concert> concerts = new List<Concert>{};

      while(rdr.Read())
      {
        int concertId = rdr.GetInt32(0);
        DateTime concertShowDate = rdr.GetDateTime(1);
        int concertBandId = rdr.GetInt32(2);

        Concert newConcert = new Concert(concertBandId, concertShowDate, concertId);
        concerts.Add(newConcert);
      }

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return concerts;
    }

    public void Delete()
      {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM goers WHERE id = @GoerId; DELETE FROM concerts_goers WHERE goers_id = @GoerId;", conn);
      SqlParameter goerIdParameter = new SqlParameter();
      goerIdParameter.ParameterName = "@GoerId";
      goerIdParameter.Value = this.GetId();

      cmd.Parameters.Add(goerIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }
    public void Update(string newName)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("UPDATE goers SET name = @NewName OUTPUT INSERTED.name WHERE id = @GoerId;", conn);

      SqlParameter newNameParameter = new SqlParameter();
      newNameParameter.ParameterName = "@NewName";
      newNameParameter.Value = newName;
      cmd.Parameters.Add(newNameParameter);

      SqlParameter goerIdParameter = new SqlParameter();
      goerIdParameter.ParameterName = "@GoerId";
      goerIdParameter.Value = this.GetId();
      cmd.Parameters.Add(goerIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        this._name = rdr.GetString(0);
      }

      if (rdr != null)
      {
        rdr.Close();
      }

      if (conn != null)
      {
        conn.Close();
      }
    }
  }
}
