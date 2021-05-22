using System;
using System.Windows.Input;

namespace LibrarySystem.MVVM.ViewModel.Command
{
    public class UpdateSettingsCommand : ICommand
    {
        private SettingsViewModel _settingsViewModel;
        public UpdateSettingsCommand(SettingsViewModel settingsViewModel)
        {
            _settingsViewModel = settingsViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return _settingsViewModel.CanExecute();
        }

        public void Execute(object parameter)
        {
            _settingsViewModel.Execute();
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}