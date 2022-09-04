using GraphQL.Types;
using ITM.Interfaces.Entities;
using ITM.Services.Dal;

namespace ITM.Service.GraphQL.Types
{
    public class TransactionType : ObjectGraphType<Interfaces.Entities.TransactionType>
    {
        public TransactionType()
        {
            Name = "TransactionType";
            Field(x => x.ID, nullable: true).Description("Record PK");
            Field(x => x.Code).Description("Transaction type code as defined by source (i.e. SEC.gov)");
            Field(x => x.Description).Description("Ownership type description");

        }
    }
}
