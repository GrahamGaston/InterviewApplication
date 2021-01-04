using System;
using System.Windows.Input;

namespace Mvvm.Commands
{
    /// <inheritdoc />
    /// <summary>
    /// RelayCommand class for MVVM
    /// </summary>
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// Action to perform
        /// </summary>
        private readonly Action<object> _execute;
        /// <summary>
        /// predicate on action
        /// </summary>
        private readonly Predicate<object> _canExecute;

        /// <summary>
        /// Relay command object
        /// </summary>
        /// <param name="execute">Action to execute</param>
        /// <param name="canExecute">Predicate to check. Optional.</param>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        /// <inheritdoc />
        /// <summary>
        /// Predicate for Action
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke(parameter) ?? true;
        }

        /// <summary>
        /// CanExecuteChanged
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        /// <inheritdoc />
        /// <summary>
        /// Execute
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }  
}
