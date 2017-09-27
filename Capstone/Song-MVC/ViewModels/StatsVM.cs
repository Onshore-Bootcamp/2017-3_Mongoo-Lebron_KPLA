namespace Capstone_MVC.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class StatsVM
    {
        //Constructor in case of null reference exception
        public StatsVM()
        {
            AverageNumberOfTracksOnAlbums = new int();

            AverageNumberOfMembersInArtists = new int();

            AverageYearsActiveOfArtists = new int();
        }

        [Display(Name = "The average Number of Tracks on an Album in our DataBase is: ")]
        public int AverageNumberOfTracksOnAlbums { get; set; }

        [Display(Name = "The average Number of Members for an Artist in our DataBase is: ")]
        public int AverageNumberOfMembersInArtists { get; set; }

        [Display(Name = "The average Number of Years an Artist in our DataBase has been Active is: ")]
        public int AverageYearsActiveOfArtists { get; set; }
    }
}