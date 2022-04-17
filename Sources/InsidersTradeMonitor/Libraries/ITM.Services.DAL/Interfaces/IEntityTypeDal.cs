


using PPT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PPT.Services.Dal
{
    public interface IEntityTypeDal : IDalBase<EntityType>
    {
        EntityType Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

    
        }
}
