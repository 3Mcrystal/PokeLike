using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace PokeLike.MVVM.ViewModel
{
    public class MainWindowVM : BaseVM
    {
        // Event => On User request to change ViewModel & view
        static public Action<BaseVM> OnRequestVMChange;

        #region Variables

        /// <summary>
        /// this the current view model where the user stands
        /// </summary>
        private BaseVM _currentVM;
        public BaseVM CurrentVM
        {
            get => _currentVM;
            set
            {
                // Using the toolkit to notify the binding of variables for the view
                SetProperty(ref _currentVM, value);
                OnPropertyChanged(nameof(CurrentVM));
            }
        }

        #endregion

        // Constructor of MainWindowVM
        public MainWindowVM()
        {
            // Subscribe to HandleRequestViewChange event
            // This will be triggered when we need to switch views
            MainWindowVM.OnRequestVMChange += HandleRequestViewChange;

            // Set the initial view model, e.g., LoginView
            MainWindowVM.OnRequestVMChange?.Invoke(new LoginVM());
        }

        /// <summary>
        /// Called when the Event is Invoked (OnRequestVMChange)
        /// </summary>
        /// <param name="a_VMToChange">The new ViewModel to switch to</param>
        public void HandleRequestViewChange(BaseVM a_VMToChange)
        {
            // Notify that the current view model will be hidden
            CurrentVM?.OnHideVM();

            // Assign the new ViewModel to CurrentVM
            CurrentVM = a_VMToChange;

            // Notify that the new ViewModel will be shown
            CurrentVM?.OnShowVM();
        }
    }
}