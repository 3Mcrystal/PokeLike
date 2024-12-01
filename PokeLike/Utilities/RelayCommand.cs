using System;
using System.Windows.Input;

namespace PokeLike.Utilities
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;
        private Action handleLogin;

        public RelayCommand(Action<object> execute, Action handleLogin, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.handleLogin = handleLogin;
            _canExecute = canExecute;
        }

        public RelayCommand(Action handleLogin, Action<object> execute, Func<object, bool> canExecute)
        {
            this.handleLogin = handleLogin;
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}