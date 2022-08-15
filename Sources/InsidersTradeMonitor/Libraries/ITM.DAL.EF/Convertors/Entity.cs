


using ITM.DAL.EF.Models;
using ITM.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace ITM.DAL.EF.Convertors
{
    public class EntityConvertor
    {

        public static ITM.Interfaces.Entities.Entity FromEFEntity(ITM.DAL.EF.Models.Entity efEntity)
        {
            ITM.Interfaces.Entities.Entity result = new Interfaces.Entities.Entity()
            {
                ID = efEntity.ID,
                EntityTypeID = efEntity.EntityTypeID,
                CIK = efEntity.CIK,
                Name = efEntity.Name,
                TradingSymbol = efEntity.TradingSymbol,
                IsMonitored = efEntity.IsMonitored,
            };

            return result;
        }

        public static ITM.DAL.EF.Models.Entity ToEFEntity(ITM.Interfaces.Entities.Entity entity)
        {
            ITM.DAL.EF.Models.Entity result = new ITM.DAL.EF.Models.Entity()
            {
                ID = entity.ID,
                EntityTypeID = entity.EntityTypeID,
                CIK = entity.CIK,
                Name = entity.Name,
                IsMonitored = entity.IsMonitored,
            };

            if (entity.TradingSymbol != null)
            {
                result.TradingSymbol = (System.String)entity.TradingSymbol;
            }

            return result;
        }
    }

}