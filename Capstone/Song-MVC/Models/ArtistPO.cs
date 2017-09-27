namespace Capstone_MVC.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ArtistPO
    {
        //Constructor to instantiate new objects in case of null reference exception
        public ArtistPO()
        {
            ArtistID = new long();

            NumberOfMembers = new short();

            YearsActive = new short();
        }
        
        [Required]
        [Display(Name = "ID: ")]
        public long ArtistID { get; set; }

        [Required(ErrorMessage = "Please enter artist's name.")]
        public string Name { get; set; }

        public string Genre { get; set; }

        [Required(ErrorMessage = "Please enter the number of members.")]
        [Display(Name = "Number of Members")]
        public short NumberOfMembers { get; set; }

        [Display(Name = "Picture URL")]
        public string PictureURL { get; set; }

        [Required(ErrorMessage = "Please enter artist's current status.")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Please enter the number of years artist has been active.")]
        [Display(Name = "Years Active")]
        public short YearsActive { get; set; }

        public string Company { get; set; }

        [Display(Name = "Gaon Award Wins")]
        public string GaonAwards { get; set; }

        [Display(Name = "Official Link")]
        public string ExternalLink { get; set; }
    }
}