using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITM.Parser.Form4
{
    public class DerivativeTransaction
    {
        public string TitleOfDerivative { get; set; }

        public decimal ConversionExcercisePrice { get; set; }

        public DateTime TransactionDate { get; set; }

        public DateTime? DeemedExecDate { get; set; }

        public bool EquitySwapsInvolved { get; set; }

        public string TransactionCode { get; set; }

        public bool EarlyVoluntarilyReport { get; set; }

        
        public DateTime? DateExercisable { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public long SharesAmount { get; set; }

        public decimal DerivativeSecurityPrice { get; set; }

        public string TransactionADType { get; set; }
        
        public string UnderlyingTitle { get; set; }

        public long UnderlyingSharesAmount { get; set; }

        public long AmountFollowingReport { get; set; }

        public string OwnershipType { get; set; }

        public string NatureOfIndirectOwnership { get; set; }

    }
}
