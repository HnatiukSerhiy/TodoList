using System.ComponentModel.DataAnnotations;
namespace TodoList.Models
{
    public class HomeModel
    {
        public TodoModel TodoFrom { get; set; } = new TodoModel();
        public IEnumerable<TodoModel>? CompleteTodoList{ get; set; } = new List<TodoModel>();
        public IEnumerable<TodoModel>? UnCompleteTodoList { get; set; } = new List<TodoModel>();
        public IEnumerable<CategoryModel>? CategoryList { get; set; } = new List<CategoryModel>();

    }
}
