


using ITM.DAL.EF.Models;
using ITM.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace ITM.DAL.EF.Convertors
{
	public class Form4ReportConvertor
	{

		public static ITM.Interfaces.Entities.Form4Report FromEFEntity(ITM.DAL.EF.Models.Form4Report efEntity)
        {
			ITM.Interfaces.Entities.Form4Report result = new Interfaces.Entities.Form4Report()
			{
							ID = efEntity.ID,
							IssuerID = efEntity.IssuerID,
							ReporterID = efEntity.ReporterID,
							ReportID = efEntity.ReportID,
							IsOfficer = efEntity.IsOfficer,
							IsDirector = efEntity.IsDirector,
							Is10PctHolder = efEntity.Is10PctHolder,
							IsOther = efEntity.IsOther,
							OtherText = efEntity.OtherText,
							OfficerTitle = efEntity.OfficerTitle,
							Date = efEntity.Date,
							DateSubmitted = efEntity.DateSubmitted,
						};

            return result;
        }

		public static ITM.DAL.EF.Models.Form4Report ToEFEntity(ITM.Interfaces.Entities.Form4Report entity)
		{
			ITM.DAL.EF.Models.Form4Report result = new ITM.DAL.EF.Models.Form4Report()
			{
							ID = entity.ID,
							IssuerID = entity.IssuerID,
							ReporterID = entity.ReporterID,
							ReportID = entity.ReportID,
							IsOfficer = entity.IsOfficer,
							IsDirector = entity.IsDirector,
							Is10PctHolder = entity.Is10PctHolder,
							IsOther = entity.IsOther,
							Date = entity.Date,
							DateSubmitted = entity.DateSubmitted,
						};

							if(entity.OtherText.HasValue)
				{
					result.OtherText = (System.String) entity.OtherText;
				}
							if(entity.OfficerTitle.HasValue)
				{
					result.OfficerTitle = (System.String) entity.OfficerTitle;
				}
			
			return result;
		}
	}
	
}