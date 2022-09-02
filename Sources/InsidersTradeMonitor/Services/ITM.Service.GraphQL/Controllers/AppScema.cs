using GraphQL.Types;

namespace ITM.Service.GraphQL.Controllers
{
    public class AppSchema : Schema
    {
        public AppSchema(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetRequiredService<AppQuery>();
        }
    }
}
