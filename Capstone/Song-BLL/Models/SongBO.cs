namespace Capstone_BLL.Models
{
    using System;
    using Interfaces;

    public class SongBO : ISongBO
    {
        //Constructor to instantiate new objects in case of null reference exception
        public SongBO()
        {
            AlbumID = new long();

            ArtistID = new long();

            Duration = new TimeSpan();

            SongID = new long();

            TrackNumber = new byte?();
        }

        public long AlbumID { get; set; }

        public long ArtistID { get; set; }

        public string AudioLink { get; set; }

        public TimeSpan Duration { get; set; }

        public string GaonAwards { get; set; }

        public string Genre { get; set; }

        public string LyricsLink { get; set; }

        public string Name { get; set; }

        public long SongID { get; set; }

        public byte? TrackNumber { get; set; }
    }
}
