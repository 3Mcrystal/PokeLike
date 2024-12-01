using System.Text;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using PokeLike.Model;
using PokeLike.MVVM.ViewModel;
using PokeLike.Utilities;
using RelayCommand = CommunityToolkit.Mvvm.Input.RelayCommand;

namespace PokeLike.MVVM.ViewModel
{

    public class LoginVM : BaseVM
    {
        private readonly ExerciceMonsterContext _dbContext;

        public LoginVM()
        {
            // Initialize the database context
            _dbContext = new ExerciceMonsterContext();

            // Initialize commands
            LoginCommand = new RelayCommand(Login);
            AddUserCommand = new RelayCommand(AddUser);
        }

        #region Properties

        private string _username;

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        private string _password;

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        #endregion

        #region Commands

        public IRelayCommand LoginCommand { get; }
        public IRelayCommand AddUserCommand { get; }

        private void Login()
        {
            try
            {
                var user = _dbContext.Logins
                    .FirstOrDefault(u => u.Username == Username && u.PasswordHash == ConvertToBase64(Password));

                if (user != null)
                {
                    MessageBox.Show("Login successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    // Trigger VM change to navigate to another view
                    MainWindowVM.OnRequestVMChange?.Invoke(new MainViewVM());
                }
                else
                {
                    MessageBox.Show("Invalid username or password!", "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during login: {ex.Message}", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void AddUser()
        {
            try
            {
                // Check if user already exists
                if (_dbContext.Logins.Any(u => u.Username == Username))
                {
                    MessageBox.Show("Username already exists!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Create and insert the new user
                var newUser = new Login
                {
                    Username = Username,
                    PasswordHash = ConvertToBase64(Password)
                };

                _dbContext.Logins.Add(newUser);
                _dbContext.SaveChanges();

                // Display confirmation message
                MessageBox.Show("User added successfully!", "Success", MessageBoxButton.OK,
                    MessageBoxImage.Information);

                // Clear fields
                Username = string.Empty;
                Password = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while adding user: {ex.Message}", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        #endregion

        #region Helpers

        private string ConvertToBase64(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
        }

        #endregion

        public ICommand BypassCommand => new RelayCommand<object>(param => HandleBypassLogin());

        private void HandleBypassLogin()
        {
            // Logique pour bypass la connexion et accéder à l'app principale
            MainWindowVM.OnRequestVMChange?.Invoke(new MainViewVM());
        }
    }
}