using ToDoList.Application.Enums;
using ToDoListCore.Models;

namespace ToDoList.Application.Models
{
    public static class SortedData
    {
        public static List<ToDoModel> GetData(IQueryable<ToDoModel> queryable, SortStatus sortStatus)
        {
            switch (sortStatus)
            {
                case SortStatus.All:
                    return queryable.ToList();
                case SortStatus.Active:
                    return queryable.Where(item => !item.IsDone).ToList();
                case SortStatus.Done:
                    return queryable.Where(item => item.IsDone).ToList();
                default:
                    return queryable.ToList();
            }
        }
    }
}
