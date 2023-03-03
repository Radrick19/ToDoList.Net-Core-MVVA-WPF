using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Application.Enums;

namespace ToDoList.Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetData();
        void AddData(T data);
        void RemoveData(T data);
        void ChangeData(T oldData, T newData);
    }
}