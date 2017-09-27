namespace Capstone_MVC.ViewModels
{
    using Capstone_DAL.Interfaces;
    using Models;
    using System.Collections.Generic;

    public class UserVM
    {
        //Constructor to instantiate new objects in case of null reference exception
        public UserVM()
        {
            User = new UserPO();

            LoginUser = new LoginPO();

            ListOfUserDOs = new List<IUserDO>();

            ListOfUserPOs = new List<UserPO>();
        }

        public UserPO User { get; set; }

        public LoginPO LoginUser { get; set; }

        public List<IUserDO> ListOfUserDOs { get; set; }

        public List<UserPO> ListOfUserPOs { get; set; }
    }
}