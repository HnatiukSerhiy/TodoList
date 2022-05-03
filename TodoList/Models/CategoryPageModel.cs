namespace TodoList.Models
{
    public class CategoryPageModel
    {
        public CategoryModel CategoryForm { get; set; } = new CategoryModel();
        public IEnumerable<CategoryModel>? CategoryList { get; set; }
    }
}
