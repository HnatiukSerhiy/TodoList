using TodoList.Models;

namespace TodoList.interfaces
{
    public interface ICategoryDataProvider
    {
        List<CategoryModel> GetCategoryList();
        void CreateCategory(CategoryModel model);
        CategoryModel EditCategory(int id);
        void UpdateCategory(CategoryModel model);
        void DeleteCategory(int id);
    }
}
