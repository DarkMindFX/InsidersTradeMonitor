


using ITM.DAL.EF.Models;
using ITM.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace ITM.DAL.EF.Dals
{
    class Form4ReportDalInitParams : InitParamsImpl
    {
    }

    [Export("EF", typeof(IForm4ReportDal))]
    public class Form4ReportDal : IForm4ReportDal
    {
        InsidersTradeMonitorContext dbContext;

        public IInitParams CreateInitParams()
        {
            return new Form4ReportDalInitParams();
        }

        public bool Delete(System.Int64? ID)
        {
            var entity = dbContext.Form4Reports.Find(ID);
            if (entity != null)
            {
                entity.IsDeleted = true;
                dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }


        public ITM.Interfaces.Entities.Form4Report Get(System.Int64? ID)
        {
            ITM.Interfaces.Entities.Form4Report result = null;
            var entity = dbContext.Form4Reports.Where(e =>         e.ID = ID  ).FirstOrDefault();
            if (entity != null)
            {
                result = Convertors.Form4ReportConvertor.FromEFEntity(entity);
            }
            return result;
        }

        public IList<ITM.Interfaces.Entities.Form4Report> GetAll()
        {
            var entities = dbContext.Form4Reports.ToList();

            IList<ITM.Interfaces.Entities.Form4Report> result = ToList(entities);
            
            return result;
        }

                public IList<Form4Report> GetByIssuerID(System.Int64 IssuerID)
        {
            var entities = dbContext.Form4Reports.Where(e => e.IssuerID == IssuerID).ToList();

            IList<ITM.Interfaces.Entities.Form4Report> result = ToList(entities);

            return result;
        }
                public IList<Form4Report> GetByReporterID(System.Int64 ReporterID)
        {
            var entities = dbContext.Form4Reports.Where(e => e.ReporterID == ReporterID).ToList();

            IList<ITM.Interfaces.Entities.Form4Report> result = ToList(entities);

            return result;
        }
                

        public void Init(IInitParams initParams)
        {
            dbContext = new InsidersTradeMonitorContext(initParams.Parameters["ConnectionString"]);
        }

        public ITM.Interfaces.Entities.Form4Report Insert(ITM.Interfaces.Entities.Form4Report entity)
        {
            ITM.Interfaces.Entities.Form4Report result = null;
            var efEntity = Convertors.Form4ReportConvertor.ToEFEntity(entity);
            var efEntityEntry = dbContext.Add<ITM.DAL.EF.Models.Form4Report>(efEntity);
            dbContext.SaveChanges();

            result = Convertors.Form4ReportConvertor.FromEFEntity(efEntityEntry.Entity);

            return result;
        }

        public ITM.Interfaces.Entities.Form4Report Update(ITM.Interfaces.Entities.Form4Report entity)
        {
            ITM.Interfaces.Entities.Form4Report result = null;
            var efEntity = dbContext.Form4Reports.Where(e =>         e.ID == entity.ID  ).FirstOrDefault();
            if (efEntity != null)
            {
        				efEntity.IssuerID = entity.IssuerID;
						efEntity.ReporterID = entity.ReporterID;
						efEntity.ReportID = entity.ReportID;
						efEntity.IsOfficer = entity.IsOfficer;
						efEntity.IsDirector = entity.IsDirector;
						efEntity.Is10PctHolder = entity.Is10PctHolder;
						efEntity.IsOther = entity.IsOther;
						efEntity.OtherText = entity.OtherText;
						efEntity.OfficerTitle = entity.OfficerTitle;
						efEntity.Date = entity.Date;
						efEntity.DateSubmitted = entity.DateSubmitted;
		                dbContext.SaveChanges();

                efEntity = dbContext.Form4Reports.Where(e =>         e.ID == entity.ID  ).FirstOrDefault();
                result = Convertors.Form4ReportConvertor.FromEFEntity(efEntity);
            }
            return result;
        }

        #region Support methods
        IList<ITM.Interfaces.Entities.Form4Report> ToList(IList<ITM.DAL.EF.Models.Form4Report> entities)
        {
            IList<ITM.Interfaces.Entities.Form4Report> result = new List<ITM.Interfaces.Entities.Form4Report>();
            if (entities != null)
            {
                foreach (var e in entities)
                {
                    result.Add(Convertors.Form4ReportConvertor.FromEFEntity(e));
                }
            }
            return result;
        }
        
        #endregion
    }
}