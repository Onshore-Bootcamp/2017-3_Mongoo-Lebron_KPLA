namespace Capstone_DAL
{
    using Interfaces;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;

    public class SongDataAccess
    {
        //Define connection
        public string _ConnectionString = @"Server=ADMIN2-PC\SQLEXPRESS; Database=CapstoneDB; 
            Trusted_Connection=True;";

        //Method to add new song
        public void AddNewSong(ISongDO iSong)
        {
            try
            {
                //Using block for connection
                using (SqlConnection lConnectionToSql = new SqlConnection(_ConnectionString))
                {
                    //Using block for command
                    using (SqlCommand lCreateCommand = new SqlCommand("ADD_NEW_SONG", lConnectionToSql))
                    {
                        try
                        {
                            //Define command properties
                            lCreateCommand.CommandType = CommandType.StoredProcedure;
                            lCreateCommand.CommandTimeout = 40;

                            //Name
                            lCreateCommand.Parameters.AddWithValue("@Name", iSong.Name);
                            //Album
                            lCreateCommand.Parameters.AddWithValue("@Album", iSong.AlbumID);
                            //Artist
                            lCreateCommand.Parameters.AddWithValue("@Artist", iSong.ArtistID);
                            //Duration
                            lCreateCommand.Parameters.AddWithValue("@Duration", iSong.Duration);

                            //Track Number
                            if (iSong.TrackNumber == null)
                            {
                                //If user did not input a value for this property, default to null
                                lCreateCommand.Parameters.AddWithValue("@TrackNumber", DBNull.Value);
                            }
                            else
                            {
                                //if user entered a value, add that value
                                lCreateCommand.Parameters.AddWithValue("@TrackNumber", iSong.TrackNumber);
                            }

                            //Genre
                            if (iSong.Genre == null || iSong.Genre == "")
                            {
                                //If user didn't input a value for this property, default to null
                                lCreateCommand.Parameters.AddWithValue("@Genre", DBNull.Value);
                            }
                            else
                            {
                                //if user entered a value, add that value
                                lCreateCommand.Parameters.AddWithValue("@Genre", iSong.Genre);
                            }

                            //Gaon Awards
                            if (iSong.GaonAwards == null || iSong.GaonAwards == "")
                            {
                                //If user didn't input a value for this property, default to null
                                lCreateCommand.Parameters.AddWithValue("@GaonAwards", DBNull.Value);
                            }
                            else
                            {
                                //if user entered a value, add that value
                                lCreateCommand.Parameters.AddWithValue("@GaonAwards", iSong.GaonAwards);
                            }

                            //Lyrics Link
                            if (iSong.LyricsLink == null || iSong.LyricsLink == "")
                            {
                                //If user didn't input a value for this property, default to null
                                lCreateCommand.Parameters.AddWithValue("@LyricsLink", DBNull.Value);
                            }
                            else
                            {
                                //if user entered a value, add that value
                                lCreateCommand.Parameters.AddWithValue("@LyricsLink", iSong.LyricsLink);
                            }

                            //AudioLink
                            if (iSong.AudioLink == null || iSong.AudioLink == "")
                            {
                                //If user didn't input a value for this property, default to null
                                lCreateCommand.Parameters.AddWithValue("@AudioLink", DBNull.Value);
                            }
                            else
                            {
                                //if user entered a value, add that value
                                lCreateCommand.Parameters.AddWithValue("@AudioLink", iSong.AudioLink);
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
                using (StreamWriter fileWriter = new StreamWriter(@"C:\Users\Mongoo\Documents\DALErrors\Visual Studio 2015\Projects\Capstone\Logs\SongDALErrors.txt", true))
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

        //Method to view all songs
        public List<ISongDO> ViewAllSongs()
        {
            //Instantiate new list
            List<ISongDO> oListOfSongDOs = new List<ISongDO>();

            try
            {
                //Using block for connection
                using (SqlConnection lConnectionToSql = new SqlConnection(_ConnectionString))
                {
                    //Using block for command
                    using (SqlCommand lViewAllCommand = new SqlCommand("VIEW_ALL_SONGS", lConnectionToSql))
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
                                    ISongDO lSongDO = new SongDO();

                                    //Assign values from reader to new DO
                                    //Song ID
                                    lSongDO.SongID = lReader.GetInt64(0);
                                    //Name
                                    lSongDO.Name = lReader.GetString(1);
                                    //Album
                                    lSongDO.AlbumID = lReader.GetInt64(2);
                                    //Artist
                                    lSongDO.ArtistID = lReader.GetInt64(4);
                                    //Duration
                                    lSongDO.Duration = lReader.GetTimeSpan(6);

                                    //Track Number
                                    if (lReader.IsDBNull(3))
                                    {
                                        lSongDO.TrackNumber = null;
                                    }
                                    else
                                    {
                                        lSongDO.TrackNumber = lReader.GetByte(3);
                                    }

                                    //Genre
                                    if (lReader.IsDBNull(5))
                                    {
                                        lSongDO.Genre = DBNull.Value.ToString();
                                    }
                                    else
                                    {
                                        lSongDO.Genre = lReader.GetString(5);
                                    }

                                    //Gaon Awards
                                    if (lReader.IsDBNull(7))
                                    {
                                        lSongDO.GaonAwards = DBNull.Value.ToString();
                                    }
                                    else
                                    {
                                        lSongDO.GaonAwards = lReader.GetString(7);
                                    }

                                    //Lyrics Link
                                    if (lReader.IsDBNull(8))
                                    {
                                        lSongDO.LyricsLink = DBNull.Value.ToString();
                                    }
                                    else
                                    {
                                        lSongDO.LyricsLink = lReader.GetString(8);
                                    }

                                    //AudioLink
                                    if (lReader.IsDBNull(9))
                                    {
                                        lSongDO.AudioLink = DBNull.Value.ToString();
                                    }
                                    else
                                    {
                                        lSongDO.AudioLink = lReader.GetString(9);
                                    }

                                    //Populate list with DO
                                    oListOfSongDOs.Add(lSongDO);
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
                    (@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\DALErrors\SongDALErrors.txt", true))
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
            return oListOfSongDOs;
        }

        //Method to view song by ID
        public ISongDO ViewSongByID(long iSongID)
        {
            //Instantiate new DO
            ISongDO oSongDO = new SongDO();

            try
            {
                //Using block for connection
                using (SqlConnection lConnectionToSql = new SqlConnection(_ConnectionString))
                {
                    //Using block for command
                    using (SqlCommand lViewCommand = new SqlCommand("VIEW_SONG_BY_ID", lConnectionToSql))
                    {
                        try
                        {
                            //Declare command parameters
                            lViewCommand.CommandType = CommandType.StoredProcedure;
                            lViewCommand.CommandTimeout = 40;
                            lViewCommand.Parameters.AddWithValue("@SongID", iSongID);

                            //Open connection
                            lConnectionToSql.Open();
                            //Using block for reader
                            using (SqlDataReader lReader = lViewCommand.ExecuteReader())
                            {
                                //Populate datatable from reader
                                while (lReader.Read())
                                {
                                    //Song ID
                                    oSongDO.SongID = lReader.GetInt64(0);
                                    //Name
                                    oSongDO.Name = lReader.GetString(1);
                                    //Album
                                    oSongDO.AlbumID = lReader.GetInt64(2);
                                    //Artist
                                    oSongDO.ArtistID = lReader.GetInt64(4);
                                    //Duration
                                    oSongDO.Duration = lReader.GetTimeSpan(6);

                                    //Track Number
                                    if (lReader.IsDBNull(3))
                                    {
                                        oSongDO.TrackNumber = null;
                                    }
                                    else
                                    {
                                        oSongDO.TrackNumber = lReader.GetByte(3);
                                    }

                                    //Genre
                                    if (lReader.IsDBNull(5))
                                    {
                                        oSongDO.Genre = DBNull.Value.ToString();
                                    }
                                    else
                                    {
                                        oSongDO.Genre = lReader.GetString(5);
                                    }

                                    //Gaon Awards
                                    if (lReader.IsDBNull(7))
                                    {
                                        oSongDO.GaonAwards = DBNull.Value.ToString();
                                    }
                                    else
                                    {
                                        oSongDO.GaonAwards = lReader.GetString(7);
                                    }

                                    //Lyrics Link
                                    if (lReader.IsDBNull(8))
                                    {
                                        oSongDO.LyricsLink = DBNull.Value.ToString();
                                    }
                                    else
                                    {
                                        oSongDO.LyricsLink = lReader.GetString(8);
                                    }

                                    //AudioLink
                                    if (lReader.IsDBNull(9))
                                    {
                                        oSongDO.AudioLink = DBNull.Value.ToString();
                                    }
                                    else
                                    {
                                        oSongDO.AudioLink = lReader.GetString(9);
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
                    (@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\DALErrors\SongDALErrors.txt", true))
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
            return oSongDO;
        }

        //Method to update song by ID
        public void UpdateSongByID(ISongDO iSongDO)
        {
            try
            {
                //Using block for connection
                using (SqlConnection lConnectionToSql = new SqlConnection(_ConnectionString))
                {
                    //Using block for command
                    using (SqlCommand lUpdateCommand = new SqlCommand("UPDATE_SONG_BY_ID", lConnectionToSql))
                    {
                        try
                        {
                            //Declare command parameters
                            lUpdateCommand.CommandType = CommandType.StoredProcedure;
                            lUpdateCommand.CommandTimeout = 40;
                            lUpdateCommand.Parameters.AddWithValue("@SongID", iSongDO.SongID);
                            lUpdateCommand.Parameters.AddWithValue("@Name", iSongDO.Name);
                            lUpdateCommand.Parameters.AddWithValue("@Album", iSongDO.AlbumID);
                            lUpdateCommand.Parameters.AddWithValue("@TrackNumber", iSongDO.TrackNumber);
                            lUpdateCommand.Parameters.AddWithValue("@Artist", iSongDO.ArtistID);
                            lUpdateCommand.Parameters.AddWithValue("@Genre", iSongDO.Genre);
                            lUpdateCommand.Parameters.AddWithValue("@Duration", iSongDO.Duration);
                            lUpdateCommand.Parameters.AddWithValue("@GaonAwards", iSongDO.GaonAwards);
                            lUpdateCommand.Parameters.AddWithValue("@LyricsLink", iSongDO.LyricsLink);
                            lUpdateCommand.Parameters.AddWithValue("@AudioLink", iSongDO.AudioLink);

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
                    (@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\DALErrors\SongDALErrors.txt", true))
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

        //Method to delete song by ID
        public void DeleteSongByID(long iSongID)
        {
            try
            {
                //Using block for connection
                using (SqlConnection lConnectionToSql = new SqlConnection(_ConnectionString))
                {
                    //Using block for command
                    using (SqlCommand lDeleteCommand = new SqlCommand("DELETE_SONG_BY_ID", lConnectionToSql))
                    {
                        try
                        {
                            //Declare command parameters
                            lDeleteCommand.CommandType = CommandType.StoredProcedure;
                            lDeleteCommand.CommandTimeout = 40;
                            lDeleteCommand.Parameters.AddWithValue("@SongID", iSongID);

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
                    (@"C:\Users\Mongoo\Documents\Visual Studio 2015\Projects\Capstone\Logs\DALErrors\SongDALErrors.txt", true))
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