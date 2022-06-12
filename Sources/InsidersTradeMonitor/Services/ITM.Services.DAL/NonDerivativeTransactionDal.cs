


using ITM.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace ITM.Services.Dal
{
    [Export(typeof(INonDerivativeTransactionDal))]
    public class NonDerivativeTransactionDal : DalBaseImpl<NonDerivativeTransaction, Interfaces.INonDerivativeTransactionDal>, INonDerivativeTransactionDal
    {

        public NonDerivativeTransactionDal(Interfaces.INonDerivativeTransactionDal dalImpl) : base(dalImpl)
        {
        }

        public NonDerivativeTransaction Get(System.Int64? ID)
        {
            return _dalImpl.Get(ID);
        }

        public bool Delete(System.Int64? ID)
        {
            return _dalImpl.Delete(ID);
        }


        public IList<NonDerivativeTransaction> GetByForm4ReportID(System.Int64 Form4ReportID)
        {
            return _dalImpl.GetByForm4ReportID(Form4ReportID);
        }
        public IList<NonDerivativeTransaction> GetByTransactionCodeID(System.Int64 TransactionCodeID)
        {
            return _dalImpl.GetByTransactionCodeID(TransactionCodeID);
        }
        public IList<NonDerivativeTransaction> GetByTransactionTypeID(System.Int64 TransactionTypeID)
        {
            return _dalImpl.GetByTransactionTypeID(TransactionTypeID);
        }
        public IList<NonDerivativeTransaction> GetByOwnershipTypeID(System.Int64 OwnershipTypeID)
        {
            return _dalImpl.GetByOwnershipTypeID(OwnershipTypeID);
        }
    }
}
