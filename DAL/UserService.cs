using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.DAL.Interface;
using LibrarySystem.MVVM.Model.DB;
using LibrarySystem.Utils;

namespace LibrarySystem.DAL
{
    public class UserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Book> _bookRepository;
        public LibraryContext LibraryContext { get; set; }

        public UserService(IRepository<User> userRepository, IRepository<Book> bookRepository)
        {
            _userRepository = userRepository;
            _bookRepository = bookRepository;
        }
        public bool Login(string username, string password)
        {
            var users = _userRepository.Get(x => x.Username == username);
            var user = users.Any() ? users.First() : null;
            UserInfo.CurrentUser = user;
            return user != null && user.Password.Equals(Cryptography.EncodePassword(password));
        }

        public void AddFavouriteBook(int bookId)
        {
            var user = _userRepository.GetById(UserInfo.CurrentUser.UserId);
            Book bookDb = _bookRepository.GetById(bookId);
            if (user != null && bookDb != null)
            {
                user.FavouriteBooks.Add(bookDb);
            }
            _userRepository.Update(user);
            _userRepository.Save();
        }

        public void AddFavouritePassword(string password)
        {
            var user = _userRepository.GetById(UserInfo.CurrentUser.UserId);
            user?.FavouritePasswords.Add(new Password(Cryptography.Encrypt(password, UserInfo.CurrentUser.Password)));
            _userRepository.Update(user);
            _userRepository.Save();
        }

        public List<Book> GetFavouriteBooks()
        {
            int userId = UserInfo.CurrentUser.UserId;
            var query = _userRepository.GetAll().Where(u => u.UserId == userId).SelectMany(u => u.FavouriteBooks);
            return query.ToList();
        }

        public List<string> GetFavouritePasswords()
        {
            int userId = UserInfo.CurrentUser.UserId;
            var query = _userRepository.GetAll().Where(u => u.UserId == userId).SelectMany(u => u.FavouritePasswords);
            return query.ToList().ConvertAll(input => Cryptography.Decrypt(input.PasswordString, UserInfo.CurrentUser.Password));
        }

        public void Register(string loginUsername, string password)
        {
            _userRepository.Insert(new User(loginUsername, loginUsername, Cryptography.EncodePassword(password)));
            _userRepository.Save();
            UserInfo.CurrentUser = _userRepository.Get((user => user.Username == loginUsername)).FirstOrDefault();
        }
    }
}
