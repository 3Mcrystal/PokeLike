using System.Windows;
using System.Windows.Controls;
using PokeLike.MVVM.ViewModel;

namespace PokeLike.MVVM.View
{
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                // On vérifie que le DataContext est bien le ViewModel et qu'il a la propriété Password
                var viewModel = (LoginVM)this.DataContext;
                if (viewModel != null)
                {
                    viewModel.Password = passwordBox.Password;
                }
            }
        }
    }
}