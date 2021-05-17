using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace LibrarySystem.MVVM.ViewModel.Command
{
    public class CycleSuggestionCommand : ICommand
    {
        private readonly MainViewModel _viewModel;
        public CycleSuggestionCommand(MainViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (!_viewModel.Suggestions.Any()) return;
            int index = _viewModel.SuggestionIndex + int.Parse((string)parameter);
            index = index % Math.Min(5, _viewModel.Suggestions.Count);
            _viewModel.IsCycling = true;
            if (index < 0)
                index = _viewModel.Suggestions.Count - 1;
            _viewModel.SuggestionEntry = _viewModel.Suggestions[index];
            _viewModel.SuggestionIndex = index;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}