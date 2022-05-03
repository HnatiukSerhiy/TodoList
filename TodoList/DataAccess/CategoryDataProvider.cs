using TodoList.Models;
using System.Data.SqlClient;
using Dapper;
using TodoList.interfaces;

namespace TodoList.DataAccess
{
    public class CategoryDataProvider : ICategoryDataProvider
    {
        private readonly string connectionString;

        private readonly CategoryBuilder categoryBuilder = new CategoryBuilder();
        public CategoryDataProvider(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<CategoryModel> GetCategoryList()
        {
            string selectQuery = "select * from dbo.Category";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var categoryList = connection.Query<CategoryModel>(selectQuery);
                return categoryList.ToList();
            }
        }

        public void CreateCategory(CategoryModel model)
        {
            var parameters = new
            {
                categoryName = model.CategoryName
            };

            string insertQuery = $@"insert into dbo.Category (CategoryName)
                                    values(@categoryName)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute(insertQuery, parameters);
            }
        }

        public CategoryModel EditCategory(int id)
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

        public void UpdateCategory(CategoryModel model)
        {
            var parameters = new
            {
               categoryId = model.Id,
               categoryName = model.CategoryName
            };

            string updateQuery = @$"update dbo.Category set CategoryName=@categoryName where dbo.Category.Id=@categoryId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute(updateQuery, parameters);
            }
        }

        public void DeleteCategory(int id)
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
        }
    }
}
