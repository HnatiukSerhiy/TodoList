using TodoList.Models;

namespace TodoList.interfaces
{
    public interface ITodoDataProvider
    {
        IEnumerable<TodoModel> GetCompleteTodo(int? categoryId);
        IEnumerable<TodoModel> GetUnCompleteTodo(int? categoryId);
        void CreateTodo(TodoModel model);
        void SolveTodo(int id);
        TodoModel EditTodo(int id);
        void UpdateTodo(TodoModel model);
        void DeleteTodo(int id);
    }
}
