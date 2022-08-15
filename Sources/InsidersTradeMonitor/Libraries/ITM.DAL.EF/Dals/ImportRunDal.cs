


using ITM.DAL.EF.Models;
using ITM.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace ITM.DAL.EF.Dals
{
    class ImportRunDalInitParams : InitParamsImpl
    {
    }

    [Export("EF", typeof(IImportRunDal))]
    public class ImportRunDal : IImportRunDal
    {
        InsidersTradeMonitorContext dbContext;

        public IInitParams CreateInitParams()
        {
            return new ImportRunDalInitParams();
        }

        public bool Delete(System.Int64? ID)
        {
            var entity = dbContext.ImportRuns.Find(ID);
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


        public ITM.Interfaces.Entities.ImportRun Get(System.Int64? ID)
        {
            ITM.Interfaces.Entities.ImportRun result = null;
            var entity = dbContext.ImportRuns.Where(e =>         e.ID == ID  ).FirstOrDefault();
            if (entity != null)
            {
                result = Convertors.ImportRunConvertor.FromEFEntity(entity);
            }
            return result;
        }

        public IList<ITM.Interfaces.Entities.ImportRun> GetAll()
        {
            var entities = dbContext.ImportRuns.ToList();

            IList<ITM.Interfaces.Entities.ImportRun> result = ToList(entities);
            
            return result;
        }

                public IList<ITM.Interfaces.Entities.ImportRun> GetByStateID(System.Int64 StateID)
        {
            var entities = dbContext.ImportRuns.Where(e => e.StateID == StateID).ToList();

            IList<ITM.Interfaces.Entities.ImportRun> result = ToList(entities);

            return result;
        }
                

        public void Init(IInitParams initParams)
        {
            dbContext = new InsidersTradeMonitorContext(initParams.Parameters["ConnectionString"]);
        }

        public ITM.Interfaces.Entities.ImportRun Insert(ITM.Interfaces.Entities.ImportRun entity)
        {
            ITM.Interfaces.Entities.ImportRun result = null;
            var efEntity = Convertors.ImportRunConvertor.ToEFEntity(entity);
            var efEntityEntry = dbContext.Add<ITM.DAL.EF.Models.ImportRun>(efEntity);
            dbContext.SaveChanges();

            result = Convertors.ImportRunConvertor.FromEFEntity(efEntityEntry.Entity);

            return result;
        }

        public ITM.Interfaces.Entities.ImportRun Update(ITM.Interfaces.Entities.ImportRun entity)
        {
            ITM.Interfaces.Entities.ImportRun result = null;
            var efEntity = dbContext.ImportRuns.Where(e =>         e.ID == entity.ID  ).FirstOrDefault();
            if (efEntity != null)
            {
        				efEntity.TimeStart = entity.TimeStart;
						efEntity.TimeEnd = entity.TimeEnd;
						efEntity.RequestJson = entity.RequestJson;
						efEntity.StateID = entity.StateID;
		                dbContext.SaveChanges();

                efEntity = dbContext.ImportRuns.Where(e =>         e.ID == entity.ID  ).FirstOrDefault();
                result = Convertors.ImportRunConvertor.FromEFEntity(efEntity);
            }
            return result;
        }

        #region Support methods
        IList<ITM.Interfaces.Entities.ImportRun> ToList(IList<ITM.DAL.EF.Models.ImportRun> entities)
        {
            IList<ITM.Interfaces.Entities.ImportRun> result = new List<ITM.Interfaces.Entities.ImportRun>();
            if (entities != null)
            {
                foreach (var e in entities)
                {
                    result.Add(Convertors.ImportRunConvertor.FromEFEntity(e));
                }
            }
            return result;
        }
        
        #endregion
    }
}