using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.MVVM.Model.DB
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }
        public string Name { get; set; }
        public List<string> AlternativeNames { get; set; }

    }
}