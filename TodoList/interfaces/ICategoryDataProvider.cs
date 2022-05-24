using TodoList.Models;

namespace TodoList.interfaces
{
    public interface ICategoryDataProvider
    {
        IEnumerable<CategoryModel> GetCategoryList();
        CategoryModel CreateCategory(CategoryModel model);
        CategoryModel GetCategoryById(int id);
        CategoryModel UpdateCategory(CategoryModel model);
        int DeleteCategory(int id);
    }
}
