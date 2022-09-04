using GraphQL.Types;
using ITM.Interfaces.Entities;

namespace ITM.Service.GraphQL.Types
{
    public class DerivativeTransaction : ObjectGraphType<Interfaces.Entities.DerivativeTransaction>
    {
        public DerivativeTransaction()
        {
            Name = "DerivativeTransaction";
            Field(x => x.ID, nullable: true).Description("Record PK");
            Field(x => x.AmountFollowingReport).Description("Amount Following Report");
            Field(x => x.ConversionExercisePrice).Description("If conversion - price at which it was excercised");
            
        }
    }
}
