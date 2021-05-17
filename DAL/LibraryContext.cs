using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.Migrations;
using LibrarySystem.MVVM.Model.DB;

namespace LibrarySystem.DAL
{
    public class LibraryContext : DbContext
    {
        public LibraryContext() : base("LibrarySystemDb")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<LibraryContext, Configuration>());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}
