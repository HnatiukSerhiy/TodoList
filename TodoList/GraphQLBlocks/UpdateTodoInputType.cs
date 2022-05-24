using GraphQL.Types;
using GraphQL;

namespace TodoList.GraphQLBlocks
{
    public class UpdateTodoInputType : InputObjectGraphType 
    {
        public UpdateTodoInputType()
        {
            Field<NonNullGraphType<IdGraphType>>("id");
            Field<NonNullGraphType<StringGraphType>>("description");
            Field<DateGraphType>("deadline");
            Field<IntGraphType>("categoryId");
        }  
    }
}
