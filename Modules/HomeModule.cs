using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;
using BandTracker.Objects;

namespace BandTracker
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        List<Venue> AllVenues = Venue.GetAll();
        return View["index.cshtml", AllVenues];
      };
      Get["/bands"] = _ => {
        List<Band> AllBands = Band.GetAll();
        return View["bands.cshtml", AllBands];
      };
      Get["/venues"] = _ => {
        List<Venue> AllVenues = Venue.GetAll();
        return View["venues.cshtml", AllVenues];
      };
      Get["/concerts"] = _ => {
        List<Concert> AllConcerts = Concert.GetAll();
        return View["concerts.cshtml", AllConcerts];
      };
      Get["/venues/new"] = _ => {
        return View["venues_form.cshtml"];
      };
      Post["/venues/new"] = _ => {
        Venue newVenue = new Venue(Request.Form["venue-name"]);
        newVenue.Save();
        return View["index.cshtml"];
      };
      Get["/bands/new"] = _ => {
        List<Venue> AllVenues = Venue.GetAll();
        return View["bands_form.cshtml", AllVenues];
      };
      Post["/bands/new"] = _ => {
        Band newBand = new Band(Request.Form["band-name"]);
        newBand.Save();
        return View["index.cshtml"];
      };
      Get["/concerts/new"] = _ => {
        return View["concerts_form.cshtml"];
      };
      Post["/concerts/new"] = _ => {
        Concert newConcert = new Concert(Request.Form["concert-showDate"], Request.Form["concert-band-id"]);
        newConcert.Save();
        return View["index.cshtml"];
      };
      Post["/venue/delete"] = _ => {
        Venue.DeleteAll();
        return View["index.cshtml"];
      };
      Post["/bands/delete"] = _ => {
        Band.DeleteAll();
        return View["index.cshtml"];
      };
      Post["/concerts/delete"] = _ => {
        Concert.DeleteAll();
        return View["index.cshtml"];
      };
      Get["venues/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Venue SelectedVenue = Venue.Find(parameters.id);
        List<Band> VenueBands = SelectedVenue.GetBands();
        List<Band> AllBands = Band.GetAll();
        model.Add("venue", SelectedVenue);
        model.Add("venueBands", VenueBands);
        model.Add("allBands", AllBands);
        return View["venue.cshtml", model];
      };
      Get["bands/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Band SelectedBand = Band.Find(parameters.id);
        List<Venue> BandVenues = SelectedBand.GetVenues();
        List<Venue> AllVenues = Venue.GetAll();
        model.Add("band", SelectedBand);
        model.Add("bandVenues", BandVenues);
        model.Add("allVenues", AllVenues);
        return View["band.cshtml", model];
       };

      Get["venue/delete/{id}"] = parameters => {
        Venue SelectedVenue = Venue.Find(parameters.id);
        return View["venue_delete.cshtml", SelectedVenue];
      };
      Delete["venue/delete/{id}"] = parameters => {
        Venue SelectedVenue = Venue.Find(parameters.id);
        SelectedVenue.Delete();
        return View["index.cshtml"];
      };
      Get["band/delete/{id}"] = parameters => {
       Band SelectedBand = Band.Find(parameters.id);
       return View["band_delete.cshtml", SelectedBand];
      };
      Delete["band/delete/{id}"] = parameters => {
        Band SelectedBand = Band.Find(parameters.id);
        SelectedBand.Delete();
        return View["index.cshtml"];
      };
      Get["concert/delete/{id}"] = parameters => {
       Concert SelectedConcert = Concert.Find(parameters.id);
       return View["concert_delete.cshtml", SelectedConcert];
      };
      Delete["concert/delete/{id}"] = parameters => {
        Concert SelectedConcert = Concert.Find(parameters.id);
        SelectedConcert.Delete();
        return View["index.cshtml"];
      };
      Post["band/add_venue"] = _ => {
        Venue venue = Venue.Find(Request.Form["venue-id"]);
        Band band = Band.Find(Request.Form["band-id"]);
        band.AddVenue(venue);
        return View["index.cshtml"];
      };
      Post["venue/add_band"] = _ => {
        Venue venue = Venue.Find(Request.Form["venue-id"]);
        Band band = Band.Find(Request.Form["band-id"]);
        venue.AddBand(band);
        return View["index.cshtml"];
      };
      Post["concert/add_band"] = _ => {
        Band band = Band.Find(Request.Form["band-id"]);
        Concert concert = Concert.Find(Request.Form["concert-id"]);
        concert.AddBand(band);
        return View["index.cshtml"];
      };
      Post["band/add_concert"] = _ => {
        Band band = Band.Find(Request.Form["band-id"]);
        Concert concert = Concert.Find(Request.Form["concert-id"]);
        band.AddConcert(concert);
        return View["index.cshtml"];
      };
      Get["venue/edit/{id}"] = parameters => {
        Venue selectedVenue = Venue.Find(parameters.id);
        return View["venue_edit.cshtml", selectedVenue];
      };
      Patch["venue/edit/{id}"] = parameters => {
        Venue SelectedVenue = Venue.Find(parameters.id);
        SelectedVenue.Update(Request.Form["venue-name"]);
        return View["index.cshtml"];
      };
      Get["band/edit/{id}"] = parameters => {
        Band selectedBand = Band.Find(parameters.id);
        return View["band_edit.cshtml", selectedBand];
      };
      Patch["band/edit/{id}"] = parameters => {
        Band SelectedBand = Band.Find(parameters.id);
        SelectedBand.Update(Request.Form["band-name"]);
        return View["index.cshtml"];
      };
      Get["Concert/edit/{id}"] = parameters => {
        Concert selectedConcert = Concert.Find(parameters.id);
        return View["Concert_edit.cshtml", selectedConcert];
      };
      Patch["Concert/edit/{id}"] = parameters => {
        Concert SelectedConcert = Concert.Find(parameters.id);
        SelectedConcert.Update(Request.Form["Concert-name"]);
        return View["index.cshtml"];
      };
    }
  }
}
