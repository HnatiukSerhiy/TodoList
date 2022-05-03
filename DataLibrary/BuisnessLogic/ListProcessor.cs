using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using DataLibrary.Model;
using DataLibrary.DataAccess;

namespace DataLibrary.BuisnessLogic
{
    public static class ListProcessor
    {
        public static int CreateListItem(int id, string description, DateTime deadline, int categhoryId, bool isDone)
        {
            TodoModel data = new TodoModel
            {
                Id = id,
                Description = description,
                Deadline = deadline,
                CateghoryID = categhoryId,
                IsDone = isDone
            };

            string sql = @"insert into TODOListDB.dbo.List (ListID, ListName, LimitDate, CateghoryID, DoneStatus)
                            values (@Id, @Description, @Deadline, @CateghoryID, @IsDone)";


            return SqlDataAccess.SaveData(sql, data);
        }

        public static List<TodoModel> LoadTodo()
        {
            string sql = @"select ListID, ListName, LimitDate, CateghoryID, DoneStatus
                           from TODOListDB.dbo.List";

            return SqlDataAccess.LoadData<TodoModel>(sql);
        }
    }
}
