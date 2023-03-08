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
                db.ToDoModels.Add(data);
                db.SaveChanges();
            }
        }

        public void ChangeData(ToDoModel newData, ToDoModel oldData)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.ToDoModels.Update(newData);
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
                db.ToDoModels.Remove(data);
                db.SaveChanges();
            }
        }
    }
}
