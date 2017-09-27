namespace Capstone_MVC.Custom.Maps
{
    using Capstone_DAL.Interfaces;
    using Capstone_DAL.Models;
    using Models;
    using System.Collections.Generic;

    public class MapUser
    {
        //Method to map a user from a PO to a DO
        public static IUserDO MapUserPOtoDO(UserPO iUserPO)
        {
            //Instantiate new DO
            IUserDO oUserDO = new UserDO();
            //Populate DO
            oUserDO.UserID = iUserPO.UserID;
            oUserDO.Username = iUserPO.Username;
            oUserDO.Role = iUserPO.Role;
            oUserDO.FirstName = iUserPO.FirstName;
            oUserDO.LastName = iUserPO.LastName;
            oUserDO.EmailAddress = iUserPO.EmailAddress;
            oUserDO.Password = iUserPO.Password;
            oUserDO.Language = iUserPO.Language;
            oUserDO.Birthdate = iUserPO.Birthdate;
            oUserDO.FavoriteSongs = iUserPO.FavoriteSongs;
            oUserDO.ExternalLink = iUserPO.ExternalLink;
            oUserDO.AboutMeContent = iUserPO.AboutMeContent;
            oUserDO.Suspended = iUserPO.Suspended;

            return oUserDO;
        }

        //Method to map a user from a DO to a PO
        public static UserPO MapUserDOtoPO(IUserDO iUserDO)
        {
            //Instantiate new PO
            UserPO oUserPO = new UserPO();
            //Populate DO
            oUserPO.UserID = iUserDO.UserID;
            oUserPO.Username = iUserDO.Username;
            oUserPO.Role = iUserDO.Role;
            oUserPO.FirstName = iUserDO.FirstName;
            oUserPO.LastName = iUserDO.LastName;
            oUserPO.EmailAddress = iUserDO.EmailAddress;
            oUserPO.Password = iUserDO.Password;
            oUserPO.Language = iUserDO.Language;
            oUserPO.Birthdate = iUserDO.Birthdate;
            oUserPO.FavoriteSongs = iUserDO.FavoriteSongs;
            oUserPO.ExternalLink = iUserDO.ExternalLink;
            oUserPO.AboutMeContent = iUserDO.AboutMeContent;
            oUserPO.Suspended = iUserDO.Suspended;

            return oUserPO;
        }

        //Method to map a list of user DOs to a list of user POs
        public static List<UserPO> MapListOfUserDOsToListOfPOs(List<IUserDO> iUserDOs)
        {
            //Instantiate new list of POs
            List<UserPO> oListOfUserPOs = new List<UserPO>();

            //Foreach loop to map data from each object in the list
            foreach (IUserDO lUser in iUserDOs)
            {
                UserPO lUserPO = MapUserDOtoPO(lUser);
                //Populate list
                oListOfUserPOs.Add(lUserPO);
            }
            return oListOfUserPOs;
        }
    }
}