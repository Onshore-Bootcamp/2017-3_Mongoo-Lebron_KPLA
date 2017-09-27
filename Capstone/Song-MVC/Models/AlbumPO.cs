namespace Capstone_MVC.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class AlbumPO
    {
        //Constructor to instantiate new objects in case of Null Reference Exception
        public AlbumPO()
        {
            AlbumID = new long();

            ArtistID = new long();

            NumberOfTracks = new short();

            ReleaseDate = new DateTime();

            Sales = new int?();

            Duration = new TimeSpan();
        }

        [Required]
        [Display(Name = "ID: ")]
        public long AlbumID { get; set; }

        [Required(ErrorMessage = "Please enter the album type.")]
        [Display(Name = "Album Type")]
        public string AlbumType { get; set; }

        [Required]
        [Display(Name = "Artist")]
        public long ArtistID { get; set; }

        [Display(Name = "Audio Link")]
        public string AudioLink { get; set; }

        [Display(Name = "Gaon Award Wins")]
        public string GaonAwards { get; set; }

        public string Genre { get; set; }

        [Required(ErrorMessage = "Please enter album name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the number of tracks included on this album.")]
        [Display(Name = "Number Of Tracks")]
        public short NumberOfTracks { get; set; }

        [Required(ErrorMessage = "Please enter album duration.")]
        public TimeSpan Duration { get; set; }

        [Display(Name = "Picture URL")]
        public string PictureURL { get; set; }

        [Required(ErrorMessage = "Please enter album's release date.")]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "Please enter album's release type.")]
        [Display(Name = "Release Type")]
        public string ReleaseType { get; set; }

        public int? Sales { get; set; }

        public SelectList DropdownArtists { get; set; }

        public string ArtistName { get; set; }
    }
}