


using ITM.DAL.EF.Models;
using ITM.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace ITM.DAL.EF.Convertors
{
	public class ImportRunStateConvertor
	{

		public static ITM.Interfaces.Entities.ImportRunState FromEFEntity(ITM.DAL.EF.Models.ImportRunState efEntity)
        {
			ITM.Interfaces.Entities.ImportRunState result = new Interfaces.Entities.ImportRunState()
			{
							ID = efEntity.ID,
							Name = efEntity.Name,
						};

            return result;
        }

		public static ITM.DAL.EF.Models.ImportRunState ToEFEntity(ITM.Interfaces.Entities.ImportRunState entity)
		{
			ITM.DAL.EF.Models.ImportRunState result = new ITM.DAL.EF.Models.ImportRunState()
			{
							ID = entity.ID,
							Name = entity.Name,
						};

			
			return result;
		}
	}
	
}