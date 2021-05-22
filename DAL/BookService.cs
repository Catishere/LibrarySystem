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

        public void AddBook(Book book)
        {
            book.CreatedOn = DateTime.Now;
            book.ModifiedOn = DateTime.Now;
            _bookRepository.Insert(book);
            _bookRepository.Save();
        }

        public bool EditBook(Book book)
        {
            var bookdb = _bookRepository.GetById(book.BookId);
            if (bookdb == null) return false;
            bookdb.Copy(book);
            bookdb.ModifiedOn = DateTime.Now;
            _bookRepository.Update(bookdb);
            _bookRepository.Save();
            return true;
        }
    }
}
