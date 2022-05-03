using TodoList.Models;
using System.Data.SqlClient;

namespace TodoList.DataAccess
{
    public class TodoBuilder
    {
        public TodoModel Buid(SqlDataReader reader)
        {
            TodoModel model = new TodoModel
            {
                Id = Convert.ToInt32(reader["Id"]),
                Description = reader["Description"].ToString(),

                Deadline = reader["Deadline"] is DBNull ? null : Convert.ToDateTime(reader["Deadline"]),
                DoneTime = reader["DoneTime"] is DBNull ? null : Convert.ToDateTime(reader["DoneTime"]),

                CategoryId = reader["CategoryId"] is DBNull ? null : Convert.ToInt32(reader["CategoryId"]),
                CategoryName = reader["CategoryName"] is DBNull ? null : Convert.ToString(reader["CategoryName"]),
                IsDone = Convert.ToBoolean(reader["IsDone"])
            };

            return model;
        }
    }
}
