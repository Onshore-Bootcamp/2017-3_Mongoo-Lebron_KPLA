namespace Capstone_MVC.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class UserPO
    {
        //Constructor to instantiate new objects in case of null reference exception
        public UserPO()
        {
            UserID = new long();

            Role = new short();

            Birthdate = new DateTime?();

            Suspended = new bool();
        }

        [Required]
        [Display(Name = "ID: ")]
        public long UserID { get; set; }

        [Required(ErrorMessage = "Please enter a username.")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Power Level: ")]
        public short Role { get; set; }

        [Required(ErrorMessage = "Please enter your first name.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your last name.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter your e-mail address.")]
        [Display(Name = "E-mail Address")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Please enter a password.")]
        public string Password { get; set; }

        [Display(Name = "Preferred Language: ")]
        public string Language { get; set; }

        [Display(Name = "Date Of Birth: ")]
        public DateTime? Birthdate { get; set; }

        [Display(Name = "Favorite Songs: ")]
        public string FavoriteSongs { get; set; }

        [Display(Name = "Personal Links")]
        public string ExternalLink { get; set; }

        [Display(Name = "About Me: ")]
        public string AboutMeContent { get; set; }

        [Required]
        [Display(Name = "Suspended: ")]
        public bool Suspended { get; set; }

        //Variables for User's role display
        [Display(Name = "User")]
        public string RoleDisplayUser { get; set; }

        [Display(Name = "Power User")]
        public string RoleDisplayPowerUser { get; set; }

        [Display(Name = "Administrator")]
        public string RoleDisplayAdmin { get; set; }

        //Labels for display
        [Display(Name = "Profile")]
        public string UsersProfile { get; set; }
    }
}