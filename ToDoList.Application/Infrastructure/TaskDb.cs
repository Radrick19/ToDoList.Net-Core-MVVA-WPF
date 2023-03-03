using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Application.Data;
using ToDoList.Application.Interfaces;
using ToDoListCore.Models;

namespace ToDoList.Application.Infrastructure
{
    public static class TaskDb
    {
        public static IRepository<ToDoModel> db;

        static TaskDb()
        {
            db = new SQLiteTaskController();
        }
    }
}
