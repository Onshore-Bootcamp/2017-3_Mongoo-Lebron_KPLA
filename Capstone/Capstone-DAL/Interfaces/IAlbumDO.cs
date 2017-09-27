namespace Capstone_DAL.Interfaces
{
    using System;

    public interface IAlbumDO
    {
        long AlbumID { get; set; }

        string Name { get; set; }

        long ArtistID { get; set; }

        string Genre { get; set; }

        DateTime ReleaseDate { get; set; }

        string PictureURL { get; set; }

        short NumberOfTracks { get; set; }

        TimeSpan Duration { get; set; }

        string AlbumType { get; set; }

        string ReleaseType { get; set; }

        int? Sales { get; set; }

        string GaonAwards { get; set; }

        string AudioLink { get; set; }
    }
}