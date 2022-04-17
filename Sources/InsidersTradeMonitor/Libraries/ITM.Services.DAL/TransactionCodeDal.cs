


using PPT.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace PPT.Services.Dal
{
    [Export(typeof(ITransactionCodeDal))]
    public class TransactionCodeDal : DalBaseImpl<TransactionCode, Interfaces.ITransactionCodeDal>, ITransactionCodeDal
    {

        public TransactionCodeDal(Interfaces.ITransactionCodeDal dalImpl) : base(dalImpl)
        {
        }

        public TransactionCode Get(System.Int64? ID)
        {
            return _dalImpl.Get(            ID);
        }

        public bool Delete(System.Int64? ID)
        {
            return _dalImpl.Delete(            ID);
        }


            }
}
