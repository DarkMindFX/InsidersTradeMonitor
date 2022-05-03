


using ITM.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace ITM.Services.Dal
{
    [Export(typeof(ITransactionTypeDal))]
    public class TransactionTypeDal : DalBaseImpl<TransactionType, Interfaces.ITransactionTypeDal>, ITransactionTypeDal
    {

        public TransactionTypeDal(Interfaces.ITransactionTypeDal dalImpl) : base(dalImpl)
        {
        }

        public TransactionType Get(System.Int64? ID)
        {
            return _dalImpl.Get(ID);
        }

        public bool Delete(System.Int64? ID)
        {
            return _dalImpl.Delete(ID);
        }


    }
}
