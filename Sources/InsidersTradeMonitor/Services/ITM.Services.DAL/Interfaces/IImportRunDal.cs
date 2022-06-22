


using ITM.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITM.Services.Dal
{
    public interface IImportRunDal : IDalBase<ImportRun>
    {
        ImportRun Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

            IList<ImportRun> GetByStateID(System.Int64 StateID);
    
        }
}
