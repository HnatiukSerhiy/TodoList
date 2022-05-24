using GraphQL;
using GraphQL.Types;

namespace TodoList.GraphQLBlocks
{
    public class UpdateCategoryInputType : InputObjectGraphType
    {
        public UpdateCategoryInputType()
        {
            Field<NonNullGraphType<IdGraphType>>("Id");
            Field<NonNullGraphType<StringGraphType>>("Name");
        }

    }
}
