


using ITM.DAL.EF.Models;
using ITM.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace ITM.DAL.EF.Convertors
{
	public class OwnershipTypeConvertor
	{

		public static ITM.Interfaces.Entities.OwnershipType FromEFEntity(ITM.DAL.EF.Models.OwnershipType efEntity)
        {
			ITM.Interfaces.Entities.OwnershipType result = new Interfaces.Entities.OwnershipType()
			{
							ID = efEntity.ID,
							Code = efEntity.Code,
							Description = efEntity.Description,
						};

            return result;
        }

		public static ITM.DAL.EF.Models.OwnershipType ToEFEntity(ITM.Interfaces.Entities.OwnershipType entity)
		{
			ITM.DAL.EF.Models.OwnershipType result = new ITM.DAL.EF.Models.OwnershipType()
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