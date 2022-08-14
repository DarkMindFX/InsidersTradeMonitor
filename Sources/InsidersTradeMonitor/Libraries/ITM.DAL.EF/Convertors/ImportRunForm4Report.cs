


using ITM.DAL.EF.Models;
using ITM.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace ITM.DAL.EF.Convertors
{
	public class ImportRunForm4ReportConvertor
	{

		public static ITM.Interfaces.Entities.ImportRunForm4Report FromEFEntity(ITM.DAL.EF.Models.ImportRunForm4Report efEntity)
        {
			ITM.Interfaces.Entities.ImportRunForm4Report result = new Interfaces.Entities.ImportRunForm4Report()
			{
							ID = efEntity.ID,
							ImportRunID = efEntity.ImportRunID,
							Form4ReportID = efEntity.Form4ReportID,
							TimeStarted = efEntity.TimeStarted,
							TimeCompleted = efEntity.TimeCompleted,
						};

            return result;
        }

		public static ITM.DAL.EF.Models.ImportRunForm4Report ToEFEntity(ITM.Interfaces.Entities.ImportRunForm4Report entity)
		{
			ITM.DAL.EF.Models.ImportRunForm4Report result = new ITM.DAL.EF.Models.ImportRunForm4Report()
			{
							ID = entity.ID,
							ImportRunID = entity.ImportRunID,
							Form4ReportID = entity.Form4ReportID,
							TimeStarted = entity.TimeStarted,
						};

							if(entity.TimeCompleted.HasValue)
				{
					result.TimeCompleted = (System.DateTime) entity.TimeCompleted;
				}
			
			return result;
		}
	}
	
}