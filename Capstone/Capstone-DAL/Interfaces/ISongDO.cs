namespace Capstone_DAL.Interfaces
{
    using System;

    public interface ISongDO
    {
        long SongID { get; set; }

        string Name { get; set; }

        long AlbumID { get; set; }

        byte? TrackNumber { get; set; }

        long ArtistID { get; set; }

        string Genre { get; set; }

        TimeSpan Duration { get; set; }

        string GaonAwards { get; set; }

        string LyricsLink { get; set; }

        string AudioLink { get; set; }
    }
}
