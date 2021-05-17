using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.Core;

namespace LibrarySystem.MVVM.ViewModel
{
    public class SettingsViewModel : ObservableObject, IViewModel
    {
        private IViewModel _currentViewModel;
        private IViewModel _currentViewModelParent;

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
