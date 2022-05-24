using GraphQL.Types;
using TodoList.Models;

namespace TodoList.GraphQLBlocks
{
    public class CategoryType : ObjectGraphType<CategoryModel>
    {
        public CategoryType()
        {
            Field(category => category.Id);
            Field(category => category.Name);
        }
    }
}
