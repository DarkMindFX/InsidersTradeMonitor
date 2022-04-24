


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITM.Interfaces.Entities;
using ITM.Interfaces;

namespace ITM.Interfaces
{
    public interface IOwnershipTypeDal : IDalBase<OwnershipType>
    {
        OwnershipType Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

        
            }
}

