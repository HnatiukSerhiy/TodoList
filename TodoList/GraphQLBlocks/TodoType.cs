using GraphQL.Types;
using TodoList.Models;
using GraphQL;

namespace TodoList.GraphQLBlocks
{
    public class TodoType : ObjectGraphType<TodoModel>
    {
        public TodoType()
        {
            Field(todo => todo.Id);
            Field(todo => todo.Description);
            Field(todo => todo.Deadline, nullable: true, type: typeof(DateGraphType));
            Field(todo => todo.DoneTime, nullable: true, type: typeof(DateGraphType));
            Field(todo => todo.CategoryId, nullable: true, type: typeof(IdGraphType));
            Field(todo => todo.CategoryName, nullable: true, type: typeof(StringGraphType));
            Field(todo => todo.IsDone);
        }
    }
}
