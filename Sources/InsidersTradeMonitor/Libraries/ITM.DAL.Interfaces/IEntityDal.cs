


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPT.Interfaces.Entities;
using PPT.Interfaces;

namespace PPT.Interfaces
{
    public interface IEntityDal : IDalBase<Entity>
    {
        Entity Get(System.Int64 ID);

        bool Delete(System.Int64 ID);

        IList<Entity> GetByEntityTypeID(System.Int64 EntityTypeID);
        
            }
}

