


using ITM.DAL.EF.Models;
using ITM.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace ITM.DAL.EF.Dals
{
    class ImportRunForm4ReportDalInitParams : InitParamsImpl
    {
    }

    [Export("EF", typeof(IImportRunForm4ReportDal))]
    public class ImportRunForm4ReportDal : IImportRunForm4ReportDal
    {
        InsidersTradeMonitorContext dbContext;

        public IInitParams CreateInitParams()
        {
            return new ImportRunForm4ReportDalInitParams();
        }

        public bool Delete(System.Int64? ID)
        {
            var entity = dbContext.ImportRunForm4Reports.Find(ID);
            if (entity != null)
            {
							dbContext.Remove(entity);
			                dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }


        public ITM.Interfaces.Entities.ImportRunForm4Report Get(System.Int64? ID)
        {
            ITM.Interfaces.Entities.ImportRunForm4Report result = null;
            var entity = dbContext.ImportRunForm4Reports.Where(e =>         e.ID == ID  ).FirstOrDefault();
            if (entity != null)
            {
                result = Convertors.ImportRunForm4ReportConvertor.FromEFEntity(entity);
            }
            return result;
        }

        public IList<ITM.Interfaces.Entities.ImportRunForm4Report> GetAll()
        {
            var entities = dbContext.ImportRunForm4Reports.ToList();

            IList<ITM.Interfaces.Entities.ImportRunForm4Report> result = ToList(entities);
            
            return result;
        }

                public IList<ITM.Interfaces.Entities.ImportRunForm4Report> GetByImportRunID(System.Int64 ImportRunID)
        {
            var entities = dbContext.ImportRunForm4Reports.Where(e => e.ImportRunID == ImportRunID).ToList();

            IList<ITM.Interfaces.Entities.ImportRunForm4Report> result = ToList(entities);

            return result;
        }
                public IList<ITM.Interfaces.Entities.ImportRunForm4Report> GetByForm4ReportID(System.Int64 Form4ReportID)
        {
            var entities = dbContext.ImportRunForm4Reports.Where(e => e.Form4ReportID == Form4ReportID).ToList();

            IList<ITM.Interfaces.Entities.ImportRunForm4Report> result = ToList(entities);

            return result;
        }
                

        public void Init(IInitParams initParams)
        {
            dbContext = new InsidersTradeMonitorContext(initParams.Parameters["ConnectionString"]);
        }

        public ITM.Interfaces.Entities.ImportRunForm4Report Insert(ITM.Interfaces.Entities.ImportRunForm4Report entity)
        {
            ITM.Interfaces.Entities.ImportRunForm4Report result = null;
            var efEntity = Convertors.ImportRunForm4ReportConvertor.ToEFEntity(entity);
            var efEntityEntry = dbContext.Add<ITM.DAL.EF.Models.ImportRunForm4Report>(efEntity);
            dbContext.SaveChanges();

            result = Convertors.ImportRunForm4ReportConvertor.FromEFEntity(efEntityEntry.Entity);

            return result;
        }

        public ITM.Interfaces.Entities.ImportRunForm4Report Update(ITM.Interfaces.Entities.ImportRunForm4Report entity)
        {
            ITM.Interfaces.Entities.ImportRunForm4Report result = null;
            var efEntity = dbContext.ImportRunForm4Reports.Where(e =>         e.ID == entity.ID  ).FirstOrDefault();
            if (efEntity != null)
            {
        				efEntity.ImportRunID = entity.ImportRunID;
						efEntity.Form4ReportID = entity.Form4ReportID;
						efEntity.TimeStarted = entity.TimeStarted;
						efEntity.TimeCompleted = entity.TimeCompleted;
		                dbContext.SaveChanges();

                efEntity = dbContext.ImportRunForm4Reports.Where(e =>         e.ID == entity.ID  ).FirstOrDefault();
                result = Convertors.ImportRunForm4ReportConvertor.FromEFEntity(efEntity);
            }
            return result;
        }

        #region Support methods
        IList<ITM.Interfaces.Entities.ImportRunForm4Report> ToList(IList<ITM.DAL.EF.Models.ImportRunForm4Report> entities)
        {
            IList<ITM.Interfaces.Entities.ImportRunForm4Report> result = new List<ITM.Interfaces.Entities.ImportRunForm4Report>();
            if (entities != null)
            {
                foreach (var e in entities)
                {
                    result.Add(Convertors.ImportRunForm4ReportConvertor.FromEFEntity(e));
                }
            }
            return result;
        }
        
        #endregion
    }
}