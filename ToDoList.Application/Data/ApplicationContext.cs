using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListCore.Models;

namespace ToDoList.Application.Data
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<ToDoModel> ToDoModels { get; set; } = null;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Data/TaskDB.db");
        }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
    }
}
