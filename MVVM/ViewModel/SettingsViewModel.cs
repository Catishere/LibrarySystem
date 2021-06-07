using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public bool AutoComplete
        {
            get => _autoComplete;
            set {
                _autoComplete = value;
                OnPropertyChanged();
            }

        }

        public List<string> DateIntervalSettingsList
        {
            get => _dateIntervalSettingsList;
            set
            {
                _dateIntervalSettingsList = value;
                OnPropertyChanged();
            }
        }

        public int InputLengthSuggestions
        {
            get => UserInfo.CurrentUser.Settings.InputLengthThreshold;
            set
            {
                UserInfo.CurrentUser.Settings.InputLengthThreshold = value;
                OnPropertyChanged();
            }
        }

        public string SelectedDateInterval
        {
            get => _selectedDateInterval;
            set
            {
                _selectedDateInterval = value;
                OnPropertyChanged();
            }
        }

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
            DateIntervalSettingsList = new List<string>
                {"Без", "Последният час", "Последният ден", "Последната седмица", "Последният месец", "Последната година"};

            SelectedDateInterval = UserInfo.CurrentUser.Settings.SuggestionTimeIntervalString;
            AutoComplete = UserInfo.CurrentUser.Settings.AutoComplete;
            UpdateSettingsCommand = new UpdateSettingsCommand(this);
            SuggestionCount = UserInfo.CurrentUser.Settings.SuggestionsCount;
            _userService = new UserService(new LibraryContext());
        }

        private IViewModel _currentViewModel;
        private IViewModel _currentViewModelParent;
        private int _suggestionCount;
        private bool _hasStatus;
        private string _statusMessage;
        private string _selectedDateInterval;
        private List<string> _dateIntervalSettingsList;
        private bool _autoComplete;

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
            settings.SuggestionTimeInterval = SelectedDateInterval switch
            {
                "Последният час" => (DateTime.Now.AddHours(1) - DateTime.Now).Ticks,
                "Последният ден" => (DateTime.Now.AddDays(1) - DateTime.Now).Ticks,
                "Последната седмица" => (DateTime.Now.AddDays(7) - DateTime.Now).Ticks,
                "Последният месец" => (DateTime.Now.AddMonths(1) - DateTime.Now).Ticks,
                "Последната година" => (DateTime.Now.AddYears(1) - DateTime.Now).Ticks,
                _ => 0
            };
            settings.SuggestionTimeIntervalString = SelectedDateInterval;
            settings.AutoComplete = _autoComplete;
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
