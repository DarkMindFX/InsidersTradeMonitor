using GraphQL.Types;
using ITM.Interfaces.Entities;
using ITM.Services.Dal;

namespace ITM.Service.GraphQL.Types
{
    public class Entity : ObjectGraphType<Interfaces.Entities.Entity>
    {
        public Entity(IEntityTypeDal entityTypeDal)
        {
            Name = "Entity";
            Field(x => x.ID, nullable: true).Name("id").Description("Record PK");
            Field(x => x.Name).Name("name").Description("Full entity name: company or person's full name");
            Field(x => x.CIK).Name("cik").Description("Entity CIK defined by Sec.gov");
            Field(x => x.TradingSymbol, nullable: true).Name("tradingsymbol").Description("For companies - ticker");
            Field(x => x.EntityTypeID).Name("entitytypeid").Description("Entity type ID: company or person");
            Field(x => x.IsMonitored).Name("ismonitored").Description("Flag to identify whether entity is monitored");

            Field<EntityType>("entitytype",
                resolve: context =>  entityTypeDal.Get( context.Source.EntityTypeID ));
            
        }
    }
}
