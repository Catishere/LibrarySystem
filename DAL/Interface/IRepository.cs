using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.MVVM.Model.DB;

namespace LibrarySystem.DAL.Interface
{
    public interface IRepository<T> where T : class
    {
        DbSet<T> GetAll();
        IList<T> GetAllList();
        T GetById(int id);
        IList<T> Get(Expression<Func<T, bool>> @where);
        void Insert(T entity);
        void Delete(int id);
        void Update(T entity);
        void Save();
    }
}
