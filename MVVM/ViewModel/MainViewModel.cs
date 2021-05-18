using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LibrarySystem.Core;
using LibrarySystem.DAL;
using LibrarySystem.MVVM.Model.DB;
using LibrarySystem.MVVM.ViewModel.Command;

namespace LibrarySystem.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject, IViewModelSuggestions
    {
        private IViewModel _currentViewModel;

        private Book _suggestionEntry;
        private readonly List<Book> _allSuggestions;
        private List<Book> _suggestions;
        private IViewModel _currentViewModelParent;
        private readonly UserService _userService;
        private bool _autoComplete = true;
        private KeyValuePair<Type, string> _itemKeyPair;

        public ICommand HomeCommand { get; set; }
        public ICommand LibraryCommand { get; set; }
        public ICommand SettingsCommand { get; set; }
        public ICommand CycleSuggestionCommand { get; set; }

        public int SuggestionIndex { get; set; } = -1;
        public bool IsCycling { get; set; }

        public KeyValuePair<Type, string> ItemKeyPair
        {
            get => _itemKeyPair;
            set
            {
                _itemKeyPair = value;
                OnPropertyChanged();
            }
        }

        public Book SuggestionEntry
        {
            get => _suggestionEntry;
            set
            {
                if (!_allSuggestions.Contains(value) && IsCycling
                    || Suggestions == null
                    || !Suggestions.Any())
                {
                    IsCycling = false;
                    SuggestionIndex = -1;
                }

                _suggestionEntry = value;
                if (_suggestionEntry != null && !IsCycling)
                    Suggestions = _allSuggestions.Where(s => s.Title.ToLower().Contains(_suggestionEntry.Title.ToLower())).ToList();
                if (_autoComplete
                    && !IsCycling
                    && Suggestions != null
                    && Suggestions.Any()
                    && _suggestionEntry != null
                    && _suggestionEntry.Title.Length >= 4)
                    _suggestionEntry = Suggestions.First();
                OnPropertyChanged();
            }
        }

        public List<Book> Suggestions
        {
            get => _suggestions;
            set
            {
                _suggestions = value;
                OnPropertyChanged();
            }
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

        public MainViewModel()
        {
            LibraryContext libraryContext = new LibraryContext();
            _userService = new UserService(new UserRepository(libraryContext), new BookRepository(libraryContext));
            CurrentViewModelParent = this;
            CurrentViewModel = new HomeViewModel();
            HomeCommand = new NavigationCommand<HomeViewModel>(this);
            LibraryCommand = new NavigationCommand<LibraryViewModel>(this);
            SettingsCommand = new NavigationCommand<SettingsViewModel>(this);
            _allSuggestions = _userService.GetFavouriteBooks();
            if (_allSuggestions.Any())
                ItemKeyPair = new KeyValuePair<Type, string>(_allSuggestions.First().GetType(), "Title");
            CycleSuggestionCommand = new CycleSuggestionCommand(this);
        }

        public void ExecuteCycleSuggestions(object parameter)
        {
            if (!Suggestions.Any()) return;
            int index = SuggestionIndex + int.Parse((string)parameter);
            index = index % Math.Min(5, Suggestions.Count);
            IsCycling = true;
            if (index < 0)
                index = Suggestions.Count - 1;
            SuggestionEntry = Suggestions[index];
            SuggestionIndex = index;
        }
    }
}
