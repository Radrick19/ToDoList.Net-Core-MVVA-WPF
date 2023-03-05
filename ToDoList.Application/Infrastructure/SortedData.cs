using ToDoList.Application.Enums;
using ToDoListCore.Models;

namespace ToDoList.Application.Infrastructure
{
    public static class SortedData
    {
        public static List<ToDoModel> GetData(IQueryable<ToDoModel> models, SortStatus sortStatus)
        {
            switch (sortStatus)
            {
                case SortStatus.All:
                    return models.ToList();
                case SortStatus.Active:
                    return models.Where(item => !item.IsDone).ToList();
                case SortStatus.Done:
                    return models.Where(item => item.IsDone).ToList();
                default:
                    return models.ToList();
            }
        }
    }
}
