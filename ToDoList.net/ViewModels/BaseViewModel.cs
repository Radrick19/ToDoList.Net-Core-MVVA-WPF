using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ToDoListCore.ViewModels
{
    abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        protected void Set<T>(ref T item, T value, [CallerMemberName] string? property = null)
        {
            item = value;
            OnPropertyChanged(property);
        }
    }
}
