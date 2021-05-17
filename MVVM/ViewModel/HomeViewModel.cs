using LibrarySystem.Core;

namespace LibrarySystem.MVVM.ViewModel
{
    public class HomeViewModel : ObservableObject, IViewModel
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
