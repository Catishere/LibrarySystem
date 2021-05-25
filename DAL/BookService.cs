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
    public class BookService
    {
        private readonly LibraryContext _libraryContext;

        public BookService(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }

        public void AddBook(Book book)
        {
            book.CreatedOn = DateTime.Now;
            book.ModifiedOn = DateTime.Now;
            _libraryContext.Books.Add(book);
            _libraryContext.SaveChanges();
        }

        public bool EditBook(Book book)
        {
            var bookdb = _libraryContext.Books.Find(book.BookId);
            if (bookdb == null) return false;
            bookdb.Copy(book);
            bookdb.ModifiedOn = DateTime.Now;
            _libraryContext.Entry(bookdb).State = EntityState.Modified;
            _libraryContext.SaveChanges();
            return true;
        }
    }
}
