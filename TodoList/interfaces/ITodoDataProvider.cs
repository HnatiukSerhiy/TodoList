using TodoList.Models;

namespace TodoList.interfaces
{
    public interface ITodoDataProvider
    {
        IEnumerable<TodoModel> GetCompleteTodo(int? categoryId);
        IEnumerable<TodoModel> GetUnCompleteTodo(int? categoryId);
        TodoModel CreateTodo(TodoModel model);
        int SolveTodo(int id);
        TodoModel GetTodoById(int id);
        TodoModel UpdateTodo(TodoModel model);
        int DeleteTodo(int id);
    }
}
