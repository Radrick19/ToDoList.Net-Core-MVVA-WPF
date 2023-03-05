using ToDoList.Application.Interfaces;
using ToDoListCore.Models;

namespace ToDoList.Application.Data
{
    public static class TaskHelper
    {
        public static IRepository<ToDoModel> db;

        static TaskHelper()
        {
            db = new SQLiteEFTaskController();
        }
    }
}
