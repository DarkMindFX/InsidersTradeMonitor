


using ITM.DAL.EF.Models;
using ITM.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace ITM.DAL.EF.Convertors
{
    public class TransactionTypeConvertor
    {

        public static ITM.Interfaces.Entities.TransactionType FromEFEntity(ITM.DAL.EF.Models.TransactionType efEntity)
        {
            ITM.Interfaces.Entities.TransactionType result = new Interfaces.Entities.TransactionType()
            {
                ID = efEntity.ID,
                Code = efEntity.Code,
                Description = efEntity.Description,
            };

            return result;
        }

        public static ITM.DAL.EF.Models.TransactionType ToEFEntity(ITM.Interfaces.Entities.TransactionType entity)
        {
            ITM.DAL.EF.Models.TransactionType result = new ITM.DAL.EF.Models.TransactionType()
            {
                ID = entity.ID,
                Code = entity.Code,
            };

            if (entity.Description != null)
            {
                result.Description = (System.String)entity.Description;
            }

            return result;
        }
    }

}