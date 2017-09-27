namespace Capstone_MVC.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class SongPO
    {
        //Constructor to instantiate new objects in case of Null Reference Exception
        public SongPO()
        {
            AlbumID = new long();

            ArtistID = new long();

            SongID = new long();

            TrackNumber = new byte?();
        }

        [Required]
        [Display(Name = "Album")]
        public long AlbumID { get; set; }

        [Required]
        [Display(Name = "Artist")]
        public long ArtistID { get; set; }

        [Display(Name = "Audio Link")]
        public string AudioLink { get; set; }

        [Required(ErrorMessage = "Please enter song duration.")]
        public string Duration { get; set; }

        [Display(Name = "Gaon Award Wins")]
        public string GaonAwards { get; set; }
                
        public string Genre { get; set; }

        [Display(Name = "Lyrics Link")]
        public string LyricsLink { get; set; }

        [Required(ErrorMessage = "Please enter song name.")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "ID: ")]
        public long SongID { get; set; }

        [Display(Name = "Track Number")]
        public byte? TrackNumber { get; set; }

        public SelectList DropdownArtists { get; set; }

        public SelectList DropdownAlbums { get; set; }

        public string AlbumName { get; set; }

        public string ArtistName { get; set; }
    }
}