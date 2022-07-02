


using ITM.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITM.Services.Dal
{
    public interface IEntityDal : IDalBase<Entity>
    {
        Entity Get(System.Int64 ID);

        bool Delete(System.Int64 ID);

        IList<Entity> GetByEntityTypeID(System.Int64 EntityTypeID);

        IList<Entity> GetMonitoredList();

    }
}
