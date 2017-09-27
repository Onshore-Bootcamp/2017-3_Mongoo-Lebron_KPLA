namespace Capstone_MVC.Custom.Maps
{
    using Models;
    using System.Collections.Generic;
    using Capstone_DAL.Models;
    using Capstone_DAL.Interfaces;

    public class MapAlbum
    {
        //Method to map an album from a PO to a DO
        public static IAlbumDO MapAlbumFromPOtoDO(AlbumPO iAlbumPO)
        {
            //Instantiate new DO
            IAlbumDO oAlbumDO = new AlbumDO();
            //Populate DO
            oAlbumDO.AlbumID = iAlbumPO.AlbumID;
            oAlbumDO.Name = iAlbumPO.Name;
            oAlbumDO.ArtistID = iAlbumPO.ArtistID;
            oAlbumDO.Genre = iAlbumPO.Genre;
            oAlbumDO.ReleaseDate = iAlbumPO.ReleaseDate;
            oAlbumDO.PictureURL = iAlbumPO.PictureURL;
            oAlbumDO.NumberOfTracks = iAlbumPO.NumberOfTracks;
            oAlbumDO.Duration = iAlbumPO.Duration;
            oAlbumDO.AlbumType = iAlbumPO.AlbumType;
            oAlbumDO.ReleaseType = iAlbumPO.ReleaseType;
            oAlbumDO.Sales = iAlbumPO.Sales;
            oAlbumDO.GaonAwards = iAlbumPO.GaonAwards;
            oAlbumDO.AudioLink = iAlbumPO.AudioLink;

            return oAlbumDO;
        }

        //Method to map an album from a DO to a PO
        public static AlbumPO MapAlbumFromDOtoPO(IAlbumDO iAlbumDO)
        {
            //Instantiate a new PO
            AlbumPO oAlbumPO = new AlbumPO();
            //Populate PO
            oAlbumPO.AlbumID = iAlbumDO.AlbumID;
            oAlbumPO.Name = iAlbumDO.Name;
            oAlbumPO.ArtistID = iAlbumDO.ArtistID;
            oAlbumPO.Genre = iAlbumDO.Genre;
            oAlbumPO.ReleaseDate = iAlbumDO.ReleaseDate;
            oAlbumPO.PictureURL = iAlbumDO.PictureURL;
            oAlbumPO.NumberOfTracks = iAlbumDO.NumberOfTracks;
            oAlbumPO.Duration = iAlbumDO.Duration;
            oAlbumPO.AlbumType = iAlbumDO.AlbumType;
            oAlbumPO.ReleaseType = iAlbumDO.ReleaseType;
            oAlbumPO.Sales = iAlbumDO.Sales;
            oAlbumPO.GaonAwards = iAlbumDO.GaonAwards;
            oAlbumPO.AudioLink = iAlbumDO.AudioLink;

            return oAlbumPO;
        }

        //Method to map a list of album DOs to a list of POs
        public static List<AlbumPO> MapListOfAlbumDOsToListOfPOs(List<IAlbumDO> iAlbumDOs)
        {
            //Instantiate a new list of POs
            List<AlbumPO> oListOfAlbumPOs = new List<AlbumPO>();

            //Foreach loop to map data from each object in the list
            foreach (IAlbumDO lAlbum in iAlbumDOs)
            {
                AlbumPO lAlbumPO = MapAlbumFromDOtoPO(lAlbum);
                //Populate list
                oListOfAlbumPOs.Add(lAlbumPO);
            }
            return oListOfAlbumPOs;
        }
    }
}