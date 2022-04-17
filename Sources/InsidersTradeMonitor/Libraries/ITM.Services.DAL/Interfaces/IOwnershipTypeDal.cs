


using PPT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PPT.Services.Dal
{
    public interface IOwnershipTypeDal : IDalBase<OwnershipType>
    {
        OwnershipType Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

    
        }
}
