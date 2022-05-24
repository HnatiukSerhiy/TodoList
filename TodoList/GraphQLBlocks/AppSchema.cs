using GraphQL.Types;
using GraphQL;
using TodoList.interfaces;
using TodoList.DataAccess;

namespace TodoList.GraphQLBlocks
{
    public class AppSchema : Schema
    {
        public AppSchema(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Query = serviceProvider.GetRequiredService<AppQuery>();
            Mutation = serviceProvider.GetRequiredService<AppMutations>();
        }
    }
}
