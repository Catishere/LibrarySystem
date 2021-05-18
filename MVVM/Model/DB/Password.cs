using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.MVVM.Model.DB
{
    public class Password
    {
        [Key]
        public int PasswordId { get; set; }
        public string PasswordString { get; set; }

        public Password()
        {
        }

        public Password(string passwordString)
        {
            PasswordString = passwordString;
        }
    }
}