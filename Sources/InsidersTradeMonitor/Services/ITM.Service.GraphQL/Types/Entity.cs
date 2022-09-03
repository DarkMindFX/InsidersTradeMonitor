using GraphQL.Types;
using ITM.Interfaces.Entities;

namespace ITM.Service.GraphQL.Types
{
    public class Entity : ObjectGraphType<Interfaces.Entities.Entity>
    {
        public Entity()
        {
            Name = "Entity";
            Field(x => x.ID, nullable: true).Description("Record PK");
            Field(x => x.Name).Description("Full entity name: company or person's full name");
            Field(x => x.CIK).Description("Entity CIK defined by Sec.gov");
            Field(x => x.TradingSymbol).Description("For companies - ticker");
            Field(x => x.EntityTypeID).Description("Entity type ID: company or person");
            Field(x => x.IsMonitored).Description("Flag to identify whether entity is monitored");
            
        }
    }
}
