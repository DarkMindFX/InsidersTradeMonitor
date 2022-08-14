


using ITM.DAL.EF.Models;
using ITM.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace ITM.DAL.EF.Convertors
{
	public class TransactionCodeConvertor
	{

		public static ITM.Interfaces.Entities.TransactionCode FromEFEntity(ITM.DAL.EF.Models.TransactionCode efEntity)
        {
			ITM.Interfaces.Entities.TransactionCode result = new Interfaces.Entities.TransactionCode()
			{
							ID = efEntity.ID,
							Code = efEntity.Code,
							Description = efEntity.Description,
						};

            return result;
        }

		public static ITM.DAL.EF.Models.TransactionCode ToEFEntity(ITM.Interfaces.Entities.TransactionCode entity)
		{
			ITM.DAL.EF.Models.TransactionCode result = new ITM.DAL.EF.Models.TransactionCode()
			{
							ID = entity.ID,
							Code = entity.Code,
						};

							if(entity.Description.HasValue)
				{
					result.Description = (System.String) entity.Description;
				}
			
			return result;
		}
	}
	
}