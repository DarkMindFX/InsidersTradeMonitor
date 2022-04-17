


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPT.Interfaces.Entities;
using PPT.Interfaces;

namespace PPT.Interfaces
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

