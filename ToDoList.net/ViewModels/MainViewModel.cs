using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using ToDoList.Application.Data;
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
        public List<ToDoModel> SortedList
        {
            get 
            {
                IQueryable<ToDoModel> models = TaskHelper.db.GetData();
                return SortedData.GetData(models, sortStatus);
            }
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
                Set(ref sortStatus, value, nameof(SortedList));
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
                        TaskHelper.db.AddData(new ToDoModel(TaskText));
                        Set(ref taskText, string.Empty, nameof(TaskText));
                        OnPropertyChanged(nameof(SortedList));
                    }
                });
            }
        }

        private void DeleteItem(object obj)
        {
            ToDoModel item = obj as ToDoModel;
            TaskHelper.db.RemoveData(item);
            OnPropertyChanged(nameof(SortedList));
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
            OnPropertyChanged(nameof(SortedList));
        }

        public MainViewModel()
        {
            ToDoModel.DeleteClickEvent += DeleteItem;
            ToDoModel.SortInfoChanged += UpdateListInfo;
        }
    }
}
