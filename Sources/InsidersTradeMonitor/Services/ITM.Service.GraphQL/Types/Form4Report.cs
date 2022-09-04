using GraphQL.Types;
using ITM.Interfaces.Entities;
using ITM.Services.Dal;

namespace ITM.Service.GraphQL.Types
{
    public class Form4Report : ObjectGraphType<Interfaces.Entities.Form4Report>
    {
        public Form4Report(IEntityDal entityDal)
        {
            Name = "Form4Report";
            Field(x => x.ID, nullable: true).Description("Record PK");
            Field(x => x.Date).Description("Form 4 report date");
            Field(x => x.DateSubmitted).Description("Date when report was submitted");
            Field(x => x.Is10PctHolder).Description("Flag whether the reporter is 10% holder");
            Field(x => x.IsDirector).Description("Flag whether reporter is Director");
            Field(x => x.IsOfficer).Description("Flag whether reporter is company's Officer");
            Field(x => x.IsOther).Description("Flag whether reporter occupies any other significant position in the company");
            Field(x => x.IssuerID).Description("PK of the copmany issuing report");
            Field(x => x.OfficerTitle).Description("IF officer - title");
            Field(x => x.OtherText).Description("If other position - description of the position");
            Field(x => x.ReporterID).Description("PK of person who reports");
            Field(x => x.ReportID).Description("Report identifier how it was obtained from the source");

            Field<Entity>("issuer",
                resolve: context => entityDal.Get(context.Source.IssuerID));

            Field<Entity>("reporter",
                resolve: context => entityDal.Get(context.Source.ReporterID));

        }
    }
}
