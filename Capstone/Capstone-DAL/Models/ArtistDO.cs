﻿namespace Capstone_DAL.Models
{
    using Interfaces;

    public class ArtistDO : IArtistDO
    {
        //Constructor to instantiate new objects in case of null reference exception
        public ArtistDO()
        {
            ArtistID = new long();

            NumberOfMembers = new short();

            YearsActive = new short();
        }

        public string ExternalLink { get; set; }

        public string GaonAwards { get; set; }

        public string Genre { get; set; }

        public long ArtistID { get; set; }

        public string Name { get; set; }

        public string PictureURL { get; set; }

        public string Company { get; set; }

        public short NumberOfMembers { get; set; }

        public string Status { get; set; }

        public short YearsActive { get; set; }
    }
}
