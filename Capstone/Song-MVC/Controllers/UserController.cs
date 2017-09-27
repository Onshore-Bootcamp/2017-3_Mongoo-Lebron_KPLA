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

    public class UserController : Controller
    {
        //New instantiation of DAL class
        UserDataAccess _UserDALAccess = new UserDataAccess();

        //Method to View All Users
        [HttpGet]
        public ActionResult Index()
        {
            ActionResult oResponse = null;

            if (Session["Username"] != null)
            {
                //If user is logged in, allow view
                try
                {
                    //Instantiate new VM
                    UserVM oViewAllUsersVM = new UserVM();

                    //Made a data call
                    List<IUserDO> lListOfUserDOs = _UserDALAccess.ViewAllUsers();
                    //Method call to map data
                    List<UserPO> lListOfUserPOs = MapUser.MapListOfUserDOsToListOfPOs(lListOfUserDOs);
                    //Assign list to VM
                    oViewAllUsersVM.ListOfUserPOs = lListOfUserPOs;

                    oResponse = View(oViewAllUsersVM);
                }
                catch (Exception ex)
                {
                    //Log error
                    using (StreamWriter fileWriter = new StreamWriter(@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\PLErrors\UserPLErrors.txt", true))
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
                //If user is not logged in, redirect to Login page
                oResponse = RedirectToAction("Login", "User");
            }
            return oResponse;
        }

        [HttpGet]
        public ActionResult AddNewUser()
        {
            ActionResult oResponse = null;

            if (Session["Username"] == null || (Int16)Session["Role"] == 3)
            {
                //If guest or Admin, allow view
                try
                {
                    //Instantiate new VM
                    UserVM oUserVM = new UserVM();
                    oResponse = View(oUserVM);
                }
                catch (Exception ex)
                {
                    //Log exception
                    using (StreamWriter fileWriter = new StreamWriter(@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\PLErrors\UserPLErrors.txt", true))
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
                //If User or Power User, redirect to View All Users
                oResponse = RedirectToAction("Index", "User");
            }
            return oResponse;
        }

        [HttpPost]
        public ActionResult AddNewUser(UserVM iUserForm)
        {
            ActionResult oResponse = null;

            if (Session["Username"] == null || (Int16)Session["Role"] == 3)
            {
                //If guest or Admin, allow process
                if (ModelState.IsValid)
                {
                    try
                    {
                        //Method call to map form info from PO to DO
                        IUserDO lUserDO = MapUser.MapUserPOtoDO(iUserForm.User);

                        //Default role to User
                        lUserDO.Role = 1;
                        //Default Suspended property to False
                        lUserDO.Suspended = false;

                        //Method call to insert user into the DB
                        _UserDALAccess.AddNewUser(lUserDO);
                        //Method call to obtain new user from DB
                        IUserDO tempUserDO = _UserDALAccess.ObtainUserByUsername(lUserDO.Username);

                        if (Session["Username"] == null)
                        {
                            //If guest is signing up, set Session
                            Session["Username"] = tempUserDO.Username;
                            Session["Role"] = tempUserDO.Role;
                            Session["UserID"] = tempUserDO.UserID;
                            Session.Timeout = 10;
                        }
                        else
                        {
                            //If Admin added user, do not set Session
                        }

                        //Redirect to Profile
                        oResponse = RedirectToAction("ViewUserByID", "User",
                            new { iUserID = tempUserDO.UserID });
                    }
                    catch (Exception ex)
                    {
                        //Log exception
                        using (StreamWriter fileWriter = new StreamWriter(@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\PLErrors\UserPLErrors.txt", true))
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
                        //Nothing needed here
                    }
                }
                else
                {
                    //If ModelState is not valid, return filled out form and try again
                    oResponse = View(iUserForm);
                }
            }
            else
            {
                //If User or Power User, redirect to View All Users
                oResponse = RedirectToAction("Index", "User");
            }
            return oResponse;
        }

        [HttpGet]
        public ActionResult ViewUserByID(long iUserID)
        {
            ActionResult oResponse = null;

            if (Session["Username"] == null)
            {
                //If guest, redirect to welcome page
                oResponse = RedirectToAction("Index", "Home");
            }
            else
            {
                //If user, allow view              
                try
                {
                    //Instantiate new view model
                    UserVM userVM = new UserVM();

                    //Make a data call
                    IUserDO lUserDO = _UserDALAccess.ViewUserByID(iUserID);
                    //Method call to map data to a PO
                    UserPO lUserPO = MapUser.MapUserDOtoPO(lUserDO);
                    //Populate VM with info from data call
                    userVM.User = lUserPO;

                    oResponse = View(userVM);
                }
                catch (Exception ex)
                {
                    //Log error
                    using (StreamWriter fileWriter = new StreamWriter(@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\PLErrors\UserPLErrors.txt", true))
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
        public ActionResult UpdateUserByID(long iUserID)
        {
            ActionResult oResponse = null;
            try
            {
                //Make a data call to get a DO for UserID
                IUserDO lTempUserDO = _UserDALAccess.ViewUserByID(iUserID);

                if (Session["Username"] == null)
                {
                    //If guest, redirect to welcome page
                    oResponse = RedirectToAction("Index", "Home");
                }
                else if ((Int64)Session["UserID"] == iUserID ||
                    (Int16)Session["Role"] == 2 && lTempUserDO.Role == 1 ||
                    (Int16)Session["Role"] == 3)
                {
                    //If user on own profile, Power User (on User profile), or Admin, allow process
                    try
                    {
                        //Instantiate new VM
                        UserVM lUserVM = new UserVM();

                        //Make a data call to get the DO
                        IUserDO lUserDO = _UserDALAccess.ViewUserByID(iUserID);
                        //Method call to map the DO to a PO
                        UserPO lUserPO = MapUser.MapUserDOtoPO(lUserDO);
                        //Populate VM with user info from data call
                        lUserVM.User = lUserPO;

                        oResponse = View(lUserVM);
                    }
                    catch (Exception ex)
                    {
                        //Throw error to outer try/catch
                        throw ex;
                    }
                    finally
                    {
                        //Nothing needed here
                    }
                }
                else
                {
                    //If User(not on own profile) or Power User(on PU or Admin profile), redirect to Profile
                    oResponse = RedirectToAction("ViewUserByID", "User", new { iUserID = iUserID });
                }
            }
            catch (Exception ex)
            {
                //Log error
                using (StreamWriter fileWriter = new StreamWriter(@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\PLErrors\UserPLErrors.txt", true))
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

        [HttpPost]
        public ActionResult UpdateUserByID(UserVM iUserForm)
        {
            ActionResult oResponse = null;

            //If User or Power User, default to previous values
            if ((Int16)Session["Role"] == 1 || (Int16)Session["Role"] == 2)
            {
                //Clear password error to set ModelState as valid
                if (ModelState.ContainsKey("User.Password"))
                {
                    ModelState["User.Password"].Errors.Clear();
                }
                else
                {
                    //If ModelState does not contain key, do nothing
                }

                //Set value for user's password and role
                try
                {
                    //Pull up user's information from the DB
                    IUserDO lUserDO = _UserDALAccess.ViewUserByID(iUserForm.User.UserID);
                    //Assign user's role
                    iUserForm.User.Role = lUserDO.Role;
                    //Assign user's password
                    iUserForm.User.Password = lUserDO.Password;
                }
                catch (Exception ex)
                {
                    //Log error
                    using (StreamWriter fileWriter = new StreamWriter(@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\PLErrors\UserPLErrors.txt", true))
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
                //If Admin, default nothing
            }

            if (Session["Username"] == null)
            {
                //If guest, redirect to welcome page
                oResponse = RedirectToAction("Index", "Home");
            }
            else if ((Int64)Session["UserID"] == iUserForm.User.UserID ||
                (Int16)Session["Role"] == 2 && iUserForm.User.Role == 1 ||
                (Int16)Session["Role"] == 3)
            {
                //If user on own profile, Power User (on User profile), or Admin, allow process
                if (ModelState.IsValid)
                {
                    try
                    {
                        //Method call to map from PO to DO
                        IUserDO lUpdateUserForm = MapUser.MapUserPOtoDO(iUserForm.User);
                        //Method call to exectue stored procedure
                        _UserDALAccess.UpdateUserByID(lUpdateUserForm);

                        //Redirect to View Details
                        oResponse = RedirectToAction("ViewUserByID", "User",
                            new { iUserID = iUserForm.User.UserID });
                    }
                    catch (Exception ex)
                    {
                        //Log error
                        using (StreamWriter fileWriter = new StreamWriter(@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\PLErrors\UserPLErrors.txt", true))
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
                    //If ModelState is not valid, return view with user input
                    oResponse = View(iUserForm);
                }
            }
            else
            {
                //If User(not on own profile) or Power User (on PU or Admin profile), redirect to Profile
                oResponse = RedirectToAction("ViewUserByID", "User",
                    new { iUserID = iUserForm.User.UserID });
            }
            return oResponse;
        }

        [HttpGet]
        public ActionResult DeleteUserByID(long iUserID)
        {
            ActionResult oResponse = null;

            if (Session["Username"] == null)
            {
                //If guest, redirect to welcome page
                oResponse = RedirectToAction("Index", "Home");
            }
            else if ((Int16)Session["Role"] == 3 && (Int64)Session["UserID"] != iUserID)
            {
                //If Admin(not on own profile), allow process
                try
                {
                    //Method call to perform requested function
                    _UserDALAccess.DeleteUserByID(iUserID);
                    //Redirect user to View All
                    oResponse = RedirectToAction("Index", "User");
                }
                catch (Exception ex)
                {
                    //Log error
                    using (StreamWriter fileWriter = new StreamWriter(@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\PLErrors\UserPLErrors.txt", true))
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
                //If not Admin, redirect to Profile
                oResponse = RedirectToAction("ViewUserByID", "User", new { iUserID = iUserID });
            }
            return oResponse;
        }

        [HttpGet]
        public ActionResult Login()
        {
            ActionResult oResponse = null;

            if (Session["Username"] == null)
            {
                //If not logged in, allow view instantiation
                UserVM oUserVM = new UserVM();
                oResponse = View(oUserVM);
            }
            else
            {
                //If logged in, redirect to profile
                oResponse = RedirectToAction("ViewUserByID", "User", new { iUserID = Session["UserID"] });
            }
            return oResponse;
        }

        [HttpPost]
        public ActionResult Login(UserVM iUserForm)
        {
            ActionResult oResponse = null;

            if (Session["Username"] == null)
            {
                //If not logged in, allow process
                if (ModelState.IsValid)
                {
                    //If all fields are filled out correctly, allow process
                    try
                    {
                        //Method call to obtain User DO by username
                        IUserDO lUserDO = _UserDALAccess.ObtainUserByUsername(iUserForm.LoginUser.Username);

                        if (lUserDO != null && lUserDO.Password.Equals(iUserForm.LoginUser.Password))
                        {
                            //If username is correct and password is correct
                            //Set Session
                            Session["Username"] = lUserDO.Username;
                            Session["Role"] = lUserDO.Role;
                            Session["UserID"] = lUserDO.UserID;
                            Session.Timeout = 10;

                            oResponse = RedirectToAction("ViewUserByID", "User",
                                new { iUserID = lUserDO.UserID });
                        }
                        else
                        {
                            //Return form to user
                            iUserForm.LoginUser.IncorrectMessage = "Username or Password is wrong.";
                            oResponse = View(iUserForm);
                        }
                    }
                    catch (Exception ex)
                    {
                        //Log error
                        using (StreamWriter fileWriter = new StreamWriter(@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\PLErrors\UserPLErrors.txt", true))
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
                    //If ModelState is not valid, return form to user
                    oResponse = View(iUserForm);
                }
            }
            else
            {
                //If logged in, redirect to profile
                oResponse = RedirectToAction("ViewUserByID", "User", new { iUserID = Session["UserID"] });
            }
            return oResponse;
        }

        [HttpGet]
        public ActionResult Logout()
        {
            ActionResult oResponse = null;

            if (Session["Username"] == null)
            {
                //If guest, redirect to welcome page
                oResponse = RedirectToAction("Index", "Home");
            }
            else
            {
                //If user, allow process
                //Abandon Session
                Session.Abandon();

                oResponse = View();
            }
            return oResponse;
        }
    }
}