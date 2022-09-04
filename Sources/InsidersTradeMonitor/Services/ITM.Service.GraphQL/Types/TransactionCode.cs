using GraphQL.Types;
using ITM.Interfaces.Entities;
using ITM.Services.Dal;

namespace ITM.Service.GraphQL.Types
{
    public class TransactionCode : ObjectGraphType<Interfaces.Entities.TransactionCode>
    {
        public TransactionCode()
        {
            Name = "TransactionCode";
            Field(x => x.ID, nullable: true).Description("Record PK");
            Field(x => x.Code).Description("Transaction code as defined by source (i.e. SEC.gov)");
            Field(x => x.Description).Description("Ownership type description");

        }
    }
}
