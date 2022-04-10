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

        public string TransactionCode { get; set; }

        public bool EarlyVoluntarilyReport { get; set; }

        public long NumberAcquired { get; set; }

        public long NumberDisposed { get; set; }

        public DateTime DateExercisable { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string UnderlyingTitle  { get; set; }

        public long AmountShares { get; set; }

        public decimal DerivativeSecurityPrice { get; set; }

        public long AmountFollowingReport { get; set; }

        public string OwnershipType { get; set; }

        public string NatureOfIndirectOwnership { get; set; }

    }
}
