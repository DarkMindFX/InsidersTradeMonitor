using GraphQL.Types;
using ITM.Interfaces.Entities;
using ITM.Services.Dal;

namespace ITM.Service.GraphQL.Types
{
    public class DerivativeTransaction : ObjectGraphType<Interfaces.Entities.DerivativeTransaction>
    {
        public DerivativeTransaction(IForm4ReportDal form4ReportDal,
            IOwnershipTypeDal ownershipTypeDal,
            ITransactionCodeDal transactionCodeDal,
            ITransactionTypeDal transactionTypeDal)
        {
            Name = "DerivativeTransaction";
            Field(x => x.ID, nullable: true).Description("Record PK");
            Field(x => x.AmountFollowingReport).Description("Amount Following Report");
            Field(x => x.ConversionExercisePrice).Description("If conversion - price at which it was excercised");
            Field(x => x.DateExercisable, nullable: true).Description("Date when transaction was excercised");
            Field(x => x.DerivativeSecurityPrice, nullable: true).Description("Price of the underlying security");
            Field(x => x.EarlyVoluntarilyReport).Description("Flag describing whether its early voluntarily reported transaction");
            Field(x => x.ExpirationDate, nullable: true).Description("Derivative expiration date");
            Field(x => x.Form4ReportID).Description("Form4 report ID");
            Field(x => x.NatureOfIndirectOwnership).Description("Nature Of Indirect Ownership");
            Field(x => x.OwnershipTypeID).Description("Ownership type ID");
            Field(x => x.SharesAmount, nullable: true).Description("NUmber of shares involved in transaction");
            Field(x => x.TitleOfDerivative).Description("Title of derivative");
            Field(x => x.TransactionCodeID).Description("Code of transaction");
            Field(x => x.TransactionDate).Description("Date of trasnaction");
            Field(x => x.TransactionTypeID, nullable: true).Description("Type of transaction Id");
            Field(x => x.UnderlyingSharesAmount).Description("Number of shares of underlying");
            Field(x => x.UnderlyingTitle).Description("Title of the underlying");

            Field<Form4Report>("form4Report",
                resolve: context => form4ReportDal.Get(context.Source.Form4ReportID));

            Field<OwnershipType>("ownershipType",
                resolve: context => ownershipTypeDal.Get(context.Source.OwnershipTypeID));

            Field<TransactionCode>("transactionCode",
                resolve: context => transactionCodeDal.Get(context.Source.TransactionCodeID));

            Field<TransactionType>("transactionType",
                resolve: context => transactionTypeDal.Get(context.Source.TransactionTypeID));

        }
    }
}
