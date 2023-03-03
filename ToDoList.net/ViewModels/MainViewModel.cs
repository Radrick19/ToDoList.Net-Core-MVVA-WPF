using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using ToDoList.Application.Enums;
using ToDoList.Application.Infrastructure;
using ToDoListCore.Infrastructure.Command;
using ToDoListCore.Models;

namespace ToDoListCore.ViewModels
{
    class MainViewModel : BaseViewModel
    {

        public SolidColorBrush AllSortModeColor
        {
            get { return sortStatus == SortStatus.All ? new SolidColorBrush(Color.FromRgb(194, 231, 255)) : new SolidColorBrush(Color.FromRgb(255, 255, 255)); }
        }
        public SolidColorBrush ActiveSortModeColor
        {
            get { return sortStatus == SortStatus.Active ? new SolidColorBrush(Color.FromRgb(194, 231, 255)) : new SolidColorBrush(Color.FromRgb(255, 255, 255)); }
        }
        public SolidColorBrush DoneSortModeColor
        {
            get { return sortStatus == SortStatus.Done ? new SolidColorBrush(Color.FromRgb(194, 231, 255)) : new SolidColorBrush(Color.FromRgb(255, 255, 255)); }
        }
        public List<ToDoModel> ToDoList
        {
            get { return TaskDb.db.GetDataList(sortStatus).ToList(); }
            set { toDoList = value; }
        }
        public string TaskText
        {
            get { return taskText; }
            set { taskText = value; }
        }
        public SortStatus SortStatus
        {
            get { return sortStatus; }
            set
            {
                Set(ref sortStatus, value, nameof(ToDoList));
                OnPropertyChanged(nameof(AllSortModeColor));
                OnPropertyChanged(nameof(ActiveSortModeColor));
                OnPropertyChanged(nameof(DoneSortModeColor));
            }
        }
        private SortStatus sortStatus = SortStatus.Active;
        private string taskText;
        private List<ToDoModel> toDoList;

        public ICommand AddItem
        {
            get
            {
                return new RelayCommand((obj) =>
                {
                    if (TaskText != null && TaskText != string.Empty)
                    {
                        TaskDb.db.AddData(new ToDoModel(TaskText));
                        Set(ref taskText, string.Empty, nameof(TaskText));
                        OnPropertyChanged(nameof(ToDoList));
                    }
                });
            }
        }

        private void DeleteItem(object obj)
        {
            ToDoModel item = obj as ToDoModel;
            TaskDb.db.RemoveData(item);
            OnPropertyChanged(nameof(ToDoList));
        }

        public ICommand SetAllSort
        {
            get
            {
                return new RelayCommand((obj) =>
                {
                    SortStatus = SortStatus.All;
                });
            }
        }

        public ICommand SetActiveSort
        {
            get
            {
                return new RelayCommand((obj) =>
                {
                    SortStatus = SortStatus.Active;
                });
            }
        }

        public ICommand SetDoneSort
        {
            get
            {
                return new RelayCommand((obj) =>
                {
                    SortStatus = SortStatus.Done;
                });
            }
        }

        private void UpdateListInfo()
        {
            OnPropertyChanged(nameof(ToDoList));
        }

        public MainViewModel()
        {
            ToDoModel.DeleteClickEvent += DeleteItem;
            ToDoModel.SortInfoChanged += UpdateListInfo;
        }
    }
}
