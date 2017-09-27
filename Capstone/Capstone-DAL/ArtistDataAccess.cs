namespace Capstone_DAL
{
    using Interfaces;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;

    public class ArtistDataAccess
    {
        //Define the connection
        public string _ConnectionString = @"Server=ADMIN2-PC\SQLEXPRESS; Database=CapstoneDB; 
            Trusted_Connection=True;";

        //Method to add new artist
        public void AddNewArtist(IArtistDO iArtist)
        {
            try
            {
                //Using block for connection
                using (SqlConnection lConnectionToSql = new SqlConnection(_ConnectionString))
                {
                    //Using block for command
                    using (SqlCommand lCreateCommand = new SqlCommand("ADD_NEW_ARTIST", lConnectionToSql))
                    {
                        try
                        {
                            //Define command properties
                            lCreateCommand.CommandType = CommandType.StoredProcedure;
                            lCreateCommand.CommandTimeout = 40;

                            //Name
                            lCreateCommand.Parameters.AddWithValue("@Name", iArtist.Name);
                            //Number of Members
                            lCreateCommand.Parameters.AddWithValue("@NumberOfMembers",
                                iArtist.NumberOfMembers);
                            //Status
                            lCreateCommand.Parameters.AddWithValue("@Status", iArtist.Status);
                            //Years Active
                            lCreateCommand.Parameters.AddWithValue("@YearsActive", iArtist.YearsActive);

                            //Genre
                            if (iArtist.Genre == null || iArtist.Genre == "")
                            {
                                //If user did not input a value for this property, default to null
                                lCreateCommand.Parameters.AddWithValue("@Genre", DBNull.Value);
                            }
                            else
                            {
                                //if user entered a value, add that value
                                lCreateCommand.Parameters.AddWithValue("@Genre", iArtist.Genre);
                            }

                            //Picture URL
                            if (iArtist.PictureURL == null || iArtist.PictureURL == "")
                            {
                                //If user didn't input a value for this property, default to null
                                lCreateCommand.Parameters.AddWithValue("@PictureURL", DBNull.Value);
                            }
                            else
                            {
                                //if user entered a value, add that value
                                lCreateCommand.Parameters.AddWithValue("@PictureURL", iArtist.PictureURL);
                            }

                            //Company
                            if (iArtist.Company == null || iArtist.Company == "")
                            {
                                //If user didn't input a value for this property, default to null
                                lCreateCommand.Parameters.AddWithValue("@Company", DBNull.Value);
                            }
                            else
                            {
                                //if user entered a value, add that value
                                lCreateCommand.Parameters.AddWithValue("@Company", iArtist.Company);
                            }

                            //Gaon Awards
                            if (iArtist.GaonAwards == null || iArtist.GaonAwards == "")
                            {
                                //If user didn't input a value for this property, default to null
                                lCreateCommand.Parameters.AddWithValue("@GaonAwards", DBNull.Value);
                            }
                            else
                            {
                                //if user entered a value, add that value
                                lCreateCommand.Parameters.AddWithValue("@GaonAwards", iArtist.GaonAwards);
                            }

                            //External Link
                            if (iArtist.ExternalLink == null || iArtist.ExternalLink == "")
                            {
                                //If user didn't input a value for this property, default to null
                                lCreateCommand.Parameters.AddWithValue("@ExternalLink", DBNull.Value);
                            }
                            else
                            {
                                //if user entered a value, add that value
                                lCreateCommand.Parameters.AddWithValue("@ExternalLink", iArtist.ExternalLink);
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
                    (@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\DALErrors\ArtistDALErrors.txt", true))
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

        //Method to view all artists
        public List<IArtistDO> ViewAllArtists()
        {
            //Instantiate new list
            List<IArtistDO> oListOfArtistDOs = new List<IArtistDO>();
            try
            {
                //Using block for connection
                using (SqlConnection lConnectionToSql = new SqlConnection(_ConnectionString))
                {
                    //Using block for command
                    using (SqlCommand lViewAllCommand = new SqlCommand("VIEW_ALL_ARTISTS", lConnectionToSql))
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
                                    IArtistDO lArtistDO = new ArtistDO();

                                    //Assign values from reader to new DO
                                    //Artist ID
                                    lArtistDO.ArtistID = lReader.GetInt64(0);
                                    //Name
                                    lArtistDO.Name = lReader.GetString(1);
                                    //Number of Members
                                    lArtistDO.NumberOfMembers = lReader.GetInt16(3);
                                    //Status
                                    lArtistDO.Status = lReader.GetString(5);
                                    //Years Active
                                    lArtistDO.YearsActive = lReader.GetInt16(6);

                                    //Genre
                                    if (lReader.IsDBNull(2))
                                    {
                                        lArtistDO.Genre = DBNull.Value.ToString();
                                    }
                                    else
                                    {
                                        lArtistDO.Genre = lReader.GetString(2);
                                    }

                                    //Picture URL
                                    if (lReader.IsDBNull(4))
                                    {
                                        lArtistDO.PictureURL = DBNull.Value.ToString();
                                    }
                                    else
                                    {
                                        lArtistDO.PictureURL = lReader.GetString(4);
                                    }

                                    //Company
                                    if (lReader.IsDBNull(7))
                                    {
                                        lArtistDO.Company = DBNull.Value.ToString();
                                    }
                                    else
                                    {
                                        lArtistDO.Company = lReader.GetString(7);
                                    }

                                    //Gaon Awards
                                    if (lReader.IsDBNull(8))
                                    {
                                        lArtistDO.GaonAwards = DBNull.Value.ToString();
                                    }
                                    else
                                    {
                                        lArtistDO.GaonAwards = lReader.GetString(8);
                                    }

                                    //External Link
                                    if (lReader.IsDBNull(9))
                                    {
                                        lArtistDO.ExternalLink = DBNull.Value.ToString();
                                    }
                                    else
                                    {
                                        lArtistDO.ExternalLink = lReader.GetString(9);
                                    }

                                    //Populate list with DO
                                    oListOfArtistDOs.Add(lArtistDO);
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
                    (@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\DALErrors\ArtistDALErrors.txt", true))
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
            return oListOfArtistDOs;
        }

        //Method to view artist by ID
        public IArtistDO ViewArtistByID(long iArtistID)
        {
            //New instantiation of DO
            IArtistDO oArtistDO = new ArtistDO();
            try
            {
                //Using block for connection
                using (SqlConnection lConnectionToSql = new SqlConnection(_ConnectionString))
                {
                    //Using block for command
                    using (SqlCommand lViewCommand = new SqlCommand("VIEW_ARTIST_BY_ID", lConnectionToSql))
                    {
                        try
                        {
                            //Declare command parameters
                            lViewCommand.CommandType = CommandType.StoredProcedure;
                            lViewCommand.CommandTimeout = 40;
                            lViewCommand.Parameters.AddWithValue("@ArtistID", iArtistID);

                            //Open connection
                            lConnectionToSql.Open();
                            //Using block for reader
                            using (SqlDataReader lReader = lViewCommand.ExecuteReader())
                            {
                                //Populate datatable from reader
                                while (lReader.Read())
                                {
                                    //Assign values from reader to new DO
                                    //ID
                                    oArtistDO.ArtistID = lReader.GetInt64(0);
                                    //Name
                                    oArtistDO.Name = lReader.GetString(1);
                                    //Number Of Members
                                    oArtistDO.NumberOfMembers = lReader.GetInt16(3);
                                    //Status
                                    oArtistDO.Status = lReader.GetString(5);
                                    //Years Active
                                    oArtistDO.YearsActive = lReader.GetInt16(6);

                                    //Genre
                                    if (lReader.IsDBNull(2))
                                    {
                                        oArtistDO.Genre = DBNull.Value.ToString();
                                    }
                                    else
                                    {
                                        oArtistDO.Genre = lReader.GetString(2);
                                    }

                                    //Picture URL
                                    if (lReader.IsDBNull(4))
                                    {
                                        oArtistDO.PictureURL = DBNull.Value.ToString();
                                    }
                                    else
                                    {
                                        oArtistDO.PictureURL = lReader.GetString(4);
                                    }

                                    //Company
                                    if (lReader.IsDBNull(7))
                                    {
                                        oArtistDO.Company = DBNull.Value.ToString();
                                    }
                                    else
                                    {
                                        oArtistDO.Company = lReader.GetString(7);
                                    }

                                    //GaonAwards
                                    if (lReader.IsDBNull(8))
                                    {
                                        oArtistDO.GaonAwards = DBNull.Value.ToString();
                                    }
                                    else
                                    {
                                        oArtistDO.GaonAwards = lReader.GetString(8);
                                    }

                                    //External Link
                                    if (lReader.IsDBNull(9))
                                    {
                                        oArtistDO.ExternalLink = DBNull.Value.ToString();
                                    }
                                    else
                                    {
                                        oArtistDO.ExternalLink = lReader.GetString(9);
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
                    (@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\DALErrors\ArtistDALErrors.txt", true))
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
            return oArtistDO;
        }

        //Method to view artist name by ID
        public string ViewArtistNameByID(long iArtistID)
        {
            //Declare var to return
            string oArtistName = null;

            try
            {
                //Using block for connection
                using (SqlConnection lConnectionToSql = new SqlConnection(_ConnectionString))
                {
                    //Using block for command
                    using (SqlCommand lViewCommand = new SqlCommand("VIEW_ARTIST_BY_ID", lConnectionToSql))
                    {
                        try
                        {
                            //Declare command parameters
                            lViewCommand.CommandType = CommandType.StoredProcedure;
                            lViewCommand.CommandTimeout = 40;
                            lViewCommand.Parameters.AddWithValue("@ArtistID", iArtistID);

                            //Open connection
                            lConnectionToSql.Open();
                            //Using block for reader
                            using (SqlDataReader lReader = lViewCommand.ExecuteReader())
                            {
                                //Populate datatable from reader
                                while (lReader.Read())
                                {
                                    //Assign value from reader to string
                                    //Name
                                    oArtistName = lReader.GetString(1);
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
                    (@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\DALErrors\ArtistDALErrors.txt", true))
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
            return oArtistName;
        }

        //Method to update an artist by ID
        public void UpdateArtistByID(IArtistDO iArtistDO)
        {
            try
            {
                //Using block for connection
                using (SqlConnection lConnectionToSql = new SqlConnection(_ConnectionString))
                {
                    //Using block for command
                    using (SqlCommand lUpdateCommand = new SqlCommand("UPDATE_ARTIST_BY_ID", lConnectionToSql))
                    {
                        try
                        {
                            //Declare command parameters
                            lUpdateCommand.CommandType = CommandType.StoredProcedure;
                            lUpdateCommand.CommandTimeout = 40;
                            lUpdateCommand.Parameters.AddWithValue("@ArtistID", iArtistDO.ArtistID);
                            lUpdateCommand.Parameters.AddWithValue("@Name", iArtistDO.Name);
                            lUpdateCommand.Parameters.AddWithValue("@Genre", iArtistDO.Genre);
                            lUpdateCommand.Parameters.AddWithValue("@NumberOfMembers",
                                iArtistDO.NumberOfMembers);
                            lUpdateCommand.Parameters.AddWithValue("@PictureURL", iArtistDO.PictureURL);
                            lUpdateCommand.Parameters.AddWithValue("@Status", iArtistDO.Status);
                            lUpdateCommand.Parameters.AddWithValue("@YearsActive", iArtistDO.YearsActive);
                            lUpdateCommand.Parameters.AddWithValue("@Company", iArtistDO.Company);
                            lUpdateCommand.Parameters.AddWithValue("@GaonAwards", iArtistDO.GaonAwards);
                            lUpdateCommand.Parameters.AddWithValue("@ExternalLink", iArtistDO.ExternalLink);

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
                    (@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\DALErrors\ArtistDALErrors.txt", true))
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

        //Method to delete an artist by ID
        public void DeleteArtistByID(long iArtistID)
        {
            try
            {
                //Using block for connection
                using (SqlConnection lConnectionToSql = new SqlConnection(_ConnectionString))
                {
                    //Using block for command
                    using (SqlCommand lDeleteCommand = new SqlCommand("DELETE_ARTIST_BY_ID", lConnectionToSql))
                    {
                        try
                        {
                            //Declare command parameters
                            lDeleteCommand.CommandType = CommandType.StoredProcedure;
                            lDeleteCommand.CommandTimeout = 40;
                            lDeleteCommand.Parameters.AddWithValue("@ArtistID", iArtistID);

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
                    (@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\DALErrors\ArtistDALErrors.txt", true))
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

        //Method to view all artists' number of members
        public List<short> ViewAllArtistsNumberOfMembers()
        {
            //New list instantiation
            List<short> oListOfNumOfMembers = new List<short>();

            try
            {
                //Using block for connection
                using (SqlConnection lConnectionToSql = new SqlConnection(_ConnectionString))
                {
                    //Using block for command
                    using (SqlCommand lViewAllCommand = new SqlCommand("VIEW_ALL_ARTISTS", lConnectionToSql))
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
                                    //Assign values from reader to new short
                                    //Number of Members
                                    short lNumberOfMembers = lReader.GetInt16(3);

                                    //Populate list with short
                                    oListOfNumOfMembers.Add(lNumberOfMembers);
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
                    (@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\DALErrors\ArtistDALErrors.txt", true))
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
            return oListOfNumOfMembers;
        }

        //Method to view all artists' years active
        public List<short> ViewAllArtistsYearsActive()
        {
            //New list instantiation
            List<short> oListOfYearsActive = new List<short>();
            try
            {
                //Using block for connection
                using (SqlConnection lConnectionToSql = new SqlConnection(_ConnectionString))
                {
                    //Using block for command
                    using (SqlCommand lViewAllCommand = new SqlCommand("VIEW_ALL_ARTISTS", lConnectionToSql))
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
                                    //Assign values from reader to new short
                                    //Years Active
                                    short lYearsActive = lReader.GetInt16(6);

                                    //Populate list with short
                                    oListOfYearsActive.Add(lYearsActive);
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
                    (@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\DALErrors\ArtistDALErrors.txt", true))
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
            return oListOfYearsActive;
        }
    }
}