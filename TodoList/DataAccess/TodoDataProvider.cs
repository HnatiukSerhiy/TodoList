using TodoList.Models;
using System.Data.SqlClient;
using Dapper;
using TodoList.interfaces;

namespace TodoList.DataAccess
{
    public class TodoDataProvider : ITodoDataProvider
    {
        private readonly string connectionString;

        public TodoDataProvider(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IEnumerable<TodoModel> GetCompleteTodo(int? categoryId)
        {
            var parameters = new
            {
                CategoryId = categoryId
            };

            string categoryСondition = categoryId.HasValue ? $"and dbo.List.CategoryId=@CategoryId" : "";

            string selectQuery = @$"select * from dbo.List left join dbo.Category
                                    on dbo.List.CategoryId=dbo.Category.Id
                                    where IsDone=1 {categoryСondition} order by DoneTime asc";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var todoList = connection.Query<TodoModel, CategoryModel, TodoModel>(selectQuery, map: (todo, category) =>
                {
                    todo.CategoryId = category != null ? category.Id : null;
                    todo.CategoryName = category != null ? category.CategoryName : null;

                    return todo;

                }, parameters);

                return todoList.ToList();
            }
        }

        public IEnumerable<TodoModel> GetUnCompleteTodo(int? categoryId)
        {
            var parameters = new
            {
                CategoryId = categoryId
            };

            string categoryCondition = categoryId.HasValue ? $"and dbo.List.CategoryId=@CategoryId" : "";

            string selectQuery = @$"select * from dbo.List left join dbo.Category
                                    on dbo.List.CategoryId = dbo.Category.Id
                                    where IsDone=0 {categoryCondition} order by case when Deadline is null then 1 else 0 end, Deadline";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var todoList = connection.Query<TodoModel, CategoryModel, TodoModel>(selectQuery, map: (todo, category) =>
                {
                    todo.CategoryId = category != null ? category.Id : null;
                    todo.CategoryName = category != null ? category.CategoryName : null;

                    return todo;

                }, parameters);

                return todoList.ToList();
            }
        }

        public void CreateTodo(TodoModel todoModel)
        {
            var parameters = new 
            { 
                todoId = todoModel.Id,
                todoDescription = todoModel.Description,
                todoDeadline = todoModel.Deadline,
                todoIsDone = todoModel.IsDone,
                categoryId = todoModel.CategoryId,
                categoryName = todoModel.CategoryName,

            };

            string insertQuery = $@"insert into dbo.List (Description, Deadline, CategoryId ,IsDone)
                                    values(@todoDescription, @todoDeadline, @categoryId, @todoIsDone)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute(insertQuery, parameters);
            }
        }

        public void SolveTodo(int id)
        {
            var parameters = new
            {
                Id = id,
                doneTime = DateTime.Now,
                todoIsDone = true
            };

            string updateQuery = $"update dbo.List set IsDone=@todoIsDOne, DoneTime=@doneTime where Id=@Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute(updateQuery, parameters);
            }
        }

        public TodoModel EditTodo(int id)
        {
            var parameters = new
            {
                Id = id
            };

            string selectQuery = @$"select * from dbo.List left join dbo.Category
                                    on dbo.List.CategoryId=dbo.Category.Id where dbo.List.Id=@Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                
                var todo = connection.Query<TodoModel, CategoryModel, TodoModel>(selectQuery, map: (todo, category) =>
                {
                    todo.CategoryId = category != null ? category.Id : null;
                    todo.CategoryName = category != null ? category.CategoryName : null;

                    return todo;

                }, parameters).LastOrDefault();

                return todo;
            }
        }

        public void UpdateTodo(TodoModel todoModel)
        {
            var parameters = new
            {
                todoId = todoModel.Id,
                todoDescription = todoModel.Description,
                todoDeadline = todoModel.Deadline,
                categoryId = todoModel.CategoryId,
                categoryName = todoModel.CategoryName,
            };

            string updateQuery = @$"update dbo.List set Description=@todoDescription, Deadline=@todoDeadline,
                                    CategoryId=@categoryId where dbo.List.Id = @todoId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute(updateQuery, parameters);
            }
        }

        public void DeleteTodo(int id)
        {
            var parameters = new
            {
                Id = id
            };

            string deleteQuery = $"delete from dbo.List where Id=@Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute(deleteQuery, parameters);
            }
        }
    }
}