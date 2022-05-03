using TodoList.Models;
using System.Data.SqlClient;

namespace TodoList.DataAccess
{
    public class CategoryBuilder
    {
        public CategoryModel Build(SqlDataReader reader)
        {
            CategoryModel model = new CategoryModel
            {
                Id = Convert.ToInt32(reader["Id"]),
                CategoryName = Convert.ToString(reader["CategoryName"])
            };

            return model;
        }
    }
}
