


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITM.Interfaces.Entities;
using ITM.Interfaces;

namespace ITM.Interfaces
{
    public interface IDerivativeTransactionDal : IDalBase<DerivativeTransaction>
    {
        DerivativeTransaction Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

        IList<DerivativeTransaction> GetByForm4ReportID(System.Int64 Form4ReportID);
        IList<DerivativeTransaction> GetByTransactionCodeID(System.Int64 TransactionCodeID);
        IList<DerivativeTransaction> GetByTransactionTypeID(System.Int64? TransactionTypeID);
        IList<DerivativeTransaction> GetByOwnershipTypeID(System.Int64 OwnershipTypeID);
        
            }
}

