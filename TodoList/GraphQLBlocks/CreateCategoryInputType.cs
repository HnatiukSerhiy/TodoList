using GraphQL;
using GraphQL.Types;

namespace TodoList.GraphQLBlocks
{
    public class CreateCategoryInputType : InputObjectGraphType
    {
        public CreateCategoryInputType()
        {
            Field<NonNullGraphType<StringGraphType>>("Name");
        }
    }
}
