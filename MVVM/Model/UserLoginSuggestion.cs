using LibrarySystem.Core;

namespace LibrarySystem.MVVM.Model
{
    public class UserLoginSuggestion : ObservableObject
    {
        private string _username;
        private string _password;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public UserLoginSuggestion(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public override bool Equals(object obj)
        {
            if (obj is UserLoginSuggestion other)
            {
                return Username == other.Username && Password == other.Password;
            }

            return false;
        }

        public UserLoginSuggestion()
        {
        }
    }
}