using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITM.Parser.Form4
{
    public class NonDerivativeTransaction
    {
        public string TitleOfSecurity { get; set; }

        public DateTime TransactionDate { get; set; }

        public DateTime? DeemedExecDate { get; set; }

        public string TransactionCode { get; set; }

        public bool EarlyVoluntarilyReport { get; set; }

        public long SharesAmount { get; set; }

        public string TransactionADType { get; set; }

        public decimal Price { get; set; }

        public long AmountFollowingReport { get; set; }

        public string OwnershipType { get; set; }

        public string NatureOfIndirectOwnership { get; set; }
    }
}
