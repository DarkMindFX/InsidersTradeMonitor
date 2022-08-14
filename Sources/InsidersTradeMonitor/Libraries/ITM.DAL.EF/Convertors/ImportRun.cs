


using ITM.DAL.EF.Models;
using ITM.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace ITM.DAL.EF.Convertors
{
	public class ImportRunConvertor
	{

		public static ITM.Interfaces.Entities.ImportRun FromEFEntity(ITM.DAL.EF.Models.ImportRun efEntity)
        {
			ITM.Interfaces.Entities.ImportRun result = new Interfaces.Entities.ImportRun()
			{
							ID = efEntity.ID,
							TimeStart = efEntity.TimeStart,
							TimeEnd = efEntity.TimeEnd,
							RequestJson = efEntity.RequestJson,
							StateID = efEntity.StateID,
						};

            return result;
        }

		public static ITM.DAL.EF.Models.ImportRun ToEFEntity(ITM.Interfaces.Entities.ImportRun entity)
		{
			ITM.DAL.EF.Models.ImportRun result = new ITM.DAL.EF.Models.ImportRun()
			{
							ID = entity.ID,
							TimeStart = entity.TimeStart,
							RequestJson = entity.RequestJson,
							StateID = entity.StateID,
						};

							if(entity.TimeEnd.HasValue)
				{
					result.TimeEnd = (System.DateTime) entity.TimeEnd;
				}
			
			return result;
		}
	}
	
}