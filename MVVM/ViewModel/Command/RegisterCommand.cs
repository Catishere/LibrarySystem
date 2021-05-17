using System;
using System.Windows.Input;

namespace LibrarySystem.MVVM.ViewModel.Command
{
    internal class RegisterCommand : ICommand
    {
        private LoginViewModel _viewModel;
        public RegisterCommand(LoginViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return _viewModel.CanExecute();
        }

        public void Execute(object parameter)
        {
            _viewModel.Register(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}