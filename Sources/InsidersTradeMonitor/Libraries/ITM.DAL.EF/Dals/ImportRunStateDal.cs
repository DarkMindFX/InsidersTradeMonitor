


using ITM.DAL.EF.Models;
using ITM.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace ITM.DAL.EF.Dals
{
    class ImportRunStateDalInitParams : InitParamsImpl
    {
    }

    [Export("EF", typeof(IImportRunStateDal))]
    public class ImportRunStateDal : IImportRunStateDal
    {
        InsidersTradeMonitorContext dbContext;

        public IInitParams CreateInitParams()
        {
            return new ImportRunStateDalInitParams();
        }

        public bool Delete(System.Int64? ID)
        {
            var entity = dbContext.ImportRunStates.Find(ID);
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


        public ITM.Interfaces.Entities.ImportRunState Get(System.Int64? ID)
        {
            ITM.Interfaces.Entities.ImportRunState result = null;
            var entity = dbContext.ImportRunStates.Where(e =>         e.ID == ID  ).FirstOrDefault();
            if (entity != null)
            {
                result = Convertors.ImportRunStateConvertor.FromEFEntity(entity);
            }
            return result;
        }

        public IList<ITM.Interfaces.Entities.ImportRunState> GetAll()
        {
            var entities = dbContext.ImportRunStates.ToList();

            IList<ITM.Interfaces.Entities.ImportRunState> result = ToList(entities);
            
            return result;
        }

                

        public void Init(IInitParams initParams)
        {
            dbContext = new InsidersTradeMonitorContext(initParams.Parameters["ConnectionString"]);
        }

        public ITM.Interfaces.Entities.ImportRunState Insert(ITM.Interfaces.Entities.ImportRunState entity)
        {
            ITM.Interfaces.Entities.ImportRunState result = null;
            var efEntity = Convertors.ImportRunStateConvertor.ToEFEntity(entity);
            var efEntityEntry = dbContext.Add<ITM.DAL.EF.Models.ImportRunState>(efEntity);
            dbContext.SaveChanges();

            result = Convertors.ImportRunStateConvertor.FromEFEntity(efEntityEntry.Entity);

            return result;
        }

        public ITM.Interfaces.Entities.ImportRunState Update(ITM.Interfaces.Entities.ImportRunState entity)
        {
            ITM.Interfaces.Entities.ImportRunState result = null;
            var efEntity = dbContext.ImportRunStates.Where(e =>         e.ID == entity.ID  ).FirstOrDefault();
            if (efEntity != null)
            {
        				efEntity.Name = entity.Name;
		                dbContext.SaveChanges();

                efEntity = dbContext.ImportRunStates.Where(e =>         e.ID == entity.ID  ).FirstOrDefault();
                result = Convertors.ImportRunStateConvertor.FromEFEntity(efEntity);
            }
            return result;
        }

        #region Support methods
        IList<ITM.Interfaces.Entities.ImportRunState> ToList(IList<ITM.DAL.EF.Models.ImportRunState> entities)
        {
            IList<ITM.Interfaces.Entities.ImportRunState> result = new List<ITM.Interfaces.Entities.ImportRunState>();
            if (entities != null)
            {
                foreach (var e in entities)
                {
                    result.Add(Convertors.ImportRunStateConvertor.FromEFEntity(e));
                }
            }
            return result;
        }
        
        #endregion
    }
}