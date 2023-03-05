using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using ToDoList.Application.Interfaces;
using ToDoListCore.Models;

namespace ToDoList.Application.Data.TaskControllers
{
    internal class SQLiteEFTaskController : IRepository<ToDoModel>
    {
        public void AddData(ToDoModel data)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Add(data);
                db.SaveChanges();
            }
        }

        public void ChangeData(ToDoModel newData, ToDoModel oldData)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Remove(oldData);
                db.Add(newData);
                db.SaveChanges();
            }
        }

        public IQueryable<ToDoModel> GetData()
        {
            ApplicationContext db = new ApplicationContext();
            IQueryable<ToDoModel> models = db.ToDoModels;
            return models;
        }

        public void RemoveData(ToDoModel data)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Remove(data);
                db.SaveChanges();
            }
        }
    }
}
