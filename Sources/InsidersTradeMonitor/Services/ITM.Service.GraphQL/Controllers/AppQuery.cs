using GraphQL.Types;
using ITM.Services.Dal;

namespace ITM.Service.GraphQL.Controllers
{
    public class AppQuery : ObjectGraphType
    {
        public AppQuery(IEntityDal entityDal,
                        IEntityTypeDal entityTypeDal,
                        IDerivativeTransactionDal derivativeTransactionDal,
                        IForm4ReportDal form4ReportDal,
                        IOwnershipTypeDal ownershipTypeDal,
                        ITransactionCodeDal transactionCodeDal,
                        ITransactionTypeDal transactionTypeDal)
        {
            Field<ListGraphType<ITM.Service.GraphQL.Types.Entity>>(
                name: "entities",
                resolve: context => entityDal.GetAll()
                );

            Field<ListGraphType<ITM.Service.GraphQL.Types.EntityType>>(
                name: "entitytypes",
                resolve: context => entityTypeDal.GetAll()
                );

            Field<ListGraphType<ITM.Service.GraphQL.Types.DerivativeTransaction>>(
                name: "derivativetransactions",
                resolve: context => derivativeTransactionDal.GetAll()
                );

            Field<ListGraphType<ITM.Service.GraphQL.Types.Form4Report>>(
                name: "form4reports",
                resolve: context => form4ReportDal.GetAll()
                );

            Field<ListGraphType<ITM.Service.GraphQL.Types.OwnershipType>>(
                name: "ownershipTypes",
                resolve: context => ownershipTypeDal.GetAll()
                );

            Field<ListGraphType<ITM.Service.GraphQL.Types.TransactionCode>>(
                name: "transactionCode",
                resolve: context => transactionCodeDal.GetAll()
                );

            Field<ListGraphType<ITM.Service.GraphQL.Types.TransactionType>>(
                name: "transactionType",
                resolve: context => transactionTypeDal.GetAll()
                );
        }
    }
}
