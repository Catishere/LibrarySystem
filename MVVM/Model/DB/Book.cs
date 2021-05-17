using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.MVVM.Model.DB
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string Title { get; set; }
#nullable enable
        public List<string> Authors { get; set; }
        public string Publisher { get; set; }
        public string Isbn { get; set; }
        public string Summary { get; set; }
        public List<Genre> Genres { get; set; }
        public string Language { get; set; }
        public int Year { get; set; }
        public int Edition { get; set; }
        public List<User> FavouriteOf { get; set; }
#nullable disable

        public Book()
        {
            FavouriteOf = new List<User>();
        }
    }
}
