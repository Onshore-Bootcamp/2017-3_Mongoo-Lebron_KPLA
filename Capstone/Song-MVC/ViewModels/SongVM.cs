namespace Capstone_MVC.ViewModels
{
    using Capstone_DAL.Interfaces;
    using Models;
    using System.Collections.Generic;

    public class SongVM
    {
        //Constructor to instantiate new objects in case of a null reference exception
        public SongVM()
        {
            Song = new SongPO();

            ListOfSongDOs = new List<ISongDO>();

            ListOfSongPOs = new List<SongPO>();
        }

        public SongPO Song { get; set; }

        public List<ISongDO> ListOfSongDOs { get; set; }

        public List<SongPO> ListOfSongPOs { get; set; }
    }
}