using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LibrarySystem.MVVM.ViewModel
{
    public interface IViewModelSuggestions : IViewModel
    {
        public ICommand CycleSuggestionCommand { get; set; }
        public int SuggestionIndex { get; set; }
        public bool IsCycling { get; set; }
        public void ExecuteCycleSuggestions(object parameter);
    }
}
