namespace Capstone_MVC.Models
{
    using System.ComponentModel.DataAnnotations;

    public class LoginPO
    {
        [Required(ErrorMessage = "Please enter a valid username.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter a valid password.")]
        public string Password { get; set; }

        [Display(Name = "Oops, either username or password is wrong!  Try again.")]
        public string IncorrectMessage { get; set; }
    }
}