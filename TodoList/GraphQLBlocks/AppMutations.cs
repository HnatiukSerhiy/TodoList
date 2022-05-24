using GraphQL;
using GraphQL.Types;
using TodoList.DataAccess;
using TodoList.GraphQLBlocks;
using TodoList.interfaces;
using TodoList.Models;

namespace TodoList.GraphQLBlocks
{
    public class AppMutations : ObjectGraphType
    {
        private readonly ITodoDataProvider todoDataProvider;

        private readonly ICategoryDataProvider categoryDataProvider;

        public AppMutations(IDataProviderResolver dataProviderResolver)
        {
            this.todoDataProvider = dataProviderResolver.GetTodoDataProvider(SourceDataRepository.SourceName);
            this.categoryDataProvider = dataProviderResolver.GetCategoryDataProvider(SourceDataRepository.SourceName);

            Field<TodoType>("createTodo",
                arguments: new QueryArguments { new QueryArgument<CreateTodoInputType> { Name = "todo" } },
                resolve: context => {
                    var todo = context.GetArgument<TodoModel>("todo");
                    return todoDataProvider.CreateTodo(todo);
                });

            Field<TodoType>("updateTodo",
                arguments: new QueryArguments { new QueryArgument<UpdateTodoInputType> { Name = "todo" } },
                resolve: context => {
                    var todo = context.GetArgument<TodoModel>("todo");
                    return todoDataProvider.UpdateTodo(todo);
                });

            Field<IdGraphType>("solveTodo",
                arguments: new QueryArguments { new QueryArgument<IdGraphType> { Name = "id" } },
                resolve: context => { 
                    var id = context.GetArgument<int>("id");
                    return todoDataProvider.SolveTodo(id);
                });

            Field<IdGraphType>("deleteTodo",
                arguments: new QueryArguments { new QueryArgument<IdGraphType> { Name = "id" } },
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    return todoDataProvider.DeleteTodo(id);
                });

            Field<StringGraphType>("changeDataProvider",
                arguments: new QueryArguments { new QueryArgument<StringGraphType> { Name = "dataSource" } },
                resolve: context =>
                {
                    var dataSource = context.GetArgument<string>("dataSource");
                    SourceDataRepository.SourceName = dataSource;
                    return dataSource;
                });

            Field<CategoryType>("createCategory",
                arguments: new QueryArguments { new QueryArgument<CreateCategoryInputType> { Name = "category" } },
                resolve: context =>
                {
                    var category = context.GetArgument<CategoryModel>("category");
                    return categoryDataProvider.CreateCategory(category);
                });

            Field<CategoryType>("updateCategory",
                arguments: new QueryArguments { new QueryArgument<UpdateCategoryInputType> { Name = "category" } },
                resolve: context =>
                {
                    var category = context.GetArgument<CategoryModel>("category");
                    return categoryDataProvider.UpdateCategory(category);
                });

            Field<IdGraphType>("deleteCategory",
                arguments: new QueryArguments { new QueryArgument<IdGraphType> { Name = "id" } },
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    return categoryDataProvider.DeleteCategory(id);
                });
        }
    }
}
