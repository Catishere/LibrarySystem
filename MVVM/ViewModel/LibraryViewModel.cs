using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using LibrarySystem.Core;
using LibrarySystem.DAL;
using LibrarySystem.MVVM.ViewModel.Command;

namespace LibrarySystem.MVVM.ViewModel
{
    public class LibraryViewModel : ObservableObject, IViewModelSuggestions
    {
        private string _passwordEntry;
        private List<string> _allPasswords;
        private List<string> _passwords;
        private IViewModel _currentViewModel;
        private IViewModel _currentViewModelParent;
        private KeyValuePair<object, string> _itemKeyPair;

        public ICommand CycleSuggestionCommand { get; set; }
        public int SuggestionIndex { get; set; } = -1;
        public bool IsCycling { get; set; }

        public void ExecuteCycleSuggestions(object parameter)
        {
            if (!Passwords.Any()) return;
            int index = SuggestionIndex + int.Parse((string)parameter);
            index = index % Math.Min(5, Passwords.Count);
            IsCycling = true;
            if (index < 0)
                index = Passwords.Count - 1;
            PasswordEntry = Passwords[index];
            SuggestionIndex = index;
        }

        public KeyValuePair<object, string> ItemKeyPair
        {
            get => _itemKeyPair;
            set
            {
                _itemKeyPair = value;
                OnPropertyChanged();
            }
        }

        public string PasswordEntry
        {
            get => _passwordEntry;
            set
            {
                if (!_allPasswords.Contains(value) && IsCycling
                    || Passwords == null
                    || !Passwords.Any())
                {
                    IsCycling = false;
                    SuggestionIndex = -1;
                }
                _passwordEntry = value;
                if (_passwordEntry != null && !IsCycling)
                    Passwords = _allPasswords.Where(s => s.ToLower().Contains(_passwordEntry.ToLower())).ToList();
                OnPropertyChanged();
            }
        }

        public List<string> Passwords
        {
            get => _passwords;
            set
            {
                _passwords = value;
                OnPropertyChanged();
            }
        }
        public List<string> AllPasswords
        {
            get => _allPasswords;
            set
            {
                _allPasswords = value;
                OnPropertyChanged();
            }
        }

        public LibraryViewModel()
        {
            var libraryContext = new LibraryContext();
            var _userService = new UserService(new UserRepository(libraryContext), new BookRepository(libraryContext));
            _allPasswords = _userService.GetFavouritePasswords();
            _passwords = new List<string>();
            _passwordEntry = "";
            ItemKeyPair = new KeyValuePair<object, string>(_passwordEntry, "");
            CycleSuggestionCommand = new CycleSuggestionCommand(this);
        }

        public IViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }

        public IViewModel CurrentViewModelParent
        {
            get => _currentViewModelParent;
            set
            {
                _currentViewModelParent = value;
                OnPropertyChanged();
            }
        }
    }
}
