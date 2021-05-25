using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.MVVM.Model.DB;
using LibrarySystem.Utils;

namespace LibrarySystem.DAL
{
    public class UserService
    {
        private readonly LibraryContext _libraryContext;
        public UserService(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }
        public bool Login(string username, string password)
        {
            var user = _libraryContext.Users
                .Where(u => u.Username == username)
                .Include("Settings")
                .FirstOrDefault();
            UserInfo.CurrentUser = user;
            return user != null && user.Password.Equals(Cryptography.EncodePassword(password));
        }

        public void AddFavouriteBook(int bookId)
        {
            var user = _libraryContext.Users.Find(UserInfo.CurrentUser.UserId);
            Book bookDb = _libraryContext.Books.Find(bookId);
            if (user != null && bookDb != null)
            {
                user.FavouriteBooks.Add(bookDb);
            }
            _libraryContext.Entry(user).State = EntityState.Modified;
            _libraryContext.SaveChanges();
        }

        public void AddFavouritePassword(string password)
        {
            var user = _libraryContext.Users.Find(UserInfo.CurrentUser.UserId);
            user?.FavouritePasswords.Add(new Password(Cryptography.Encrypt(password, UserInfo.CurrentUser.Password)));
            _libraryContext.Entry(user).State = EntityState.Modified;
            _libraryContext.SaveChanges();
        }

        public List<Book> GetFavouriteBooks()
        {
            int userId = UserInfo.CurrentUser.UserId;
            var query = _libraryContext.Users.Where(u => u.UserId == userId).SelectMany(u => u.FavouriteBooks);
            return query.ToList();
        }

        public List<string> GetFavouritePasswords()
        {
            int userId = UserInfo.CurrentUser.UserId;
            var query = _libraryContext.Users.Where(u => u.UserId == userId).SelectMany(u => u.FavouritePasswords);
            return query.ToList().ConvertAll(input => Cryptography.Decrypt(input.PasswordString, UserInfo.CurrentUser.Password));
        }

        public bool UpdateUserSettings(int userId, Settings settings)
        {
            var user = _libraryContext.Users.Find(userId);
            if (user == null) return false;
            user.Settings = settings;
            _libraryContext.Entry(user).State = EntityState.Modified;
            _libraryContext.SaveChanges();
            return true;
        } 

        public void Register(string loginUsername, string password)
        {
            var user = _libraryContext.Users.FirstOrDefault(user => user.Username == loginUsername);
            if (user == null)
            {
                _libraryContext.Users.Add(new User(loginUsername, loginUsername, Cryptography.EncodePassword(password)));
                _libraryContext.SaveChanges();
            }
            UserInfo.CurrentUser = _libraryContext.Users.FirstOrDefault(user => user.Username == loginUsername);
        }
    }
}
