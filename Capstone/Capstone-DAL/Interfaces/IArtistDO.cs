namespace Capstone_DAL.Interfaces
{
    public interface IArtistDO
    {
        long ArtistID { get; set; }

        string Name { get; set; }

        string Genre { get; set; }

        short NumberOfMembers { get; set; }

        string PictureURL { get; set; }

        string Status { get; set; }

        short YearsActive { get; set; }

        string Company { get; set; }

        string GaonAwards { get; set; }

        string ExternalLink { get; set; }
    }
}
