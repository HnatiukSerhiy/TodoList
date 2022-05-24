using TodoList.interfaces;

namespace TodoList.interfaces
{
    public interface IDataProviderResolver
    {
        ITodoDataProvider GetTodoDataProvider(string dataProviderName);

        ICategoryDataProvider GetCategoryDataProvider(string dataProviderName);
    }
}
