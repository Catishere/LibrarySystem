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
    public class UserRepository : IRepository<User>
    {
        private readonly LibraryContext _context;
        public UserRepository(LibraryContext context)
        {
            _context = context;
        }

        public DbSet<User> GetAll()
        {
            return _context.Users;
        }

        public IList<User> GetAllList()
        {
            return _context.Users.ToList();
        }

        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public IList<User> Get(Expression<Func<User, bool>> @where)
        {
            return _context.Users.Where(where).ToList();
        }

        public void Insert(User entity)
        {
            _context.Users.Add(entity);
        }

        public void Delete(int id)
        {
            User user = _context.Users.Find(id);
            if (user != null)
                _context.Users.Remove(user);
        }

        public void Update(User entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
