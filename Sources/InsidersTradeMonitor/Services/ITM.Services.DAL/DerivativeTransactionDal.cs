


using ITM.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace ITM.Services.Dal
{
    [Export(typeof(IDerivativeTransactionDal))]
    public class DerivativeTransactionDal : DalBaseImpl<DerivativeTransaction, Interfaces.IDerivativeTransactionDal>, IDerivativeTransactionDal
    {

        public DerivativeTransactionDal(Interfaces.IDerivativeTransactionDal dalImpl) : base(dalImpl)
        {
        }

        public DerivativeTransaction Get(System.Int64? ID)
        {
            return _dalImpl.Get(            ID);
        }

        public bool Delete(System.Int64? ID)
        {
            return _dalImpl.Delete(            ID);
        }


        public IList<DerivativeTransaction> GetByForm4ReportID(System.Int64 Form4ReportID)
        {
            return _dalImpl.GetByForm4ReportID(Form4ReportID);
        }
        public IList<DerivativeTransaction> GetByTransactionCodeID(System.Int64 TransactionCodeID)
        {
            return _dalImpl.GetByTransactionCodeID(TransactionCodeID);
        }
        public IList<DerivativeTransaction> GetByTransactionTypeID(System.Int64? TransactionTypeID)
        {
            return _dalImpl.GetByTransactionTypeID(TransactionTypeID);
        }
        public IList<DerivativeTransaction> GetByOwnershipTypeID(System.Int64 OwnershipTypeID)
        {
            return _dalImpl.GetByOwnershipTypeID(OwnershipTypeID);
        }
            }
}
