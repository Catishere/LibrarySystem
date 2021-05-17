using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LibrarySystem.Core;

namespace LibrarySystem.MVVM.ViewModel
{
    public class LibraryViewModel : ObservableObject, IViewModel
    {
        private string _suggestionEntry;
        private readonly List<string> _allSuggestions;
        private List<string> _suggestions;
        private IViewModel _currentViewModel;
        private IViewModel _currentViewModelParent;

        public string SuggestionEntry
        {
            get => _suggestionEntry;
            set
            {
                _suggestionEntry = value;
                if (_suggestionEntry != null)
                    Suggestions = _allSuggestions.Where(s => s.ToLower().Contains(_suggestionEntry.ToLower())).ToList();
                OnPropertyChanged();
            }
        }

        public List<string> Suggestions
        {
            get => _suggestions;
            set
            {
                _suggestions = value;
                OnPropertyChanged();
            }
        }

        public LibraryViewModel()
        {
            _allSuggestions = new List<string> {"Football", "Basketball", "Softball", "Rugby", "Hockey"};
            _suggestions = new List<string>();
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
