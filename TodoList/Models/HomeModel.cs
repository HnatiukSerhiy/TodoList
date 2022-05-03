using System.ComponentModel.DataAnnotations;
namespace TodoList.Models
{
    public class HomeModel
    {
        public IEnumerable<TodoModel>? CompleteTodoList{ get; set; } = new List<TodoModel>();
        public IEnumerable<TodoModel>? UnCompleteTodoList { get; set; } = new List<TodoModel>();
        public List<CategoryModel>? CategoryList { get; set; } = new List<CategoryModel>();
    }
}
