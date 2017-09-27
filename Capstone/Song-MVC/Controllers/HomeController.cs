namespace Capstone_MVC.Controllers
{
    using Capstone_BLL;
    using Capstone_DAL;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web.Mvc;
    using ViewModels;

    public class HomeController : Controller
    {
        //New instantiations of DAL & BLL class for Album
        AlbumDataAccess _AlbumDALAccess = new AlbumDataAccess();
        AlbumBusinessLogic _AlbumBLLAccess = new AlbumBusinessLogic();

        //New instantiations of DAL & BLL class for Artist
        ArtistDataAccess _ArtistDALAccess = new ArtistDataAccess();
        ArtistBusinessLogic _ArtistBLLAccess = new ArtistBusinessLogic();

        //Welcome
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        //About Us, Stats
        [HttpGet]
        public ActionResult About()
        {
            ActionResult oResponse = null;
            try
            {
                //Instantiate new VM
                StatsVM oViewStatsVM = new StatsVM();

                //Make a data call for NumberOfTracks on Album
                List<short> lListOfNumberOfTracks = _AlbumDALAccess.ViewAllAlbumsNumberOfTracks();
                //Calculate average
                int lAverageNumberOfTracks = _ArtistBLLAccess.CalculateAverageFromListOfShorts
                    (lListOfNumberOfTracks);
                //Assign average to VM
                oViewStatsVM.AverageNumberOfTracksOnAlbums = lAverageNumberOfTracks;

                //Make a data call for NumberOfMembers on Artist
                List<short> lListOfNumberOfMembers = _ArtistDALAccess.ViewAllArtistsNumberOfMembers();
                //Calculate average
                int lAverageNumberOfMembers = _ArtistBLLAccess.CalculateAverageFromListOfShorts
                    (lListOfNumberOfMembers);
                //Assign average to VM
                oViewStatsVM.AverageNumberOfMembersInArtists = lAverageNumberOfMembers;

                //Make a data call for YearsActive on Artist
                List<short> lListOfYearsActive = _ArtistDALAccess.ViewAllArtistsYearsActive();
                //Calculate average
                int lAverageYearsActive = _ArtistBLLAccess.CalculateAverageFromListOfShorts
                    (lListOfYearsActive);
                //Assign average to VM
                oViewStatsVM.AverageYearsActiveOfArtists = lAverageYearsActive;

                //Set oResponse to use VM
                oResponse = View(oViewStatsVM);
            }
            catch (Exception ex)
            {
                //Log error
                using (StreamWriter fileWriter = new StreamWriter(@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\PLErrors\PLErrors.txt", true))
                {
                    fileWriter.WriteLine("{0} - {1}", DateTime.Now.ToString("MM-dd-yyyy hh:ss"), ex.Message);
                }

                //Instantiate new Error object
                Error lError = new Error();
                //Populate object
                lError.ErrorHeader = "Oh no, something has gone wrong!";
                lError.ErrorMessage = "If error persists, please contact us with the details" +
                    " of your problem and we'll get back to you as soon as we can.";
                //Send user to Error page
                oResponse = View("Error", lError);
            }
            finally
            {
                //Do nothing
            }
            return oResponse;
        }

        //Contact Info
        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }
    }
}