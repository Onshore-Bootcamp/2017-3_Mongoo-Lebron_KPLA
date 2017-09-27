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

    public class AlbumController : Controller
    {
        //Constructor
        public AlbumController()
        {
            PopulateAlbumVM();
        }

        //New instantiations of DAL classes for Album and Arist
        AlbumDataAccess _AlbumDALAccess = new AlbumDataAccess();
        ArtistDataAccess _ArtistDALAccess = new ArtistDataAccess();
        //New VM instantiation
        AlbumVM _AlbumVM = new AlbumVM();

        [HttpGet]
        public ActionResult AddNewAlbum()
        {
            ActionResult oResponse = null;

            if (Session["Username"] == null)
            {
                //If not a user, redirect to welcome page
                oResponse = RedirectToAction("Index", "Home");
            }
            else if ((Int16)Session["Role"] == 1 || (Int16)Session["Role"] == 2)
            {
                //If User or Power User, redirect to Veiw All Albums
                oResponse = RedirectToAction("Index", "Album");
            }
            else
            {
                //If Admin, allow Add New Album view
                try
                {
                    //Instantiate new list of Artists
                    List<IArtistDO> lListOfArtists = _ArtistDALAccess.ViewAllArtists();
                    //Assign list of Artists to Album VM
                    _AlbumVM.Album.DropdownArtists = new SelectList(lListOfArtists, "ArtistID", "Name");

                    oResponse = View(_AlbumVM);
                }
                catch (Exception ex)
                {
                    //Log exception
                    using (StreamWriter fileWriter = new StreamWriter(@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\PLErrors\AlbumPLErrors.txt", true))
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
        public ActionResult AddNewAlbum(AlbumVM iAlbumVM)
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
                oResponse = RedirectToAction("Index", "Album");
            }
            else
            {
                //If Admin, allow view
                if (ModelState.IsValid)
                {
                    try
                    {
                        //Method call to map form info from PO to DO
                        IAlbumDO lAlbumDO = MapAlbum.MapAlbumFromPOtoDO(iAlbumVM.Album);
                        //Method call to insert album into the DB
                        _AlbumDALAccess.AddNewAlbum(lAlbumDO);

                        oResponse = RedirectToAction("Index", "Album");
                    }
                    catch (Exception ex)
                    {
                        //Log exception
                        using (StreamWriter fileWriter = new StreamWriter(@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\PLErrors\AlbumPLErrors.txt", true))
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
                    oResponse = View(_AlbumVM);
                }
            }
            return oResponse;
        }

        //Method to View All Albums
        [HttpGet]
        public ActionResult Index()
        {
            ActionResult oResponse = null;
            try
            {
                //Make a data call
                List<IAlbumDO> lListOfAlbumDOs = _AlbumDALAccess.ViewAllAlbums();
                //Method call to map data
                List<AlbumPO> lListOfAlbumPOs = MapAlbum.MapListOfAlbumDOsToListOfPOs(lListOfAlbumDOs);
                //Assign list to VM
                _AlbumVM.ListOfAlbumPOs = lListOfAlbumPOs;

                //Foreach loop to assign artist's name to AlbumPO so we can display it
                foreach (AlbumPO album in _AlbumVM.ListOfAlbumPOs)
                {
                    album.ArtistName = _ArtistDALAccess.ViewArtistNameByID(album.ArtistID);
                }

                oResponse = View(_AlbumVM);
            }
            catch (Exception ex)
            {
                //Log error
                using (StreamWriter fileWriter = new StreamWriter(@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\PLErrors\AlbumPLErrors.txt", true))
                {
                    fileWriter.WriteLine("{0} - {1}", DateTime.Now.ToString("MM-dd-yyyy hh:ss"), ex.Message);
                }

                //Instantiate new Error object
                Error lError = new Error();
                //Populate object
                lError.ErrorHeader = "Oh no, something has gone wrong!";
                lError.ErrorMessage = "If error persists, please contact us with the details of your problem"
                    + " and we'll get back to you as soon as we can.";
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
        public ActionResult ViewAlbumByID(long iAlbumID)
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
                    IAlbumDO lAlbumDO = _AlbumDALAccess.ViewAlbumByID(iAlbumID);
                    //Method call to map data to a PO
                    AlbumPO lAlbumPO = MapAlbum.MapAlbumFromDOtoPO(lAlbumDO);

                    //Assign Artist's Name to AlbumPO so we can display it
                    lAlbumPO.ArtistName = _ArtistDALAccess.ViewArtistNameByID(lAlbumPO.ArtistID);

                    //Populate VM with info from data call
                    _AlbumVM.Album = lAlbumPO;

                    oResponse = View(_AlbumVM);
                }
                catch (Exception ex)
                {
                    //Log error
                    using (StreamWriter fileWriter = new StreamWriter(@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\PLErrors\AlbumPLErrors.txt", true))
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
        public ActionResult UpdateAlbumByID(long iAlbumID)
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
                    IAlbumDO lAlbumDO = _AlbumDALAccess.ViewAlbumByID(iAlbumID);
                    //Method call to map the DO to a PO
                    AlbumPO lAlbumPO = MapAlbum.MapAlbumFromDOtoPO(lAlbumDO);
                    //Populate VM with album info from data call
                    _AlbumVM.Album = lAlbumPO;

                    //Instantiate new list of Artists
                    List<IArtistDO> lListOfArtists = _ArtistDALAccess.ViewAllArtists();
                    //Assign list of Artists to Album VM
                    _AlbumVM.Album.DropdownArtists = new SelectList(lListOfArtists, "ArtistID", "Name");

                    oResponse = View(_AlbumVM);
                }
                catch (Exception ex)
                {
                    //Log error
                    using (StreamWriter fileWriter = new StreamWriter(@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\PLErrors\AlbumPLErrors.txt", true))
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
                oResponse = RedirectToAction("ViewAlbumByID", "Album", new { iAlbumID = iAlbumID });
            }
            return oResponse;
        }

        [HttpPost]
        public ActionResult UpdateAlbumByID(AlbumVM iAlbumForm)
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
                        IAlbumDO lUpdateAlbumForm = MapAlbum.MapAlbumFromPOtoDO(iAlbumForm.Album);
                        //Method call to execute procedure
                        _AlbumDALAccess.UpdateAlbumByID(lUpdateAlbumForm);

                        //Redirect user to View Details
                        oResponse = RedirectToAction("ViewAlbumByID", "Album",
                            new { iAlbumID = iAlbumForm.Album.AlbumID });
                    }
                    catch (Exception ex)
                    {
                        //Log error
                        using (StreamWriter fileWriter = new StreamWriter(@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\PLErrors\AlbumPLErrors.txt", true))
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
                    //If form is not filled out properly, return the view
                    oResponse = View(_AlbumVM);
                }
            }
            else
            {
                //If User, redirect to View Album By ID
                oResponse = RedirectToAction("ViewAlbumByID", "Album",
                    new { iAlbumID = iAlbumForm.Album.AlbumID });
            }
            return oResponse;
        }

        [HttpGet]
        public ActionResult DeleteAlbumByID(long iAlbumID)
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
                oResponse = RedirectToAction("ViewAlbumByID", "Album");
            }
            else
            {
                //If Admin, allow process
                try
                {
                    //Method call to perform requested function
                    _AlbumDALAccess.DeleteAlbumByID(iAlbumID);
                    //Redirect user to View All
                    oResponse = RedirectToAction("Index", "Album");
                }
                catch (Exception ex)
                {
                    //Log error
                    using (StreamWriter fileWriter = new StreamWriter(@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\PLErrors\AlbumPLErrors.txt", true))
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

        //Method to populate VM with Artist dropdown
        public void PopulateAlbumVM()
        {
            //Instantiate new list of Artists
            List<IArtistDO> lListOfArtists = _ArtistDALAccess.ViewAllArtists();
            //Assign list of Artists to Album VM
            _AlbumVM.Album.DropdownArtists = new SelectList(lListOfArtists, "ArtistID", "Name");
        }
    }
}