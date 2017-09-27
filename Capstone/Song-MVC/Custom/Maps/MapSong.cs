namespace Capstone_MVC.Custom.Maps
{
    using Capstone_DAL.Interfaces;
    using Capstone_DAL.Models;
    using Models;
    using System;
    using System.Collections.Generic;

    public class MapSong
    {
        //Method to map a song from a PO to a DO
        public static ISongDO MapSongFromPOtoDO(SongPO iSongPO)
        {
            //Instantiate new DO
            ISongDO oSongDO = new SongDO();
            //Populate DO
            oSongDO.SongID = iSongPO.SongID;
            oSongDO.Name = iSongPO.Name;
            oSongDO.AlbumID = iSongPO.AlbumID;
            oSongDO.TrackNumber = iSongPO.TrackNumber;
            oSongDO.ArtistID = iSongPO.ArtistID;
            oSongDO.Genre = iSongPO.Genre;
            oSongDO.Duration = TimeSpan.Parse(iSongPO.Duration);
            oSongDO.GaonAwards = iSongPO.GaonAwards;
            oSongDO.LyricsLink = iSongPO.LyricsLink;
            oSongDO.AudioLink = iSongPO.AudioLink;

            return oSongDO;
        }

        //Method to map a song from a DO to a PO
        public static SongPO MapSongFromDOtoPO(ISongDO iSongDO)
        {
            //Instantiate new PO
            SongPO oSongPO = new SongPO();
            //Populate PO
            oSongPO.SongID = iSongDO.SongID;
            oSongPO.Name = iSongDO.Name;
            oSongPO.AlbumID = iSongDO.AlbumID;
            oSongPO.TrackNumber = iSongDO.TrackNumber;
            oSongPO.ArtistID = iSongDO.ArtistID;
            oSongPO.Genre = iSongDO.Genre;
            oSongPO.Duration = iSongDO.Duration.ToString();
            oSongPO.GaonAwards = iSongDO.GaonAwards;
            oSongPO.LyricsLink = iSongDO.LyricsLink;
            oSongPO.AudioLink = iSongDO.AudioLink;

            return oSongPO;
        }

        //Method to map a list of songs DOs to a list of POs
        public static List<SongPO> MapListOfSongDOsToListOfPOs(List<ISongDO> iSongDOs)
        {
            //Instantiate a new list of POs
            List<SongPO> oListOfSongPOs = new List<SongPO>();

            //Foreach loop to map data from each object in the list
            foreach (ISongDO lSong in iSongDOs)
            {
                SongPO lSongPO = MapSongFromDOtoPO(lSong);
                //Populate list
                oListOfSongPOs.Add(lSongPO);
            }
            return oListOfSongPOs;
        }
    }
}