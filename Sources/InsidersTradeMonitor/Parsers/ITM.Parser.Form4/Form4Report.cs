using ITM.Interfaces;

namespace ITM.Parser.Form4
{
    public class Form4Report : IStatement
    {
        private const string ReportTypeName = "4";

        #region interface IStatement
        public string ReportType => ReportTypeName;

        #endregion

        public string IssuerName { get; set; }

        public string IssuerSymbol { get; set; }

        public string IssuerCIK { get; set; }

        public DateTime PeriodOfReport { get; set; }

        public string DocumentType { get; set; }

        public string OwnerName { get; set; }

        public string OwnerCIK { get; set; }

        public string OwnerStreet1 { get; set; }

        public string OwnerCity { get; set; }

        public string OwnerState { get; set; }

        public string OwnerZipCode { get; set; }

        public string OwnerStateDescription { get; set; }

        public bool IsDirector { get; set; }

        public bool IsOfficer { get; set; }

        public bool IsTenPercentHolder { get; set; }

        public bool IsOther { get; set; }

        public string OwnerOtherText { get; set; }

        public string OfficerTitle { get; set; }

        public IList<DerivativeTransaction> DerivativeTransactions { get; set; }

        public IList<NonDerivativeTransaction> NonDerivativeTransactions { get; set; }
    }
}