using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace LoginTest.Models
{
    public class User
    {
        public User(string password, string userName)
        {
            Password = password;
            UserName = userName;
        }

        [Required(ErrorMessage = "Password is required!")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }
    }
}
