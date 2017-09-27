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

    public class ArtistController : Controller
    {
        //New instantiation of DAL class
        ArtistDataAccess _ArtistDALAccess = new ArtistDataAccess();

        //Method to View All Artists
        [HttpGet]
        public ActionResult Index()
        {
            ActionResult oResponse = null;
            try
            {
                //Instantiate a new VM
                ArtistVM oViewAllArtistsVM = new ArtistVM();

                //Make a data call
                List<IArtistDO> lListOfArtistDOs = _ArtistDALAccess.ViewAllArtists();
                //Method call to map data
                List<ArtistPO> lListOfArtistPOs = MapArtist.MapListOfArtistDOsToListOfPOs(lListOfArtistDOs);
                //Assign list to VM
                oViewAllArtistsVM.ListOfArtistPOs = lListOfArtistPOs;

                oResponse = View(oViewAllArtistsVM);
            }
            catch (Exception ex)
            {
                //Log error
                using (StreamWriter fileWriter = new StreamWriter(@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\PLErrors\ArtistPLErrors.txt", true))
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

        [HttpGet]
        public ActionResult AddNewArtist()
        {
            ActionResult oResponse = null;

            if (Session["Username"] == null)
            {
                //If not a user, redirect to welcome page
                oResponse = RedirectToAction("Index", "Home");
            }
            else if ((Int16)Session["Role"] == 1 || (Int16)Session["Role"] == 2)
            {
                //If User or Power User, redirect to Veiw All Artists
                oResponse = RedirectToAction("Index", "Artist");
            }
            else
            {
                //If Admin, allow Add New Artist view
                try
                {
                    //New VM instantiation
                    ArtistVM oArtistVM = new ArtistVM();
                    oResponse = View(oArtistVM);
                }
                catch (Exception ex)
                {
                    //Log exception
                    using (StreamWriter fileWriter = new StreamWriter(@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\PLErrors\ArtistPLErrors.txt", true))
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
        public ActionResult AddNewArtist(ArtistVM iArtistVM)
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
                oResponse = RedirectToAction("Index", "Artist");
            }
            else
            {
                //If Admin, allow view
                if (ModelState.IsValid)
                {
                    try
                    {
                        //Method call to map form info from PO to DO
                        IArtistDO lArtistDO = MapArtist.MapArtistPOtoDO(iArtistVM.Artist);
                        //Method call to insert artist into the DB
                        _ArtistDALAccess.AddNewArtist(lArtistDO);
                        //Redirect to View All Artists
                        oResponse = RedirectToAction("Index", "Artist");
                    }
                    catch (Exception ex)
                    {
                        //Log exception
                        using (StreamWriter fileWriter = new StreamWriter(@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\PLErrors\ArtistPLErrors.txt", true))
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
                    //If ModelState is not valid, return filled out form and try again
                    oResponse = View(iArtistVM);
                }
            }
            return oResponse;
        }

        [HttpGet]
        public ActionResult ViewArtistByID(long iArtistID)
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
                    //Instantiate new view model
                    ArtistVM oArtistVM = new ArtistVM();

                    //Make a data call
                    IArtistDO lArtistDO = _ArtistDALAccess.ViewArtistByID(iArtistID);
                    //Method call to map data to a PO
                    ArtistPO lArtistPO = MapArtist.MapArtistDOtoPO(lArtistDO);
                    //Populate VM with info from data call
                    oArtistVM.Artist = lArtistPO;

                    oResponse = View(oArtistVM);
                }
                catch (Exception ex)
                {
                    //Log error
                    using (StreamWriter fileWriter = new StreamWriter(@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\PLErrors\ArtistPLErrors.txt", true))
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
        public ActionResult UpdateArtistByID(long iArtistID)
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
                    //Instantiate a new VM
                    ArtistVM lArtistVM = new ArtistVM();

                    //Make a data call to get the DO
                    IArtistDO lArtistDO = _ArtistDALAccess.ViewArtistByID(iArtistID);
                    //Method call to map the DO to a PO
                    ArtistPO lArtistPO = MapArtist.MapArtistDOtoPO(lArtistDO);
                    //Populate VM with artist info from data call
                    lArtistVM.Artist = lArtistPO;

                    oResponse = View(lArtistVM);
                }
                catch (Exception ex)
                {
                    //Log error
                    using (StreamWriter fileWriter = new StreamWriter(@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\PLErrors\ArtistPLErrors.txt", true))
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
                //If User, redirect to View Artist By ID
                oResponse = RedirectToAction("ViewArtistByID", "Artist", new { iArtistID = iArtistID });
            }
            return oResponse;
        }

        [HttpPost]
        public ActionResult UpdateArtistByID(ArtistVM iArtistForm)
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
                        IArtistDO lUpdateArtistForm = MapArtist.MapArtistPOtoDO(iArtistForm.Artist);
                        //Method call to execute procedure
                        _ArtistDALAccess.UpdateArtistByID(lUpdateArtistForm);

                        //Redirect user to View Details
                        oResponse = RedirectToAction("ViewArtistByID", "Artist",
                            new { iArtistID = iArtistForm.Artist.ArtistID });
                    }
                    catch (Exception ex)
                    {
                        //Log error
                        using (StreamWriter fileWriter = new StreamWriter(@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\PLErrors\ArtistPLErrors.txt", true))
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
                    oResponse = View(iArtistForm);
                }
            }
            else
            {
                //If User, redirect to View Artist By ID
                oResponse = RedirectToAction("ViewArtistByID", "Artist",
                    new { iArtistID = iArtistForm.Artist.ArtistID });
            }
            return oResponse;
        }

        [HttpGet]
        public ActionResult DeleteArtistByID(long iArtistID)
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
                oResponse = RedirectToAction("ViewArtistByID", "Artist", new { iArtistID = iArtistID });
            }
            else
            {
                //If Admin, allow process
                try
                {
                    //Method call to perform requested function
                    _ArtistDALAccess.DeleteArtistByID(iArtistID);
                    //Redirect user to View All
                    oResponse = RedirectToAction("Index", "Artist");
                }
                catch (Exception ex)
                {
                    //Log error
                    using (StreamWriter fileWriter = new StreamWriter(@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\PLErrors\ArtistPLErrors.txt", true))
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
    }
}