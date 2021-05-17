using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LibrarySystem.MVVM.ViewModel.Command
{
    internal class NavigationCommand<TViewModel> : ICommand
        where TViewModel : IViewModel, new()
    {
        private readonly Predicate<object> _canExecute;
        private readonly IViewModel _viewModel;
        public NavigationCommand(IViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public NavigationCommand(IViewModel viewModel, Predicate<object> predicate)
        {
            _viewModel = viewModel;
            _canExecute = predicate;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _viewModel.CurrentViewModel = new TViewModel();
        }
    }
}
