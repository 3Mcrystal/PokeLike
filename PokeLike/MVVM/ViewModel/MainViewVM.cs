using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace PokeLike.MVVM.ViewModel
{
    public class MainViewVM : BaseVM
    {
        private object _currentVM;

        public object CurrentVM
        {
            get => _currentVM;
            set
            {
                _currentVM = value;
                OnPropertyChanged(nameof(CurrentVM));
            }
        }

        public MainViewVM()
        {
            // Initialiser avec la vue de login au démarrage
            CurrentVM = new LoginVM(); // Par exemple, commence avec LoginView
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
