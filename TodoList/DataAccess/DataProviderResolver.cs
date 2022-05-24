using TodoList.interfaces;

namespace TodoList.DataAccess
{
    public class DataProviderResolver : IDataProviderResolver
    {
        private readonly IServiceProvider serviceProvider;

        public DataProviderResolver(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public ITodoDataProvider GetTodoDataProvider(string dataProviderName)
        {
            if (dataProviderName == "xml")
            {
                return serviceProvider.GetRequiredService<TodoXmlDataProvider>();
            }

            return serviceProvider.GetRequiredService<TodoSqlDataProvider>();
        }

        public ICategoryDataProvider GetCategoryDataProvider(string dataProviderName)
        {
            if (dataProviderName == "xml")
            {
                return serviceProvider.GetRequiredService<CategoryXmlDataProvider>();
            }

            return serviceProvider.GetRequiredService<CategorySqlDataProvider>();
        }
    }
}
