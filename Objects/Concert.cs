using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using BandTracker;

namespace BandTracker.Objects
{
  public class Concert
  {
    private int _id;
    private DateTime _showDate;
    private int _bands_id;

  public Concert(int BandId, DateTime ShowDate, int Id = 0)
  {
    _id = Id;
    _bands_id = BandId;
    _showDate = ShowDate;
  }

  public override bool Equals(System.Object otherConcert)
  {
    if(!(otherConcert is Concert))
    {
      return false;
    }
    else
    {
      Concert newConcert = (Concert) otherConcert;
      bool idEquality = (this.GetId() == newConcert.GetId());
      bool bandIdEquality = (this.GetBandId() == newConcert.GetBandId());
      bool showDateEquality = (this.GetShowDate() == newConcert.GetShowDate());
      return (idEquality && bandIdEquality && showDateEquality);
    }
  }
    public int GetId()
    {
      return _id;
    }
    public void SetId(int Id)
    {
      _id = Id;
    }
    public int GetBandId()
    {
      return _bands_id;
    }
    public void SetBandId(int BandId)
    {
      _bands_id = BandId;
    }
    public DateTime GetShowDate()
    {
      return _showDate;
    }

  public static void DeleteAll()
  {
    SqlConnection conn = DB.Connection();
    conn.Open();
    SqlCommand cmd = new SqlCommand("Delete FROM concerts;", conn);
    cmd.ExecuteNonQuery();
    conn.Close();
  }
  public static List<Concert> GetAll()
  {
    List<Concert> allConcerts = new List<Concert>{};

    SqlConnection conn = DB.Connection();
    conn.Open();

    SqlCommand cmd = new SqlCommand("SELECT * FROM concerts", conn);
    SqlDataReader rdr = cmd.ExecuteReader();

    while(rdr.Read())
    {
      int concertId = rdr.GetInt32(0);
      DateTime concertShowDate = rdr.GetDateTime(1);
      int concertBandId = rdr.GetInt32(2);

      Concert newConcert = new Concert(concertBandId, concertShowDate, concertId);
      allConcerts.Add(newConcert);
    }

    if (rdr != null)
        {
          rdr.Close();
        }
        if (conn != null)
        {
          conn.Close();
        }
        return allConcerts;
    }
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO concerts (showDate, bands_id) OUTPUT INSERTED.id VALUES (@ConcertShowDate, @BandId);", conn);
      SqlParameter showDateParameter = new SqlParameter();
      showDateParameter.ParameterName = "@ConcertShowDate";
      showDateParameter.Value = this.GetShowDate();

      SqlParameter bandIdParameter = new SqlParameter();
      bandIdParameter.ParameterName = "@BandId";
      bandIdParameter.Value = this.GetBandId();

      cmd.Parameters.Add(showDateParameter);
      cmd.Parameters.Add(bandIdParameter);

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
    public static Concert Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM concerts WHERE id = @ConcertId;", conn);
      SqlParameter ConcertIdParameter = new SqlParameter();
      ConcertIdParameter.ParameterName = "@ConcertId";
      ConcertIdParameter.Value = id.ToString();
      cmd.Parameters.Add(ConcertIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundConcertId = 0;
      int foundConcertBandId = 0;
      DateTime foundConcertShowDate = default(DateTime);

      while(rdr.Read())
      {
        foundConcertId = rdr.GetInt32(0);
        foundConcertShowDate = rdr.GetDateTime(1);
        foundConcertBandId = rdr.GetInt32(2);
      }
      Concert foundConcert = new Concert(foundConcertBandId, foundConcertShowDate, foundConcertId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundConcert;
      }
  }
}
