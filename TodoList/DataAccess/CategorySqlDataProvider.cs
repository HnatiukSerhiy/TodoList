using TodoList.Models;
using System.Data.SqlClient;
using Dapper;
using TodoList.interfaces;

namespace TodoList.DataAccess
{
    public class CategorySqlDataProvider : ICategoryDataProvider
    {
        private readonly string connectionString;

        private readonly CategoryBuilder categoryBuilder = new CategoryBuilder();
        public CategorySqlDataProvider(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IEnumerable<CategoryModel> GetCategoryList()
        {
            string selectQuery = "select * from dbo.Category";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var categoryList = connection.Query<CategoryModel>(selectQuery);
                return categoryList.ToList();
            }
        }

        public CategoryModel CreateCategory(CategoryModel categoryModel)
        {
            var parameters = new
            {
                categoryName = categoryModel.Name
            };

            string insertQuery = $@"insert into dbo.Category (Name)
                                    values(@categoryName)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute(insertQuery, parameters);
            }

            return categoryModel;
        }

        public CategoryModel GetCategoryById(int id)
        {
            var parameters = new
            {
                Id = id
            };

            string selectQuery = @$"select * from dbo.Category where dbo.Category.Id=@Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var category = connection.Query<CategoryModel>(selectQuery, parameters).LastOrDefault();

                return category;
            }
        }

        public CategoryModel UpdateCategory(CategoryModel categoryModel)
        {
            var parameters = new
            {
               Id = categoryModel.Id,
               Name = categoryModel.Name
            };

            string updateQuery = @$"update dbo.Category set Name=@Name where dbo.Category.Id=@Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute(updateQuery, parameters);
            }
            return categoryModel;
        }

        public int DeleteCategory(int id)
        {
            var parameters = new
            {
                Id = id
            };

            string updateQuery = @$"delete from dbo.Category where Id=@Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute(updateQuery, parameters);
            }

            return id;
        }
    }
}
