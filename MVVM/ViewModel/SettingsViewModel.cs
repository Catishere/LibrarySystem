using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LibrarySystem.Core;
using LibrarySystem.DAL;
using LibrarySystem.MVVM.Model.DB;
using LibrarySystem.MVVM.ViewModel.Command;
using LibrarySystem.Utils;

namespace LibrarySystem.MVVM.ViewModel
{
    public class SettingsViewModel : ObservableObject, IViewModel
    {
        private readonly UserService _userService;
        public ICommand UpdateSettingsCommand { get; set; }

        public int SuggestionCount
        {
            get => _suggestionCount;
            set
            {
                _suggestionCount = value;
                OnPropertyChanged();
            }
        }

        public SettingsViewModel()
        {
            var lc = new LibraryContext();
            UpdateSettingsCommand = new UpdateSettingsCommand(this);
            SuggestionCount = UserInfo.CurrentUser.Settings.SuggestionsCount;
            _userService = new UserService(new UserRepository(lc), new BookRepository(lc));
        }

        private IViewModel _currentViewModel;
        private IViewModel _currentViewModelParent;
        private int _suggestionCount;
        private bool _hasStatus;
        private string _statusMessage;

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

        public bool CanExecute()
        {
            return _suggestionCount >= 0;
        }

        public void Execute()
        {
            var user = UserInfo.CurrentUser;
            var settings = user.Settings;
            settings.SuggestionsCount = _suggestionCount;
            _userService.UpdateUserSettings(user.UserId, settings);
            HasStatus = true;
            StatusMessage = "Настройките са запазени!";
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                OnPropertyChanged();
            }
        }

        public bool HasStatus
        {
            get => _hasStatus;
            set
            {
                _hasStatus = value;
                OnPropertyChanged();
            }
        }
    }
}
