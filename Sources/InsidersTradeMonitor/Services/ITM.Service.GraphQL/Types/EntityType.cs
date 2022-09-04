using GraphQL.Types;

namespace ITM.Service.GraphQL.Types
{
    public class EntityType : ObjectGraphType<Interfaces.Entities.EntityType>
    {
        public EntityType()
        {
            Name = "EntityType";
            Field( x => x.ID, nullable: true ).Description("Record PK");
            Field(x => x.TypeName).Description("Type name - Company or Person");
        }
    }
}
