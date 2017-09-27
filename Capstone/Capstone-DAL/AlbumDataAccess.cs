namespace Capstone_DAL
{
    using Interfaces;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;

    public class AlbumDataAccess
    {
        //Define the connection
        public string _ConnectionString = @"Server=ADMIN2-PC\SQLEXPRESS; Database=CapstoneDB; 
            Trusted_Connection=True;";

        //Method to add new album
        public void AddNewAlbum(IAlbumDO iAlbum)
        {
            try
            {
                //Using block for connection
                using (SqlConnection lConnectionToSql = new SqlConnection(_ConnectionString))
                {
                    //Using block for command
                    using (SqlCommand lCreateCommand = new SqlCommand("ADD_NEW_ALBUM", lConnectionToSql))
                    {
                        try
                        {
                            //Define command properties
                            lCreateCommand.CommandType = CommandType.StoredProcedure;
                            lCreateCommand.CommandTimeout = 40;

                            //Name
                            lCreateCommand.Parameters.AddWithValue("@Name", iAlbum.Name);
                            //Artist
                            lCreateCommand.Parameters.AddWithValue("@Artist", iAlbum.ArtistID);
                            //Release Date
                            lCreateCommand.Parameters.AddWithValue("@ReleaseDate", iAlbum.ReleaseDate);
                            //Number of Tracks
                            lCreateCommand.Parameters.AddWithValue("@NumberOfTracks", iAlbum.NumberOfTracks);
                            //Duration
                            lCreateCommand.Parameters.AddWithValue("@Duration", iAlbum.Duration);
                            //Album Type
                            lCreateCommand.Parameters.AddWithValue("@AlbumType", iAlbum.AlbumType);
                            //Release Type
                            lCreateCommand.Parameters.AddWithValue("@ReleaseType", iAlbum.ReleaseType);

                            //Genre
                            if (iAlbum.Genre == null || iAlbum.Genre == "")
                            {
                                //If user did not input a value for this property, default to null
                                lCreateCommand.Parameters.AddWithValue("@Genre", DBNull.Value);
                            }
                            else
                            {
                                //if user entered a value, add that value
                                lCreateCommand.Parameters.AddWithValue("@Genre", iAlbum.Genre);
                            }

                            //Picture URL
                            if (iAlbum.PictureURL == null || iAlbum.PictureURL == "")
                            {
                                //If user didn't input a value for this property, default to null
                                lCreateCommand.Parameters.AddWithValue("@PictureURL", DBNull.Value);
                            }
                            else
                            {
                                //if user entered a value, add that value
                                lCreateCommand.Parameters.AddWithValue("@PictureURL", iAlbum.PictureURL);
                            }

                            //Sales
                            if (iAlbum.Sales == null)
                            {
                                //If user didn't input a value for this property, default to null
                                lCreateCommand.Parameters.AddWithValue("@Sales", DBNull.Value);
                            }
                            else
                            {
                                //if user entered a value, add that value
                                lCreateCommand.Parameters.AddWithValue("@Sales", iAlbum.Sales);
                            }

                            //Gaon Awards
                            if (iAlbum.GaonAwards == null || iAlbum.GaonAwards == "")
                            {
                                //If user didn't input a value for this property, default to null
                                lCreateCommand.Parameters.AddWithValue("@GaonAwards", DBNull.Value);
                            }
                            else
                            {
                                //if user entered a value, add that value
                                lCreateCommand.Parameters.AddWithValue("@GaonAwards", iAlbum.GaonAwards);
                            }

                            //AudioLink
                            if (iAlbum.AudioLink == null || iAlbum.AudioLink == "")
                            {
                                //If user didn't input a value for this property, default to null
                                lCreateCommand.Parameters.AddWithValue("@AudioLink", DBNull.Value);
                            }
                            else
                            {
                                //if user entered a value, add that value
                                lCreateCommand.Parameters.AddWithValue("@AudioLink", iAlbum.AudioLink);
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
                    (@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\DALErrors\AlbumDALErrors.txt", true))
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

        //Method to view all albums
        public List<IAlbumDO> ViewAllAlbums()
        {
            //Instantiate new list
            List<IAlbumDO> oListOfAlbumDOs = new List<IAlbumDO>();

            try
            {
                //Using block for connection
                using (SqlConnection lConnectionToSql = new SqlConnection(_ConnectionString))
                {
                    //Using block for command
                    using (SqlCommand lViewAllCommand = new SqlCommand("VIEW_ALL_ALBUMS", lConnectionToSql))
                    {
                        try
                        {
                            //Define command properties
                            lViewAllCommand.CommandType = CommandType.StoredProcedure;
                            lViewAllCommand.CommandTimeout = 40;

                            //Open connection to SQL
                            lConnectionToSql.Open();
                            //Using block for reader
                            using (SqlDataReader lReader = lViewAllCommand.ExecuteReader())
                            {
                                //Populate datatable from reader
                                while (lReader.Read())
                                {
                                    //New instantiation of DO
                                    IAlbumDO lAlbumDO = new AlbumDO();

                                    //Assign values from reader to new DO
                                    //Album ID
                                    lAlbumDO.AlbumID = lReader.GetInt64(0);
                                    //Name
                                    lAlbumDO.Name = lReader.GetString(1);
                                    //Artist
                                    lAlbumDO.ArtistID = lReader.GetInt64(2);
                                    //Release Date
                                    lAlbumDO.ReleaseDate = lReader.GetDateTime(4);
                                    //Number of Tracks
                                    lAlbumDO.NumberOfTracks = lReader.GetInt16(6);
                                    //Duration
                                    lAlbumDO.Duration = lReader.GetTimeSpan(7);
                                    //Album Type
                                    lAlbumDO.AlbumType = lReader.GetString(8);
                                    //Release Type
                                    lAlbumDO.ReleaseType = lReader.GetString(9);

                                    //Genre
                                    if (lReader.IsDBNull(3))
                                    {
                                        lAlbumDO.Genre = DBNull.Value.ToString();
                                    }
                                    else
                                    {
                                        lAlbumDO.Genre = lReader.GetString(3);
                                    }

                                    //Picture URL
                                    if (lReader.IsDBNull(5))
                                    {
                                        lAlbumDO.PictureURL = DBNull.Value.ToString();
                                    }
                                    else
                                    {
                                        lAlbumDO.PictureURL = lReader.GetString(5);
                                    }

                                    //Sales
                                    if (lReader.IsDBNull(10))
                                    {
                                        lAlbumDO.Sales = null;
                                    }
                                    else
                                    {
                                        lAlbumDO.Sales = lReader.GetInt32(10);
                                    }

                                    //Gaon Awards
                                    if (lReader.IsDBNull(11))
                                    {
                                        lAlbumDO.GaonAwards = DBNull.Value.ToString();
                                    }
                                    else
                                    {
                                        lAlbumDO.GaonAwards = lReader.GetString(11);
                                    }

                                    //AudioLink
                                    if (lReader.IsDBNull(12))
                                    {
                                        lAlbumDO.AudioLink = DBNull.Value.ToString();
                                    }
                                    else
                                    {
                                        lAlbumDO.AudioLink = lReader.GetString(12);
                                    }

                                    //Populate list with DO
                                    oListOfAlbumDOs.Add(lAlbumDO);
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
                    (@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\DALErrors\AlbumDALErrors.txt", true))
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
            return oListOfAlbumDOs;
        }

        //Method to view album by ID
        public IAlbumDO ViewAlbumByID(long iAlbumID)
        {
            //Instantiate new DO
            IAlbumDO oAlbumDO = new AlbumDO();

            try
            {
                //Using block for connection
                using (SqlConnection lConnectionToSql = new SqlConnection(_ConnectionString))
                {
                    //Using block for command
                    using (SqlCommand lViewCommand = new SqlCommand("VIEW_ALBUM_BY_ID", lConnectionToSql))
                    {
                        try
                        {
                            //Declare command parameters
                            lViewCommand.CommandType = CommandType.StoredProcedure;
                            lViewCommand.CommandTimeout = 40;
                            lViewCommand.Parameters.AddWithValue("@AlbumID", iAlbumID);

                            //Open connection
                            lConnectionToSql.Open();
                            //Using block for reader
                            using (SqlDataReader lReader = lViewCommand.ExecuteReader())
                            {
                                //Populate datatable from reader
                                while (lReader.Read())
                                {
                                    //Album ID
                                    oAlbumDO.AlbumID = lReader.GetInt64(0);
                                    //Name
                                    oAlbumDO.Name = lReader.GetString(1);
                                    //Artist
                                    oAlbumDO.ArtistID = lReader.GetInt64(2);
                                    //Release Date
                                    oAlbumDO.ReleaseDate = lReader.GetDateTime(4);
                                    //Number of Tracks
                                    oAlbumDO.NumberOfTracks = lReader.GetInt16(6);
                                    //Duration
                                    oAlbumDO.Duration = lReader.GetTimeSpan(7);
                                    //Album Type
                                    oAlbumDO.AlbumType = lReader.GetString(8);
                                    //Release Type
                                    oAlbumDO.ReleaseType = lReader.GetString(9);

                                    //Genre
                                    if (lReader.IsDBNull(3))
                                    {
                                        oAlbumDO.Genre = DBNull.Value.ToString();
                                    }
                                    else
                                    {
                                        oAlbumDO.Genre = lReader.GetString(3);
                                    }

                                    //Picture URL
                                    if (lReader.IsDBNull(5))
                                    {
                                        oAlbumDO.PictureURL = DBNull.Value.ToString();
                                    }
                                    else
                                    {
                                        oAlbumDO.PictureURL = lReader.GetString(5);
                                    }

                                    //Sales
                                    if (lReader.IsDBNull(10))
                                    {
                                        oAlbumDO.Sales = null;
                                    }
                                    else
                                    {
                                        oAlbumDO.Sales = lReader.GetInt32(10);
                                    }

                                    //Gaon Awards
                                    if (lReader.IsDBNull(11))
                                    {
                                        oAlbumDO.GaonAwards = DBNull.Value.ToString();
                                    }
                                    else
                                    {
                                        oAlbumDO.GaonAwards = lReader.GetString(11);
                                    }

                                    //AudioLink
                                    if (lReader.IsDBNull(12))
                                    {
                                        oAlbumDO.AudioLink = DBNull.Value.ToString();
                                    }
                                    else
                                    {
                                        oAlbumDO.AudioLink = lReader.GetString(12);
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
                    (@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\DALErrors\AlbumDALErrors.txt", true))
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
            return oAlbumDO;
        }

        //Method to view album name by ID
        public string ViewAlbumNameByID(long iAlbumID)
        {
            //Declare var to return
            string oAlbumName = null;

            try
            {
                //Using block for connection
                using (SqlConnection lConnectionToSql = new SqlConnection(_ConnectionString))
                {
                    //Using block for command
                    using (SqlCommand lViewCommand = new SqlCommand("VIEW_ALBUM_BY_ID", lConnectionToSql))
                    {
                        try
                        {
                            //Declare command parameters
                            lViewCommand.CommandType = CommandType.StoredProcedure;
                            lViewCommand.CommandTimeout = 40;
                            lViewCommand.Parameters.AddWithValue("@AlbumID", iAlbumID);

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
                                    oAlbumName = lReader.GetString(1);
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
                    (@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\DALErrors\AlbumDALErrors.txt", true))
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
            return oAlbumName;
        }

        //Method to update album by ID
        public void UpdateAlbumByID(IAlbumDO iAlbumDO)
        {
            try
            {
                //Using block for connection
                using (SqlConnection lConnectionToSql = new SqlConnection(_ConnectionString))
                {
                    //Using block for command
                    using (SqlCommand lUpdateCommand = new SqlCommand("UPDATE_ALBUM_BY_ID", lConnectionToSql))
                    {
                        try
                        {
                            //Declare command parameters
                            lUpdateCommand.CommandType = CommandType.StoredProcedure;
                            lUpdateCommand.CommandTimeout = 40;
                            lUpdateCommand.Parameters.AddWithValue("@AlbumID", iAlbumDO.AlbumID);
                            lUpdateCommand.Parameters.AddWithValue("@Name", iAlbumDO.Name);
                            lUpdateCommand.Parameters.AddWithValue("@Artist", iAlbumDO.ArtistID);
                            lUpdateCommand.Parameters.AddWithValue("@Genre", iAlbumDO.Genre);
                            lUpdateCommand.Parameters.AddWithValue("@ReleaseDate", iAlbumDO.ReleaseDate);
                            lUpdateCommand.Parameters.AddWithValue("@PictureURL", iAlbumDO.PictureURL);
                            lUpdateCommand.Parameters.AddWithValue("@NumberOfTracks", iAlbumDO.NumberOfTracks);
                            lUpdateCommand.Parameters.AddWithValue("@Duration", iAlbumDO.Duration);
                            lUpdateCommand.Parameters.AddWithValue("@AlbumType", iAlbumDO.AlbumType);
                            lUpdateCommand.Parameters.AddWithValue("@ReleaseType", iAlbumDO.ReleaseType);
                            lUpdateCommand.Parameters.AddWithValue("@Sales", iAlbumDO.Sales);
                            lUpdateCommand.Parameters.AddWithValue("@GaonAwards", iAlbumDO.GaonAwards);
                            lUpdateCommand.Parameters.AddWithValue("@AudioLink", iAlbumDO.AudioLink);

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
                    (@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\DALErrors\AlbumDALErrors.txt", true))
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

        //Method to delete album by ID
        public void DeleteAlbumByID(long iAlbumID)
        {
            try
            {
                //Using block for connection
                using (SqlConnection lConnectionToSql = new SqlConnection(_ConnectionString))
                {
                    //Using block for command
                    using (SqlCommand lDeleteCommand = new SqlCommand("DELETE_ALBUM_BY_ID", lConnectionToSql))
                    {
                        try
                        {
                            //Declare command parameters
                            lDeleteCommand.CommandType = CommandType.StoredProcedure;
                            lDeleteCommand.CommandTimeout = 40;
                            lDeleteCommand.Parameters.AddWithValue("@AlbumID", iAlbumID);

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
                    (@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\DALErrors\AlbumDALErrors.txt", true))
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

        //Method to view all albums' number of tracks
        public List<short> ViewAllAlbumsNumberOfTracks()
        {
            //New list instantiation
            List<short> oListOfNumOfTracks = new List<short>();
            try
            {
                //Using block for connection
                using (SqlConnection lConnectionToSql = new SqlConnection(_ConnectionString))
                {
                    //Using block for command
                    using (SqlCommand lViewAllCommand = new SqlCommand("VIEW_ALL_ALBUMS", lConnectionToSql))
                    {
                        try
                        {
                            //Define command properties
                            lViewAllCommand.CommandType = CommandType.StoredProcedure;
                            lViewAllCommand.CommandTimeout = 40;

                            //Open connection to SQL
                            lConnectionToSql.Open();

                            //Using block for reader
                            using (SqlDataReader lReader = lViewAllCommand.ExecuteReader())
                            {
                                //Populate datatable from reader
                                while (lReader.Read())
                                {
                                    //Assign value from reader to new short
                                    //Number of Tracks
                                    short lNumberOfTracks = lReader.GetInt16(6);

                                    //Populate list with short
                                    oListOfNumOfTracks.Add(lNumberOfTracks);
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
                    (@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\DALErrors\AlbumDALErrors.txt", true))
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
            return oListOfNumOfTracks;
        }
    }
}