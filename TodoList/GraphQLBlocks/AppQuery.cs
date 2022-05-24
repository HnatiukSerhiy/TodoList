using GraphQL.Types;
using TodoList.DataAccess;
using TodoList.interfaces;
using GraphQL;

namespace TodoList.GraphQLBlocks
{
    public class AppQuery : ObjectGraphType
    {
        private readonly ITodoDataProvider todoDataProvider;

        private readonly ICategoryDataProvider categoryDataProvider;

        public AppQuery(IDataProviderResolver dataProviderResolver)
        {
            this.todoDataProvider = dataProviderResolver.GetTodoDataProvider(SourceDataRepository.SourceName);
            this.categoryDataProvider = dataProviderResolver.GetCategoryDataProvider(SourceDataRepository.SourceName);

            Field<ListGraphType<TodoType>>(Name = "unCompleteTodo",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "categoryId" }),
                resolve: context =>
                {
                    var categoryId = context.GetArgument<int?>("categoryId");
                    return todoDataProvider.GetUnCompleteTodo(categoryId);
                });

            Field<ListGraphType<TodoType>>(Name = "completeTodo",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "categoryId" }),
                resolve: context =>
                {
                    var categoryId = context.GetArgument<int?>("categoryId");
                    return todoDataProvider.GetCompleteTodo(categoryId);
                });

            Field<TodoType>(Name = "todo",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    return todoDataProvider.GetTodoById(id);
                });

            Field<ListGraphType<CategoryType>>(Name = "categories",
                resolve: context =>
                {
                    return categoryDataProvider.GetCategoryList();
                });

            Field<CategoryType>(Name = "category",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    return categoryDataProvider.GetCategoryById(id);
                });
        }
    }
}
