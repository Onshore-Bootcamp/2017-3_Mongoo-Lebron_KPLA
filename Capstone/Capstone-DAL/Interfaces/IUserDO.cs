namespace Capstone_DAL.Interfaces
{
    using System;

    public interface IUserDO
    {
        long UserID { get; set; }

        string Username { get; set; }

        short Role { get; set; }

        string FirstName { get; set; }

        string LastName { get; set; }

        string EmailAddress { get; set; }

        string Password { get; set; }

        string Language { get; set; }

        DateTime? Birthdate { get; set; }

        string FavoriteSongs { get; set; }

        string ExternalLink { get; set; }

        string AboutMeContent { get; set; }

        bool Suspended { get; set; }
    }
}