using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.DAL.Interface;
using LibrarySystem.MVVM.Model.DB;

namespace LibrarySystem.DAL
{
    public class BookRepository : IRepository<Book>
    {
        private readonly LibraryContext _context;
        public BookRepository(LibraryContext context)
        {
            _context = context;
        }

        public DbSet<Book> GetAll()
        {
            return _context.Books;
        }

        public IList<Book> GetAllList()
        {
            return _context.Books.ToList();
        }

        public Book GetById(int id)
        {
            return _context.Books.Find(id);
        }

        public IList<Book> Get(Expression<Func<Book, bool>> @where)
        {
            return _context.Books.Where(where).ToList();
        }

        public void Insert(Book entity)
        {
            _context.Books.Add(entity);
        }

        public void Delete(int id)
        {
            Book user = _context.Books.Find(id);
            if (user != null)
                _context.Books.Remove(user);
        }

        public void Update(Book entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
