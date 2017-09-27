namespace Capstone_DAL
{
    using Interfaces;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;

    public class UserDataAccess
    {
        //Define the connection
        public string _ConnectionString = @"Server=ADMIN2-PC\SQLEXPRESS; Database=CapstoneDB; 
            Trusted_Connection=True;";

        //Method to add new user
        public void AddNewUser(IUserDO iUser)
        {
            try
            {
                //Using block for connection
                using (SqlConnection lConnectionToSql = new SqlConnection(_ConnectionString))
                {
                    //Using block for command
                    using (SqlCommand lCreateCommand = new SqlCommand("ADD_NEW_USER", lConnectionToSql))
                    {
                        try
                        {
                            //Define command properties
                            lCreateCommand.CommandType = CommandType.StoredProcedure;
                            lCreateCommand.CommandTimeout = 40;

                            //Username
                            lCreateCommand.Parameters.AddWithValue("@Username", iUser.Username);
                            //Role
                            lCreateCommand.Parameters.AddWithValue("@Role", iUser.Role);
                            //First Name
                            lCreateCommand.Parameters.AddWithValue("@FirstName", iUser.FirstName);
                            //Last Name
                            lCreateCommand.Parameters.AddWithValue("@LastName", iUser.LastName);
                            //Email Address
                            lCreateCommand.Parameters.AddWithValue("@EmailAddress", iUser.EmailAddress);
                            //Password
                            lCreateCommand.Parameters.AddWithValue("@Password", iUser.Password);
                            //Suspended
                            lCreateCommand.Parameters.AddWithValue("@Suspended", iUser.Suspended);

                            //Language
                            if (iUser.Language == null || iUser.Language == "")
                            {
                                //If user did not input a value for this property, default to null
                                lCreateCommand.Parameters.AddWithValue("@Language", DBNull.Value);
                            }
                            else
                            {
                                //If user entered a value, add that value
                                lCreateCommand.Parameters.AddWithValue("@Language", iUser.Language);
                            }

                            //Birthdate
                            if (iUser.Birthdate == null)
                            {
                                //If user did not input a value for this property, default to empty string
                                lCreateCommand.Parameters.AddWithValue("@Birthdate", DBNull.Value);
                            }
                            else
                            {
                                //If user entered value, add that value
                                lCreateCommand.Parameters.AddWithValue("@Birthdate", iUser.Birthdate);
                            }

                            //Favorite Songs
                            if (iUser.FavoriteSongs == null || iUser.FavoriteSongs == "")
                            {
                                //If user did not input a value for this property, default to null
                                lCreateCommand.Parameters.AddWithValue("@FavoriteSongs", DBNull.Value);
                            }
                            else
                            {
                                //If user entered value, add that value
                                lCreateCommand.Parameters.AddWithValue("@FavoriteSongs", iUser.FavoriteSongs);
                            }

                            //External Link
                            if (iUser.ExternalLink == null || iUser.ExternalLink == "")
                            {
                                //If user did not input a value for this property, default to null
                                lCreateCommand.Parameters.AddWithValue("@ExternalLink", DBNull.Value);
                            }
                            else
                            {
                                //If user entered value, add that value
                                lCreateCommand.Parameters.AddWithValue("@ExternalLink", iUser.ExternalLink);
                            }

                            //About Me Content
                            if (iUser.AboutMeContent == null || iUser.AboutMeContent == "")
                            {
                                //If user did not input a value for this property, default to null
                                lCreateCommand.Parameters.AddWithValue("@AboutMeContent", DBNull.Value);
                            }
                            else
                            {
                                //If user entered value, add that value
                                lCreateCommand.Parameters.AddWithValue("@AboutMeContent",
                                    iUser.AboutMeContent);
                            }

                            //Open connection to SQL
                            lConnectionToSql.Open();
                            //Execute command
                            lCreateCommand.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            //Do nothing
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Log error
                using (StreamWriter fileWriter = new StreamWriter
                    (@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\DALErrors\UserDALErrors.txt", true))
                {
                    fileWriter.WriteLine("{0} - {1}", DateTime.Now.ToString("MM-dd-yyyy hh:ss"), ex.Message);
                }
                //Throw error
                throw ex;
            }
            finally
            {
                //Do nothing
            }
        }

        //Method to view all users
        public List<IUserDO> ViewAllUsers()
        {
            //Instantiate new list
            List<IUserDO> oListOfUserDOs = new List<IUserDO>();
            try
            {
                //Using block for connection
                using (SqlConnection lConnectionToSql = new SqlConnection(_ConnectionString))
                {
                    //Using block for command
                    using (SqlCommand lViewAllCommand = new SqlCommand("VIEW_ALL_USERS", lConnectionToSql))
                    {
                        try
                        {
                            //Declare command parameters
                            lViewAllCommand.CommandType = CommandType.StoredProcedure;
                            lViewAllCommand.CommandTimeout = 40;

                            //Open connection
                            lConnectionToSql.Open();
                            //Using block for reader
                            using (SqlDataReader lReader = lViewAllCommand.ExecuteReader())
                            {
                                //Populate datatable from reader
                                while (lReader.Read())
                                {
                                    //New instantiation of a DO
                                    IUserDO lUserDO = new UserDO();

                                    //Assign values from reader to new DO
                                    //User ID
                                    lUserDO.UserID = lReader.GetInt64(0);
                                    //Username
                                    lUserDO.Username = lReader.GetString(1);
                                    //Role
                                    lUserDO.Role = lReader.GetByte(2);
                                    //First Name
                                    lUserDO.FirstName = lReader.GetString(3);
                                    //Last Name
                                    lUserDO.LastName = lReader.GetString(4);
                                    //Email Address
                                    lUserDO.EmailAddress = lReader.GetString(5);
                                    //Password
                                    lUserDO.Password = lReader.GetString(6);
                                    //Suspended
                                    lUserDO.Suspended = lReader.GetBoolean(12);

                                    //Language
                                    if (lReader.IsDBNull(7))
                                    {
                                        lUserDO.Language = DBNull.Value.ToString();
                                    }
                                    else
                                    {
                                        lUserDO.Language = lReader.GetString(7);
                                    }

                                    //Birthdate
                                    if (lReader.IsDBNull(8))
                                    {
                                        lUserDO.Birthdate = null;
                                    }
                                    else
                                    {
                                        lUserDO.Birthdate = lReader.GetDateTime(8);
                                    }

                                    //Favorite Songs
                                    if (lReader.IsDBNull(9))
                                    {
                                        lUserDO.FavoriteSongs = DBNull.Value.ToString();
                                    }
                                    else
                                    {
                                        lUserDO.FavoriteSongs = lReader.GetString(9);
                                    }

                                    //External Link
                                    if (lReader.IsDBNull(10))
                                    {
                                        lUserDO.ExternalLink = DBNull.Value.ToString();
                                    }
                                    else
                                    {
                                        lUserDO.ExternalLink = lReader.GetString(10);
                                    }

                                    //About Me Content
                                    if (lReader.IsDBNull(11))
                                    {
                                        lUserDO.AboutMeContent = DBNull.Value.ToString();
                                    }
                                    else
                                    {
                                        lUserDO.AboutMeContent = lReader.GetString(11);
                                    }

                                    //Populate list with DO
                                    oListOfUserDOs.Add(lUserDO);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            //Do nothing
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Log error
                using (StreamWriter fileWriter = new StreamWriter
                    (@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\DALErrors\UserDALErrors.txt", true))
                {
                    fileWriter.WriteLine("{0} - {1}", DateTime.Now.ToString("MM-dd-yyyy hh:ss"), ex.Message);
                }
                //Throw error
                throw ex;
            }
            finally
            {
                //Do nothing
            }
            return oListOfUserDOs;
        }

        //Method to view user by ID
        public IUserDO ViewUserByID(long iUserID)
        {
            //New instantiation of DO
            IUserDO oUserDO = new UserDO();
            try
            {
                //Using block for connection
                using (SqlConnection lConnectionToSql = new SqlConnection(_ConnectionString))
                {
                    //Using block for command
                    using (SqlCommand lViewByIDCommand = new SqlCommand("VIEW_USER_BY_ID", lConnectionToSql))
                    {
                        try
                        {
                            //Declare command parameters
                            lViewByIDCommand.CommandType = CommandType.StoredProcedure;
                            lViewByIDCommand.CommandTimeout = 40;
                            lViewByIDCommand.Parameters.AddWithValue("@UserID", iUserID);

                            //Open connection
                            lConnectionToSql.Open();
                            //Using block for reader
                            using (SqlDataReader lReader = lViewByIDCommand.ExecuteReader())
                            {
                                //Populate datatable from reader
                                while (lReader.Read())
                                {
                                    //Assign values from reader to new DO
                                    //ID
                                    oUserDO.UserID = lReader.GetInt64(0);
                                    //Username
                                    oUserDO.Username = lReader.GetString(1);
                                    //Role
                                    oUserDO.Role = lReader.GetByte(2);
                                    //First Name
                                    oUserDO.FirstName = lReader.GetString(3);
                                    //Last Name
                                    oUserDO.LastName = lReader.GetString(4);
                                    //Email Address
                                    oUserDO.EmailAddress = lReader.GetString(5);
                                    //Password
                                    oUserDO.Password = lReader.GetString(6);
                                    //Suspended
                                    oUserDO.Suspended = lReader.GetBoolean(12);

                                    //Language
                                    if (lReader.IsDBNull(7))
                                    {
                                        oUserDO.Language = DBNull.Value.ToString();
                                    }
                                    else
                                    {
                                        oUserDO.Language = lReader.GetString(7);
                                    }

                                    //Birthdate
                                    if (lReader.IsDBNull(8))
                                    {
                                        oUserDO.Birthdate = null;
                                    }
                                    else
                                    {
                                        oUserDO.Birthdate = lReader.GetDateTime(8);
                                    }

                                    //Favorite Songs
                                    if (lReader.IsDBNull(9))
                                    {
                                        oUserDO.FavoriteSongs = DBNull.Value.ToString();
                                    }
                                    else
                                    {
                                        oUserDO.FavoriteSongs = lReader.GetString(9);
                                    }

                                    //External Link
                                    if (lReader.IsDBNull(10))
                                    {
                                        oUserDO.ExternalLink = DBNull.Value.ToString();
                                    }
                                    else
                                    {
                                        oUserDO.ExternalLink = lReader.GetString(10);
                                    }

                                    //About Me Content
                                    if (lReader.IsDBNull(11))
                                    {
                                        oUserDO.AboutMeContent = DBNull.Value.ToString();
                                    }
                                    else
                                    {
                                        oUserDO.AboutMeContent = lReader.GetString(11);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            //Do nothing
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Log error
                using (StreamWriter fileWriter = new StreamWriter
                    (@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\DALErrors\UserDALErrors.txt", true))
                {
                    fileWriter.WriteLine("{0} - {1}", DateTime.Now.ToString("MM-dd-yyyy hh:ss"), ex.Message);
                }
                //Throw error
                throw ex;
            }
            finally
            {
                //Do nothing
            }
            return oUserDO;
        }

        //Method to obtain user by Username
        public IUserDO ObtainUserByUsername(string iUsername)
        {
            //New instantiation of DO
            IUserDO oUserDO = new UserDO();
            try
            {
                //Using block for connection
                using (SqlConnection lConnectionToSql = new SqlConnection(_ConnectionString))
                {
                    //Using block for command
                    using (SqlCommand lViewByUsernameCommand = new SqlCommand("VIEW_USER_BY_USERNAME",
                            lConnectionToSql))
                    {
                        try
                        {
                            //Declare command parameters
                            lViewByUsernameCommand.CommandType = CommandType.StoredProcedure;
                            lViewByUsernameCommand.CommandTimeout = 40;
                            lViewByUsernameCommand.Parameters.AddWithValue("@Username", iUsername);

                            //Open connection
                            lConnectionToSql.Open();
                            //Using block for reader
                            using (SqlDataReader lReader = lViewByUsernameCommand.ExecuteReader())
                            {
                                //Populate datatable from reader
                                while (lReader.Read())
                                {
                                    //Assign values from reader to new DO
                                    //ID
                                    oUserDO.UserID = lReader.GetInt64(0);
                                    //Username
                                    oUserDO.Username = lReader.GetString(1);
                                    //Role
                                    oUserDO.Role = lReader.GetByte(2);
                                    //First Name
                                    oUserDO.FirstName = lReader.GetString(3);
                                    //Last Name
                                    oUserDO.LastName = lReader.GetString(4);
                                    //Email Address
                                    oUserDO.EmailAddress = lReader.GetString(5);
                                    //Password
                                    oUserDO.Password = lReader.GetString(6);
                                    //Suspended
                                    oUserDO.Suspended = lReader.GetBoolean(12);

                                    //Language
                                    if (lReader.IsDBNull(7))
                                    {
                                        oUserDO.Language = DBNull.Value.ToString();
                                    }
                                    else
                                    {
                                        oUserDO.Language = lReader.GetString(7);
                                    }

                                    //Birthdate
                                    if (lReader.IsDBNull(8))
                                    {
                                        oUserDO.Birthdate = null;
                                    }
                                    else
                                    {
                                        oUserDO.Birthdate = lReader.GetDateTime(8);
                                    }

                                    //Favorite Songs
                                    if (lReader.IsDBNull(9))
                                    {
                                        oUserDO.FavoriteSongs = DBNull.Value.ToString();
                                    }
                                    else
                                    {
                                        oUserDO.FavoriteSongs = lReader.GetString(9);
                                    }

                                    //External Link
                                    if (lReader.IsDBNull(10))
                                    {
                                        oUserDO.ExternalLink = DBNull.Value.ToString();
                                    }
                                    else
                                    {
                                        oUserDO.ExternalLink = lReader.GetString(10);
                                    }

                                    //About Me Content
                                    if (lReader.IsDBNull(11))
                                    {
                                        oUserDO.AboutMeContent = DBNull.Value.ToString();
                                    }
                                    else
                                    {
                                        oUserDO.AboutMeContent = lReader.GetString(11);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            //Do nothing
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Log error
                using (StreamWriter fileWriter = new StreamWriter
                    (@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\DALErrors\UserDALErrors.txt", true))
                {
                    fileWriter.WriteLine("{0} - {1}", DateTime.Now.ToString("MM-dd-yyyy hh:ss"), ex.Message);
                }
                //Throw error
                throw ex;
            }
            finally
            {
                //Do nothing
            }

            if (oUserDO.Username == null)
            {
                //If user does not exist in DB, return null object
                oUserDO = null;
            }
            else
            {
                //If user exists in DB, return object with values assigned
            }
            return oUserDO;
        }

        //Method to update user by ID
        public void UpdateUserByID(IUserDO iUserDO)
        {
            try
            {
                //Using block for connection
                using (SqlConnection lConnectionToSql = new SqlConnection(_ConnectionString))
                {
                    //Using block for command
                    using (SqlCommand lUpdateCommand = new SqlCommand("UPDATE_USER_BY_ID", lConnectionToSql))
                    {
                        try
                        {
                            //Declare command parameters
                            lUpdateCommand.CommandType = CommandType.StoredProcedure;
                            lUpdateCommand.CommandTimeout = 40;
                            lUpdateCommand.Parameters.AddWithValue("@UserID", iUserDO.UserID);
                            lUpdateCommand.Parameters.AddWithValue("@Username", iUserDO.Username);
                            lUpdateCommand.Parameters.AddWithValue("@Role", iUserDO.Role);
                            lUpdateCommand.Parameters.AddWithValue("@FirstName", iUserDO.FirstName);
                            lUpdateCommand.Parameters.AddWithValue("@LastName", iUserDO.LastName);
                            lUpdateCommand.Parameters.AddWithValue("@EmailAddress", iUserDO.EmailAddress);
                            lUpdateCommand.Parameters.AddWithValue("@Password", iUserDO.Password);
                            lUpdateCommand.Parameters.AddWithValue("@Language", iUserDO.Language);
                            lUpdateCommand.Parameters.AddWithValue("@Birthdate", iUserDO.Birthdate);
                            lUpdateCommand.Parameters.AddWithValue("@FavoriteSongs", iUserDO.FavoriteSongs);
                            lUpdateCommand.Parameters.AddWithValue("@ExternalLink", iUserDO.ExternalLink);
                            lUpdateCommand.Parameters.AddWithValue("@AboutMeContent", iUserDO.AboutMeContent);
                            lUpdateCommand.Parameters.AddWithValue("@Suspended", iUserDO.Suspended);

                            //Open connection
                            lConnectionToSql.Open();
                            //Execute update command
                            lUpdateCommand.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            //Do nothing
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Log error
                using (StreamWriter fileWriter = new StreamWriter
                    (@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\DALErrors\UserDALErrors.txt", true))
                {
                    fileWriter.WriteLine("{0} - {1}", DateTime.Now.ToString("MM-dd-yyyy hh:ss"), ex.Message);
                }
                //Throw error
                throw ex;
            }
            finally
            {
                //Do nothing
            }
        }

        //Method to delete user by ID
        public void DeleteUserByID(long iUserID)
        {
            try
            {
                //Using block for connection
                using (SqlConnection lConnectionToSql = new SqlConnection(_ConnectionString))
                {
                    //Using block for command
                    using (SqlCommand lDeleteCommand = new SqlCommand("DELETE_USER_BY_ID", lConnectionToSql))
                    {
                        try
                        {
                            //Declare command parameters
                            lDeleteCommand.CommandType = CommandType.StoredProcedure;
                            lDeleteCommand.CommandTimeout = 40;
                            lDeleteCommand.Parameters.AddWithValue("@UserID", iUserID);

                            //Open connection
                            lConnectionToSql.Open();
                            //Execute delete command
                            lDeleteCommand.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            //Do nothing
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Log error
                using (StreamWriter fileWriter = new StreamWriter
                    (@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\DALErrors\UserDALErrors.txt", true))
                {
                    fileWriter.WriteLine("{0} - {1}", DateTime.Now.ToString("MM-dd-yyyy hh:ss"), ex.Message);
                }
                //Throw error
                throw ex;
            }
            finally
            {
                //Do nothing
            }
        }
    }
}