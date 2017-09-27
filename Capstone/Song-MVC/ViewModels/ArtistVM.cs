namespace Capstone_MVC.ViewModels
{
    using Capstone_DAL.Interfaces;
    using Models;
    using System.Collections.Generic;

    public class ArtistVM
    {
        //Constructor to instantiate new objects in case of a null reference exception
        public ArtistVM()
        {
            Artist = new ArtistPO();

            ListOfArtistDOs = new List<IArtistDO>();

            ListOfArtistPOs = new List<ArtistPO>();
        }


        public ArtistPO Artist { get; set; }

        public List<IArtistDO> ListOfArtistDOs { get; set; }

        public List<ArtistPO> ListOfArtistPOs { get; set; }
    }
}