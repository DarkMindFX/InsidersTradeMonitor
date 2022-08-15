


using ITM.DAL.EF.Models;
using ITM.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace ITM.DAL.EF.Dals
{
    class UserDalInitParams : InitParamsImpl
    {
    }

    [Export("EF", typeof(IUserDal))]
    public class UserDal : IUserDal
    {
        InsidersTradeMonitorContext dbContext;

        public IInitParams CreateInitParams()
        {
            return new UserDalInitParams();
        }

        public bool Delete(System.Int64? ID)
        {
            var entity = dbContext.Users.Find(ID);
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


        public ITM.Interfaces.Entities.User Get(System.Int64? ID)
        {
            ITM.Interfaces.Entities.User result = null;
            var entity = dbContext.Users.Where(e =>         e.ID == ID  ).FirstOrDefault();
            if (entity != null)
            {
                result = Convertors.UserConvertor.FromEFEntity(entity);
            }
            return result;
        }

        public IList<ITM.Interfaces.Entities.User> GetAll()
        {
            var entities = dbContext.Users.ToList();

            IList<ITM.Interfaces.Entities.User> result = ToList(entities);
            
            return result;
        }

                public IList<ITM.Interfaces.Entities.User> GetByModifiedByID(System.Int64? ModifiedByID)
        {
            var entities = dbContext.Users.Where(e => e.ModifiedByID == ModifiedByID).ToList();

            IList<ITM.Interfaces.Entities.User> result = ToList(entities);

            return result;
        }
                

        public void Init(IInitParams initParams)
        {
            dbContext = new InsidersTradeMonitorContext(initParams.Parameters["ConnectionString"]);
        }

        public ITM.Interfaces.Entities.User Insert(ITM.Interfaces.Entities.User entity)
        {
            ITM.Interfaces.Entities.User result = null;
            var efEntity = Convertors.UserConvertor.ToEFEntity(entity);
            var efEntityEntry = dbContext.Add<ITM.DAL.EF.Models.User>(efEntity);
            dbContext.SaveChanges();

            result = Convertors.UserConvertor.FromEFEntity(efEntityEntry.Entity);

            return result;
        }

        public ITM.Interfaces.Entities.User Update(ITM.Interfaces.Entities.User entity)
        {
            ITM.Interfaces.Entities.User result = null;
            var efEntity = dbContext.Users.Where(e =>         e.ID == entity.ID  ).FirstOrDefault();
            if (efEntity != null)
            {
        				efEntity.Login = entity.Login;
						efEntity.PwdHash = entity.PwdHash;
						efEntity.Salt = entity.Salt;
						efEntity.FirstName = entity.FirstName;
						efEntity.MiddleName = entity.MiddleName;
						efEntity.LastName = entity.LastName;
						efEntity.FriendlyName = entity.FriendlyName;
						efEntity.CreatedDate = entity.CreatedDate;
						efEntity.ModifiedDate = entity.ModifiedDate;
						efEntity.ModifiedByID = entity.ModifiedByID;
		                dbContext.SaveChanges();

                efEntity = dbContext.Users.Where(e =>         e.ID == entity.ID  ).FirstOrDefault();
                result = Convertors.UserConvertor.FromEFEntity(efEntity);
            }
            return result;
        }

        #region Support methods
        IList<ITM.Interfaces.Entities.User> ToList(IList<ITM.DAL.EF.Models.User> entities)
        {
            IList<ITM.Interfaces.Entities.User> result = new List<ITM.Interfaces.Entities.User>();
            if (entities != null)
            {
                foreach (var e in entities)
                {
                    result.Add(Convertors.UserConvertor.FromEFEntity(e));
                }
            }
            return result;
        }
        
        #endregion
    }
}