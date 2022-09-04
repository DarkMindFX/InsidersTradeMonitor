using GraphQL.Types;
using ITM.Interfaces.Entities;
using ITM.Services.Dal;

namespace ITM.Service.GraphQL.Types
{
    public class OwnershipType : ObjectGraphType<Interfaces.Entities.OwnershipType>
    {
        public OwnershipType()
        {
            Name = "OwnershipType";
            Field(x => x.ID, nullable: true).Description("Record PK");
            Field(x => x.Code).Description("Ownership type code as defined by source (i.e. SEC.gov)");
            Field(x => x.Description).Description("Ownership type description");

        }
    }
}
