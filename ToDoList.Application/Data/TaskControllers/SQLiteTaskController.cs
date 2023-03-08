using System.Data;
using System.Data.SQLite;
using System.Windows;
using ToDoList.Application.Interfaces;
using ToDoListCore.Models;

namespace ToDoList.Application.Data.TaskControllers
{
    internal class SQLiteTaskController : IRepository<ToDoModel>
    {
        private static SQLiteConnection sqlConnection;
        private static SQLiteCommand sqlCommand;

        public void AddData(ToDoModel data)
        {
            SQLiteTransaction transaction = sqlConnection.BeginTransaction();
            try
            {
                sqlCommand.CommandText = "INSERT INTO [Tasks] (id, task, isDone) VALUES (:id, :task, :isDone)";
                sqlCommand.Parameters.AddWithValue("id", data.Id);
                sqlCommand.Parameters.AddWithValue("task", data.Task);
                sqlCommand.Parameters.AddWithValue("isDone", Convert.ToInt32(data.IsDone));
                sqlCommand.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                MessageBox.Show("Can't add data to DB : " + ex.Message);
            }
        }

        public void ChangeData(ToDoModel newData, ToDoModel oldData)
        {
            sqlCommand.CommandText = $"DELETE FROM [Tasks] WHERE [id]='{oldData.Id}'";
            sqlCommand.ExecuteNonQuery();
            AddData(newData);
        }

        public void RemoveData(ToDoModel data)
        {
            sqlCommand.CommandText = $"DELETE FROM [Tasks] WHERE [id]='{data.Id}'";
            sqlCommand.ExecuteNonQuery();
        }

        public IQueryable<ToDoModel> GetData()
        {
            sqlCommand.CommandText = "SELECT * FROM Tasks";
            IList<ToDoModel> returnModel = new List<ToDoModel>();
            DataTable dt = new DataTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlCommand);
            adapter.Fill(dt);
            foreach (DataRow item in dt.Rows)
            {
                string task = item.Field<string>("task");
                long id = item.Field<long>("id");
                bool isDone = Convert.ToBoolean(item.Field<long>("isDone"));
                returnModel.Add(new ToDoModel(task, isDone, id));
            }
            return returnModel.AsQueryable();
        }

        public SQLiteTaskController(string dataPath = "Data/TaskDB.db")
        {
            sqlConnection = new SQLiteConnection();
            sqlCommand = new SQLiteCommand();
            if (DbExists())
            {
                sqlConnection = new SQLiteConnection("Data Source=" + dataPath + ";Version=3;");
                sqlConnection.Open();
                sqlCommand.Connection = sqlConnection;
            }
            //Почему он не хочет заходить в елс?
            else
            {
                CreateDataBase("Data/TaskDB.db");
            }
        }

        private bool DbExists()
        {
            if (File.Exists("Data/TaskDB.db"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static void CreateDataBase(string dataPath)
        {
            try
            {
                Directory.CreateDirectory("Data");
                sqlConnection = new SQLiteConnection("Data Source=" + dataPath + ";Version=3;FailIfMissing=False");
                sqlConnection.Open();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "CREATE TABLE IF NOT EXISTS [Tasks] ([id] INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE, [task] TEXT, [isDone] INTEGER)";
                sqlCommand.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Can't create data base : " + ex.Message);
            }
        }
    }
}
