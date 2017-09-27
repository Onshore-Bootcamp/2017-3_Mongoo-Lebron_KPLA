namespace Capstone_DAL.Models
{
    using System;
    using Interfaces;

    public class AlbumDO : IAlbumDO
    {
        //Constructor to instantiate new objects in case of null reference exception
        public AlbumDO()
        {
            AlbumID = new long();

            ArtistID = new long();

            NumberOfTracks = new short();

            ReleaseDate = new DateTime();

            Sales = new int?();

            Duration = new TimeSpan();
        }
        public long AlbumID { get; set; }

        public string AlbumType { get; set; }

        public long ArtistID { get; set; }

        public string AudioLink { get; set; }

        public string GaonAwards { get; set; }

        public string Genre { get; set; }

        public string Name { get; set; }

        public short NumberOfTracks { get; set; }

        public string PictureURL { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string ReleaseType { get; set; }

        public int? Sales { get; set; }

        public TimeSpan Duration { get; set; }
    }
}
