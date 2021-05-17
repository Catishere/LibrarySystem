using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LibrarySystem.Core;
using LibrarySystem.DAL;
using LibrarySystem.MVVM.ViewModel.Command;

namespace LibrarySystem.MVVM.ViewModel
{
    public class LoginViewModel : ObservableObject, IViewModel
    {
        private string _username;
        private string _password;
        private UserService _userService;
        private IViewModel _currentViewModel;
        private IViewModel _currentViewModelParent;
        private bool _hasError;
        private string _errorMessage;

        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        public string Username
        {
            get => _username;
            set
            {
                HasError = false;
                _username = value;
                OnPropertyChanged(nameof(HasError));
                OnPropertyChanged();
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                HasError = false;
                _password = value;
                OnPropertyChanged(nameof(HasError));
                OnPropertyChanged();
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        public bool HasError
        {
            get => _hasError;
            set
            {
                _hasError = value;
                OnPropertyChanged();
            }
        }

        public LoginViewModel()
        {
            CurrentViewModelParent = this;
            CurrentViewModel = null;
            _userService = new UserService(new UserRepository(new LibraryContext()), null);
            LoginCommand = new LoginCommand(this);
            RegisterCommand = new RegisterCommand(this);
        }

        public bool CanExecute()
        {
            return !string.IsNullOrEmpty(_username) && !string.IsNullOrEmpty(_password);
        }
        public void Login(object parameter)
        {
            if (_userService.Login(_username, _password)) {
                CurrentViewModelParent = new MainViewModel();
                CurrentViewModel = new HomeViewModel();
            }
            else
            {
                ErrorMessage = "Невалидни данни за вход!";
                HasError = true;
            }
        }

        public void Register(object parameter)
        {
            _userService.Register(_username, _password);
            CurrentViewModelParent = new MainViewModel();
            CurrentViewModel = new HomeViewModel();
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
