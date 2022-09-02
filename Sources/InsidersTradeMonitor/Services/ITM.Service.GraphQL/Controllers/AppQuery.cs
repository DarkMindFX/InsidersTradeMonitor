using GraphQL.Types;
using ITM.Services.Dal;

namespace ITM.Service.GraphQL.Controllers
{
    public class AppQuery : ObjectGraphType
    {
        public AppQuery(IEntityDal entityDal)
        {
            Field<ListGraphType<ITM.Service.GraphQL.Types.Entity>>(
                "entities",
                resolve: context => entityDal.GetAll()
                );
        }
    }
}
