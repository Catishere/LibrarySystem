using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.MVVM.Model.DB
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
#nullable enable
        public string? DisplayName { get; set; }
        public string Password { get; set; }
        public List<Book> FavouriteBooks { get; set; }
        public List<Password> FavouritePasswords { get; set; }

        public User(string username, string? displayName, string password)
        {
            Username = username;
            DisplayName = displayName;
            Password = password;
            FavouriteBooks = new List<Book>();
            FavouritePasswords = new List<Password>();
        }
#nullable disable

        public User()
        {
            FavouriteBooks = new List<Book>();
            FavouritePasswords = new List<Password>();
        }
    }
}
