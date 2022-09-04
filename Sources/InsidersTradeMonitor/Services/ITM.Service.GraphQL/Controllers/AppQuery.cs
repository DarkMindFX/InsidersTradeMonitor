﻿using GraphQL.Types;
using ITM.Services.Dal;

namespace ITM.Service.GraphQL.Controllers
{
    public class AppQuery : ObjectGraphType
    {
        public AppQuery(IEntityDal entityDal,
                        IEntityTypeDal entityTypeDal)
        {
            Field<ListGraphType<ITM.Service.GraphQL.Types.Entity>>(
                name: "entities",
                resolve: context => entityDal.GetAll()
                );

            Field<ListGraphType<ITM.Service.GraphQL.Types.EntityType>>(
                name: "entitytypes",
                resolve: context => entityTypeDal.GetAll()
                );
        }
    }
}
