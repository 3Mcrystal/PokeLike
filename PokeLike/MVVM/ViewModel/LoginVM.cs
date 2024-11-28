using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using PokeLike.Model;
using PokeLike.MVVM.ViewModel;
using PokeLike.Utilities;

namespace PokeLike.MVVM.ViewModel
{
   
    public class LoginVM : BaseVM
    {
        private string _username;
        private string _password;

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        // Commande de connexion
        public RelayCommand LoginCommand { get; }

        public LoginVM()
        {
            LoginCommand = new RelayCommand(HandleLogin);
        }

        private void HandleLogin()
        {
            // Logique de connexion (vérification des identifiants)
            bool loginSuccessful = CheckLogin(Username, Password);

            if (loginSuccessful)
            {
                // Si la connexion est réussie, changer de vue pour la vue principale
                MainWindowVM.OnRequestVMChange?.Invoke(new MainViewVM());
            }
            else
            {
                // Afficher un message d'erreur ou faire une autre action en cas d'échec
            }
        }

        private bool CheckLogin(string username, string password)
        {
            using var context = new ExerciceMonsterContext();
            // Trouver l'utilisateur dans la base de données
            var user = context.Logins.FirstOrDefault(u => u.Username == username);

            // Si l'utilisateur n'existe pas
            if (user == null)
            {
                return false;
            }

            // Comparer le mot de passe en Base64
            string passwordBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(password));

            return user.PasswordHash == passwordBase64;
        }
    }
}