namespace Capstone_BLL.Models
{
    using Interfaces;
    using System;

    public class UserBO : IUserBO
    {
        //Constructor to instantiate new objects in case of Null Reference Exception
        public UserBO()
        {
            UserID = new long();

            Role = new short();

            Birthdate = new DateTime?();

            Suspended = new bool();
        }

        public string AboutMeContent { get; set; }

        public string EmailAddress { get; set; }

        public string ExternalLink { get; set; }

        public string FavoriteSongs { get; set; }

        public string FirstName { get; set; }

        public string Language { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public short Role { get; set; }

        public bool Suspended { get; set; }

        public long UserID { get; set; }

        public string Username { get; set; }

        public DateTime? Birthdate { get; set; }
    }
}
