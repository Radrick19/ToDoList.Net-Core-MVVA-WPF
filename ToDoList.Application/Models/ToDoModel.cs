using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using ToDoList.Application.Data;
using ToDoListCore.Infrastructure.Command;

namespace ToDoListCore.Models
{
    public class ToDoModel : INotifyPropertyChanged
    {
        public static event Action<object> DeleteClickEvent;
        public static event Action SortInfoChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public SolidColorBrush IsDoneButtonBackgroundColor
        {
            get { return IsDone ? new SolidColorBrush(Color.FromRgb(52, 168, 83)) : new SolidColorBrush(Color.FromRgb(197, 34, 31)); }
        }

        public long? Id { get; set; }

        public string Task { get; set; }

        public bool IsDone { get; set; }

        public void OnPropertyChanged([CallerMemberName] string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public ICommand DeleteClick
        {
            get { return new RelayCommand((obj) => DeleteClickEvent(this)); }
        }

        public ICommand ChangeIsDoneStatus
        {
            get
            {
                return new RelayCommand((obj) => {
                    bool newIsDoneData;
                    if (IsDone)
                        newIsDoneData = false;
                    else
                        newIsDoneData = true;
                    TaskHelper.db.ChangeData(new ToDoModel(Task, newIsDoneData, Id), this);
                    OnPropertyChanged(nameof(IsDoneButtonBackgroundColor));
                    SortInfoChanged();
                });
            }
        }

        public ToDoModel(string task, bool isDone = false, long? id = null)
        {
            Task = task;
            IsDone = isDone;
            Id = id;
        }

    }
}
