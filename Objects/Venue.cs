using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using BandTracker;

namespace BandTracker.Objects
{
  public class Venue
  {
    private int _id;
    private string _name;

  public Venue(string Name, int Id = 0)
  {
    _name = Name;
    _id = Id;
  }

  public override bool Equals(System.Object otherVenue)
  {
    if(!(otherVenue is Venue))
    {
      return false;
    }
    else
    {
      Venue newVenue = (Venue) otherVenue;
      bool idEquality = (this.GetId() == newVenue.GetId());
      bool nameEquality = (this.GetName() == newVenue.GetName());
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
    SqlCommand cmd = new SqlCommand("Delete FROM venues;", conn);
    cmd.ExecuteNonQuery();
    conn.Close();
  }

  public static List<Venue> GetAll()
  {
    List<Venue> allVenues = new List<Venue>{};

    SqlConnection conn = DB.Connection();
    conn.Open();

    SqlCommand cmd = new SqlCommand("SELECT * FROM venues", conn);
    SqlDataReader rdr = cmd.ExecuteReader();

    while(rdr.Read())
    {
      int venueId = rdr.GetInt32(0);
      string venueName = rdr.GetString(1);

      Venue newVenue = new Venue(venueName, venueId);
      allVenues.Add(newVenue);
    }

    if (rdr != null)
        {
          rdr.Close();
        }
        if (conn != null)
        {
          conn.Close();
        }
        return allVenues;
    }
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO venues (name) OUTPUT INSERTED.id VALUES (@Name);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@Name";
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
    public static Venue Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM venues WHERE id = @VenueId;", conn);
      SqlParameter VenueIdParameter = new SqlParameter();
      VenueIdParameter.ParameterName = "@VenueId";
      VenueIdParameter.Value = id.ToString();
      cmd.Parameters.Add(VenueIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundVenueId = 0;
      string foundVenueName = null;

      while(rdr.Read())
      {
        foundVenueId = rdr.GetInt32(0);
        foundVenueName = rdr.GetString(1);
      }
      Venue foundVenue = new Venue(foundVenueName, foundVenueId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundVenue;
      }

    public void AddBand(Band newBand)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO venues_bands (venues_id, bands_id) VALUES (@VenueId, @BandId);", conn);

      SqlParameter VenueIdParameter = new SqlParameter();
      VenueIdParameter.ParameterName = "@VenueId";
      VenueIdParameter.Value = this.GetId();
      cmd.Parameters.Add(VenueIdParameter);

      SqlParameter BandIdParameter = new SqlParameter();
      BandIdParameter.ParameterName = "@BandId";
      BandIdParameter.Value = newBand.GetId();
      cmd.Parameters.Add(BandIdParameter);

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

      SqlCommand cmd = new SqlCommand("SELECT bands.* FROM venues JOIN venues_bands ON (venues.id = venues_bands.venues_id) JOIN bands ON (venues_bands.bands_id = bands.id) WHERE venues.id = @VenueId;", conn);

      SqlParameter VenueIdParameter = new SqlParameter();
      VenueIdParameter.ParameterName = "@VenueId";
      VenueIdParameter.Value = this.GetId();

      cmd.Parameters.Add(VenueIdParameter);

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

        SqlCommand cmd = new SqlCommand("DELETE FROM venues WHERE id = @VenueId; DELETE FROM venues_bands WHERE venue_id = @VenueId;", conn);
        SqlParameter venueIdParameter = new SqlParameter();
        venueIdParameter.ParameterName = "@VenueId";
        venueIdParameter.Value = this.GetId();

        cmd.Parameters.Add(venueIdParameter);
        cmd.ExecuteNonQuery();

        if (conn != null)
        {
          conn.Close();
        }
      }

  }
}
