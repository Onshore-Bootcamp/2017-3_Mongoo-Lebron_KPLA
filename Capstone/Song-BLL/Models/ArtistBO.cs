namespace Capstone_BLL.Models
{
    using Interfaces;

    public class ArtistBO : IArtistBO
    {
        //Constructor to instantiate new objects in case of null reference exception
        public ArtistBO()
        {
            ArtistID = new long();

            NumberOfMembers = new short();

            YearsActive = new short();
        }

        public long ArtistID { get; set; }

        public string Company { get; set; }

        public string ExternalLink { get; set; }

        public string GaonAwards { get; set; }

        public string Genre { get; set; }

        public string Name { get; set; }

        public short NumberOfMembers { get; set; }

        public string PictureURL { get; set; }

        public string Status { get; set; }

        public short YearsActive { get; set; }
    }
}
