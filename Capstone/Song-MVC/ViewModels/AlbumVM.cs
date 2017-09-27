namespace Capstone_MVC.ViewModels
{
    using Capstone_DAL.Interfaces;
    using Models;
    using System.Collections.Generic;

    public class AlbumVM
    {
        //Constructor to instantiate new objects in case of Null Reference Exception
        public AlbumVM()
        {
            Album = new AlbumPO();

            ListOfAlbumDOs = new List<IAlbumDO>();

            ListOfAlbumPOs = new List<AlbumPO>();
        }

        public AlbumPO Album { get; set; }

        public List<IAlbumDO> ListOfAlbumDOs { get; set; }

        public List<AlbumPO> ListOfAlbumPOs { get; set; }
    }
}