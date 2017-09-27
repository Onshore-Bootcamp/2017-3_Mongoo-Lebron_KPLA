namespace Capstone_MVC.Controllers
{
    using Capstone_DAL;
    using Capstone_DAL.Interfaces;
    using Custom.Maps;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web.Mvc;
    using ViewModels;

    public class SongController : Controller
    {
        //Constructor
        public SongController()
        {
            PopulateSongVM();
        }

        //New instantiations of DAL classes for Arist, Album, and Song
        ArtistDataAccess _ArtistDALAccess = new ArtistDataAccess();
        AlbumDataAccess _AlbumDALAccess = new AlbumDataAccess();
        SongDataAccess _SongDALAccess = new SongDataAccess();
        //New instantiation of VM
        SongVM _SongVM = new SongVM();

        //View All method
        [HttpGet]
        public ActionResult Index()
        {
            ActionResult oResponse = null;
            try
            {
                //Make a data call
                List<ISongDO> lListOfSongDOs = _SongDALAccess.ViewAllSongs();
                //Method call to map data
                List<SongPO> lListOfSongPOs = MapSong.MapListOfSongDOsToListOfPOs(lListOfSongDOs);
                //Assign list to VM
                _SongVM.ListOfSongPOs = lListOfSongPOs;

                //Foreach loop to assign album's name, and artist's name to SongPO
                foreach (SongPO song in _SongVM.ListOfSongPOs)
                {
                    song.AlbumName = _AlbumDALAccess.ViewAlbumNameByID(song.AlbumID);
                    song.ArtistName = _ArtistDALAccess.ViewArtistNameByID(song.ArtistID);
                }

                oResponse = View(_SongVM);
            }
            catch (Exception ex)
            {
                //Log error
                using (StreamWriter fileWriter = new StreamWriter(@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\PLErrors\SongPLErrors.txt", true))
                {
                    fileWriter.WriteLine("{0} - {1}", DateTime.Now.ToString("MM-dd-yyyy hh:ss"),
                        ex.Message);
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

        [HttpGet]
        public ActionResult AddNewSong()
        {
            ActionResult oResponse = null;

            if (Session["Username"] == null)
            {
                //If not a user, redirect to welcome page
                oResponse = RedirectToAction("Index", "Home");
            }
            else if ((Int16)Session["Role"] == 1 || (Int16)Session["Role"] == 2)
            {
                //If User or Power User, redirect to Veiw All Songs
                oResponse = RedirectToAction("Index", "Song");
            }
            else
            {
                //If Admin, allow Add New Song view
                try
                {
                    oResponse = View(_SongVM);
                }
                catch (Exception ex)
                {
                    //Log exception
                    using (StreamWriter fileWriter = new StreamWriter(@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\PLErrors\SongPLErrors.txt", true))
                    {
                        fileWriter.WriteLine("{0} - {1}", DateTime.Now.ToString("MM-dd-yyyy hh:ss"),
                            ex.Message);
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
            }
            return oResponse;
        }

        [HttpPost]
        public ActionResult AddNewSong(SongVM iSongVM)
        {
            ActionResult oResponse = null;

            if (Session["Username"] == null)
            {
                //If not a user, redirect to welcome page
                oResponse = RedirectToAction("Index", "Home");
            }
            else if ((Int16)Session["Role"] == 1 || (Int16)Session["Role"] == 2)
            {
                //If User or Power User, redirect to View All
                oResponse = RedirectToAction("Index", "Song");
            }
            else
            {
                //If Admin, allow view
                if (ModelState.IsValid)
                {
                    try
                    {
                        //Method call to map form info from PO to DO
                        ISongDO lSongDO = MapSong.MapSongFromPOtoDO(iSongVM.Song);
                        //Method call to insert song into the DB
                        _SongDALAccess.AddNewSong(lSongDO);

                        oResponse = RedirectToAction("Index", "Song");
                    }
                    catch (Exception ex)
                    {
                        //Log exception
                        using (StreamWriter fileWriter = new StreamWriter(@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\PLErrors\SongPLErrors.txt", true))
                        {
                            fileWriter.WriteLine("{0} - {1}", DateTime.Now.ToString("MM-dd-yyyy hh:ss"),
                                ex.Message);
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
                        //Don't think we need anything more
                    }
                }
                else
                {
                    //If ModelState is not valid, return form and try again
                    oResponse = View(_SongVM);
                }
            }
            return oResponse;
        }

        [HttpGet]
        public ActionResult ViewSongByID(long iSongID)
        {
            ActionResult oResponse = null;

            if (Session["Username"] == null)
            {
                //If not a user, redirect to welcome page
                oResponse = RedirectToAction("Index", "Home");
            }
            else
            {
                //If user, allow view
                try
                {
                    //Make a data call
                    ISongDO lSongDO = _SongDALAccess.ViewSongByID(iSongID);
                    //Method call to map data to a PO
                    SongPO lSongPO = MapSong.MapSongFromDOtoPO(lSongDO);

                    //Assign album name to SongPO
                    lSongPO.AlbumName = _AlbumDALAccess.ViewAlbumNameByID(lSongPO.AlbumID);
                    //Assign artist name to SongPO
                    lSongPO.ArtistName = _ArtistDALAccess.ViewArtistNameByID(lSongPO.ArtistID);

                    //Populate VM with info from data call
                    _SongVM.Song = lSongPO;

                    oResponse = View(_SongVM);
                }
                catch (Exception ex)
                {
                    //Log error
                    using (StreamWriter fileWriter = new StreamWriter(@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\PLErrors\SongPLErrors.txt", true))
                    {
                        fileWriter.WriteLine("{0} - {1}", DateTime.Now.ToString("MM-dd-yyyy hh:ss"),
                            ex.Message);
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
                    //Don't think we need anything here
                }
            }
            return oResponse;
        }

        [HttpGet]
        public ActionResult UpdateSongByID(long iSongID)
        {
            ActionResult oResponse = null;

            if (Session["Username"] == null)
            {
                //If not a user, redirect to welcome page
                oResponse = RedirectToAction("Index", "Home");
            }
            else if ((Int16)Session["Role"] == 2 || (Int16)Session["Role"] == 3)
            {
                //If Power User or Admin, allow process
                try
                {
                    //Make a data call to get the DO
                    ISongDO lSongDO = _SongDALAccess.ViewSongByID(iSongID);
                    //Method call to map the DO to a PO
                    SongPO lSongPO = MapSong.MapSongFromDOtoPO(lSongDO);
                    //Populate VM with song info from data call
                    _SongVM.Song = lSongPO;

                    //Instantiate new list of Albums
                    List<IAlbumDO> lListOfAlbums = _AlbumDALAccess.ViewAllAlbums();
                    //Assign list of Artists to Song VM
                    _SongVM.Song.DropdownAlbums = new SelectList(lListOfAlbums, "AlbumID", "Name");

                    //Instantiate new list of Artists
                    List<IArtistDO> lListOfArtists = _ArtistDALAccess.ViewAllArtists();
                    //Assign list of Artists to Song VM
                    _SongVM.Song.DropdownArtists = new SelectList(lListOfArtists, "ArtistID", "Name");

                    oResponse = View(_SongVM);
                }
                catch (Exception ex)
                {
                    //Log error
                    using (StreamWriter fileWriter = new StreamWriter(@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\PLErrors\SongPLErrors.txt", true))
                    {
                        fileWriter.WriteLine("{0} - {1}", DateTime.Now.ToString("MM-dd-yyyy hh:ss"),
                            ex.Message);
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
                    //Don't think we need anything here
                }
            }
            else
            {
                //If User, redirect to View Album By ID
                oResponse = RedirectToAction("ViewSongByID", "Song", new { iSongID = iSongID });
            }
            return oResponse;
        }

        [HttpPost]
        public ActionResult UpdateSongByID(SongVM iSongForm)
        {
            ActionResult oResponse = null;

            if (Session["Username"] == null)
            {
                //If not a user, redirect to welcome page
                oResponse = RedirectToAction("Index", "Home");
            }
            else if ((Int16)Session["Role"] == 2 || (Int16)Session["Role"] == 3)
            {
                //If Power User or Admin, allow process
                if (ModelState.IsValid)
                {
                    try
                    {
                        //Method call to map from PO to DO
                        ISongDO lUpdateSongForm = MapSong.MapSongFromPOtoDO(iSongForm.Song);
                        //Method call to execute procedure
                        _SongDALAccess.UpdateSongByID(lUpdateSongForm);

                        //Redirect user to View Details
                        oResponse = RedirectToAction("ViewSongByID", "Song",
                            new { iSongID = iSongForm.Song.SongID });
                    }
                    catch (Exception ex)
                    {
                        //Log error
                        using (StreamWriter fileWriter = new StreamWriter(@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\PLErrors\SongPLErrors.txt", true))
                        {
                            fileWriter.WriteLine("{0} - {1}", DateTime.Now.ToString("MM-dd-yyyy hh:ss"),
                                ex.Message);
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
                }
                else
                {
                    //If form is not filled out, return the view with inputs
                    oResponse = View(_SongVM);
                }
            }
            else
            {
                //If User, redirect to View Song By ID
                oResponse = RedirectToAction("ViewSongByID", "Song",
                    new { iSongID = iSongForm.Song.SongID });
            }
            return oResponse;
        }

        [HttpGet]
        public ActionResult DeleteSongByID(long iSongID)
        {
            ActionResult oResponse = null;

            if (Session["Username"] == null)
            {
                //If not a user, redirect to welcome page
                oResponse = RedirectToAction("Index", "Home");
            }
            else if ((Int16)Session["Role"] == 1 || (Int16)Session["Role"] == 2)
            {
                //If User or Power User, redirect to View By ID
                oResponse = RedirectToAction("ViewSongByID", "Song", new { iSongID = iSongID });
            }
            else
            {
                //If Admin, allow process
                try
                {
                    //Method call to perform requested function
                    _SongDALAccess.DeleteSongByID(iSongID);
                    //Redirect user to View All
                    oResponse = RedirectToAction("Index", "Song");
                }
                catch (Exception ex)
                {
                    //Log error
                    using (StreamWriter fileWriter = new StreamWriter(@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\PLErrors\SongPLErrors.txt", true))
                    {
                        fileWriter.WriteLine("{0} - {1}", DateTime.Now.ToString("MM-dd-yyyy hh:ss"),
                            ex.Message);
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
                    //Nothing to do here
                }
            }
            return oResponse;
        }

        //Method to populate VM with Album and Artist dropdowns
        public void PopulateSongVM()
        {
            //Instantiate new list of Albums
            List<IAlbumDO> lListOfAlbums = _AlbumDALAccess.ViewAllAlbums();
            //Assign list of Albums to Song VM
            _SongVM.Song.DropdownAlbums = new SelectList(lListOfAlbums, "AlbumID", "Name");

            //Instantiate new list of Artists
            List<IArtistDO> lListOfArtists = _ArtistDALAccess.ViewAllArtists();
            //Assign list of Artists to Song VM
            _SongVM.Song.DropdownArtists = new SelectList(lListOfArtists, "ArtistID", "Name");
        }
    }
}