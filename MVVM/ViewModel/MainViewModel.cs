﻿using System.Collections.Generic;
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
    public class MainViewModel : ObservableObject, IViewModel
    {
        private IViewModel _currentViewModel;

        private Book _suggestionEntry;
        private readonly List<Book> _allSuggestions;
        private List<Book> _suggestions;
        private IViewModel _currentViewModelParent;
        private readonly UserService _userService;
        public ICommand HomeCommand { get; set; }
        public ICommand LibraryCommand { get; set; }
        public ICommand SettingsCommand { get; set; }
        public ICommand CycleSuggestionCommand { get; set; }

        public int SuggestionIndex { get; set; } = -1;

        public Book SuggestionEntry
        {
            get => _suggestionEntry;
            set
            {
                if (!_allSuggestions.Contains(value) && IsCycling 
                    || Suggestions == null 
                    || !Suggestions.Any())
                    IsCycling = false;

                _suggestionEntry = value;
                if (_suggestionEntry != null && !IsCycling)
                    Suggestions = _allSuggestions.Where(s => s.Title.ToLower().Contains(_suggestionEntry.Title.ToLower())).ToList();
                OnPropertyChanged();
            }
        }

        public bool IsCycling { get; set; }

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
            CycleSuggestionCommand = new CycleSuggestionCommand(this);
        }
    }
}