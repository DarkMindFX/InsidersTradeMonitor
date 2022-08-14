


using ITM.DAL.EF.Models;
using ITM.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace ITM.DAL.EF.Convertors
{
	public class EntityTypeConvertor
	{

		public static ITM.Interfaces.Entities.EntityType FromEFEntity(ITM.DAL.EF.Models.EntityType efEntity)
        {
			ITM.Interfaces.Entities.EntityType result = new Interfaces.Entities.EntityType()
			{
							ID = efEntity.ID,
							TypeName = efEntity.TypeName,
						};

            return result;
        }

		public static ITM.DAL.EF.Models.EntityType ToEFEntity(ITM.Interfaces.Entities.EntityType entity)
		{
			ITM.DAL.EF.Models.EntityType result = new ITM.DAL.EF.Models.EntityType()
			{
							ID = entity.ID,
							TypeName = entity.TypeName,
						};

			
			return result;
		}
	}
	
}