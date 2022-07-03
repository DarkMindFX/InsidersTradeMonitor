using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITM.Interfaces.Entities
{
    public class Form4DetailedReport
    {
        /// <summary>
        /// Form4 report details
        /// </summary>
        public Form4Report ReportDetails { get; set; }

        /// <summary>
        /// List of non-derivative transactions
        /// </summary>
        public IList<NonDerivativeTransaction> NonDerivativeTransactions { get; set; }

        /// <summary>
        /// List of derivative transactions
        /// </summary>
        public IList<DerivativeTransaction> DerivativeTransactions { get; set; }
    }
}
