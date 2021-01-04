using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Mvvm
{
    /// <summary>
    /// Implements INotifyPropertyChanged
    /// </summary>
    public abstract class ObservableBase : INotifyPropertyChanged
    {

        /// <summary>
        /// Event handler for PropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// OnPropertyChanged implementation.
        /// </summary>
        /// <param name="propertyName">Property name - not needed</param>
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Setter for properties implementing OnPropertyChanged
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        public void SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return;
            field = value;
            OnPropertyChanged(propertyName);
        }

    }
}
