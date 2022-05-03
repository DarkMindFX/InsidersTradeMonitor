


using ITM.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITM.Services.Dal
{
    public interface INonDerivativeTransactionDal : IDalBase<NonDerivativeTransaction>
    {
        NonDerivativeTransaction Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

            IList<NonDerivativeTransaction> GetByForm4ReportID(System.Int64 Form4ReportID);
            IList<NonDerivativeTransaction> GetByTransactionCodeID(System.Int64 TransactionCodeID);
            IList<NonDerivativeTransaction> GetByTransactionTypeID(System.Int64 TransactionTypeID);
            IList<NonDerivativeTransaction> GetByOwnershipTypeID(System.Int64 OwnershipTypeID);
    
        }
}
