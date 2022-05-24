using GraphQL.Types;
using GraphQL;

namespace TodoList.GraphQLBlocks
{
    public class CreateTodoInputType : InputObjectGraphType
    {
        public CreateTodoInputType()
        {
            Field<NonNullGraphType<StringGraphType>>("description");
            Field<DateGraphType>("deadline");
            Field<IntGraphType>("categoryId");
            Field<NonNullGraphType<BooleanGraphType>>("isDone");
        }
    }
}
