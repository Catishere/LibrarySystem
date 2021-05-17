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
    public class BookService
    {
        private readonly IRepository<Book> _bookRepository;
        public LibraryContext LibraryContext { get; set; }

        public BookService(IRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public bool Login(string username, SecureString password)
        {
            return true;
        }

        public void Register(string loginUsername, SecureString loginSecurePassword)
        {
            _bookRepository.Save();
        }
    }
}
