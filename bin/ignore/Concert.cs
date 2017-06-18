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
    private int _venues_id;

  public Concert(DateTime ShowDate, int VenueId, int Id = 0)
  {
    _id = Id;
    _showDate = ShowDate;
    _venues_id = VenueId;
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
      bool showDateEquality = (this.GetShowDate() == newConcert.GetShowDate());
      bool venueIdEquality = (this.GetVenueId() == newConcert.GetVenueId());
      return (idEquality && showDateEquality && venueIdEquality);
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
    public DateTime GetShowDate()
    {
      return _showDate;
    }
    public int GetVenueId()
    {
      return _venues_id;
    }
    public void SetVenueId(int VenueId)
    {
      _venues_id = VenueId;
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
      int concertVenueId = rdr.GetInt32(2);

      Concert newConcert = new Concert(concertShowDate, concertVenueId, concertId);
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

      SqlCommand cmd = new SqlCommand("INSERT INTO concerts (showDate, venues_id) OUTPUT INSERTED.id VALUES (@ConcertShowDate, @ConcertVenueId);", conn);
      SqlParameter showDateParameter = new SqlParameter();
      showDateParameter.ParameterName = "@ConcertShowDate";
      showDateParameter.Value = this.GetShowDate();

      SqlParameter venueIdParameter = new SqlParameter();
      venueIdParameter.ParameterName = "@ConcertVenueId";
      venueIdParameter.Value = this.GetVenueId();

      cmd.Parameters.Add(showDateParameter);
      cmd.Parameters.Add(venueIdParameter);

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
      DateTime foundConcertShowDate = default(DateTime);
      int foundConcertVenueId = 0;

      while(rdr.Read())
      {
        foundConcertId = rdr.GetInt32(0);
        foundConcertShowDate = rdr.GetDateTime(1);
        foundConcertVenueId = rdr.GetInt32(2);
      }
      Concert foundConcert = new Concert(foundConcertShowDate, foundConcertVenueId, foundConcertId);

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
      public void AddBand(Band newBand)
      {
        SqlConnection conn = DB.Connection();
        conn.Open();

        SqlCommand cmd = new SqlCommand("INSERT INTO concerts_bands (bands_id, concerts_id) VALUES (@BandId, @ConcertId);", conn);

        SqlParameter BandIdParameter = new SqlParameter();
        BandIdParameter.ParameterName = "@BandId";
        BandIdParameter.Value = newBand.GetId();
        cmd.Parameters.Add(BandIdParameter);

        SqlParameter ConcertIdParameter = new SqlParameter();
        ConcertIdParameter.ParameterName = "@ConcertId";
        ConcertIdParameter.Value = this.GetId();
        cmd.Parameters.Add(ConcertIdParameter);

        cmd.ExecuteNonQuery();

        if(conn != null)
        {
          conn.Close();
        }
      }

      public List<Band> GetBands()
      {
        SqlConnection conn = DB.Connection();
        conn.Open();

        SqlCommand cmd = new SqlCommand("SELECT bands.* FROM concerts JOIN concerts_bands ON (concerts.id = concerts_bands.concerts_id) JOIN bands ON (concerts_bands.bands_id = bands.id) WHERE concerts.id = @ConcertId;", conn);

        SqlParameter ConcertIdParameter = new SqlParameter();
        ConcertIdParameter.ParameterName = "@ConcertId";
        ConcertIdParameter.Value = this.GetId().ToString();

        cmd.Parameters.Add(ConcertIdParameter);

        SqlDataReader rdr = cmd.ExecuteReader();

        List<Band> bands = new List<Band>{};

        while(rdr.Read())
        {
          int bandId = rdr.GetInt32(0);
          string bandName = rdr.GetString(1);

          Band newBand = new Band(bandName, bandId);
          bands.Add(newBand);
        }

        if(rdr != null)
        {
          rdr.Close();
        }
        if(conn != null)
        {
          conn.Close();
        }
        return bands;
      }

      public void Delete()
        {
        SqlConnection conn = DB.Connection();
        conn.Open();

        SqlCommand cmd = new SqlCommand("DELETE FROM concerts WHERE id = @ConcertId; DELETE FROM concerts_bands WHERE concerts_id = @ConcertId;", conn);
        SqlParameter concertIdParameter = new SqlParameter();
        concertIdParameter.ParameterName = "@ConcertId";
        concertIdParameter.Value = this.GetId();

        cmd.Parameters.Add(concertIdParameter);
        cmd.ExecuteNonQuery();

        if (conn != null)
        {
          conn.Close();
        }
      }
      public void Update(DateTime newShowDate)
      {
        SqlConnection conn = DB.Connection();
        conn.Open();
        SqlCommand cmd = new SqlCommand("UPDATE concerts SET showDate = @NewShowDate OUTPUT INSERTED.name WHERE id = @ConcertId;", conn);

        SqlParameter newShowDateParameter = new SqlParameter();
        newShowDateParameter.ParameterName = "@NewShowDate";
        newShowDateParameter.Value = newShowDate;
        cmd.Parameters.Add(newShowDateParameter);

        SqlParameter concertIdParameter = new SqlParameter();
        concertIdParameter.ParameterName = "@ConcertId";
        concertIdParameter.Value = this.GetId();
        cmd.Parameters.Add(concertIdParameter);

        SqlDataReader rdr = cmd.ExecuteReader();
        while(rdr.Read())
        {
          this._showDate = rdr.GetDateTime(0);
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
