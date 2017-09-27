namespace Capstone_MVC.Custom.Maps
{
    using Capstone_DAL.Interfaces;
    using Capstone_DAL.Models;
    using Models;
    using System.Collections.Generic;

    public class MapArtist
    {
        //Method to map an artist from a PO to a DO
        public static IArtistDO MapArtistPOtoDO(ArtistPO iArtistPO)
        {
            //Instantiate new DO
            IArtistDO oArtistDO = new ArtistDO();
            //Populate DO
            oArtistDO.ArtistID = iArtistPO.ArtistID;
            oArtistDO.Name = iArtistPO.Name;
            oArtistDO.Genre = iArtistPO.Genre;
            oArtistDO.NumberOfMembers = iArtistPO.NumberOfMembers;
            oArtistDO.PictureURL = iArtistPO.PictureURL;
            oArtistDO.Status = iArtistPO.Status;
            oArtistDO.YearsActive = iArtistPO.YearsActive;
            oArtistDO.Company = iArtistPO.Company;
            oArtistDO.GaonAwards = iArtistPO.GaonAwards;
            oArtistDO.ExternalLink = iArtistPO.ExternalLink;

            return oArtistDO;
        }

        //Method to map an artist from a DO to a PO
        public static ArtistPO MapArtistDOtoPO(IArtistDO iArtistDO)
        {
            //Instantiate a new PO
            ArtistPO oArtistPO = new ArtistPO();
            //Populate PO
            oArtistPO.ArtistID = iArtistDO.ArtistID;
            oArtistPO.Name = iArtistDO.Name;
            oArtistPO.Genre = iArtistDO.Genre;
            oArtistPO.NumberOfMembers = iArtistDO.NumberOfMembers;
            oArtistPO.PictureURL = iArtistDO.PictureURL;
            oArtistPO.Status = iArtistDO.Status;
            oArtistPO.YearsActive = iArtistDO.YearsActive;
            oArtistPO.Company = iArtistDO.Company;
            oArtistPO.GaonAwards = iArtistDO.GaonAwards;
            oArtistPO.ExternalLink = iArtistDO.ExternalLink;

            return oArtistPO;
        }

        //Method to map a list of artist DOs to a list of POs
        public static List<ArtistPO> MapListOfArtistDOsToListOfPOs(List<IArtistDO> iArtistDOs)
        {
            //Instantiate a new list of POs
            List<ArtistPO> oListOfArtistPOs = new List<ArtistPO>();

            //Foreach loop to map data from each object in the list
            foreach (IArtistDO lArtist in iArtistDOs)
            {
                ArtistPO lArtistPO = MapArtistDOtoPO(lArtist);
                //Populate list
                oListOfArtistPOs.Add(lArtistPO);
            }
            return oListOfArtistPOs;
        }
    }
}