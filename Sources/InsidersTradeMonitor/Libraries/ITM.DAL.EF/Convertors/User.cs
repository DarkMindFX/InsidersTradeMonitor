


using ITM.DAL.EF.Models;
using ITM.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace ITM.DAL.EF.Convertors
{
    public class UserConvertor
    {

        public static ITM.Interfaces.Entities.User FromEFEntity(ITM.DAL.EF.Models.User efEntity)
        {
            ITM.Interfaces.Entities.User result = new Interfaces.Entities.User()
            {
                ID = efEntity.ID,
                Login = efEntity.Login,
                PwdHash = efEntity.PwdHash,
                Salt = efEntity.Salt,
                FirstName = efEntity.FirstName,
                MiddleName = efEntity.MiddleName,
                LastName = efEntity.LastName,
                FriendlyName = efEntity.FriendlyName,
                CreatedDate = efEntity.CreatedDate,
                ModifiedDate = efEntity.ModifiedDate,
                ModifiedByID = efEntity.ModifiedByID,
            };

            return result;
        }

        public static ITM.DAL.EF.Models.User ToEFEntity(ITM.Interfaces.Entities.User entity)
        {
            ITM.DAL.EF.Models.User result = new ITM.DAL.EF.Models.User()
            {
                ID = entity.ID,
                Login = entity.Login,
                PwdHash = entity.PwdHash,
                Salt = entity.Salt,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                CreatedDate = entity.CreatedDate,
            };

            if (entity.MiddleName != null)
            {
                result.MiddleName = (System.String)entity.MiddleName;
            }
            if (entity.FriendlyName != null)
            {
                result.FriendlyName = (System.String)entity.FriendlyName;
            }
            if (entity.ModifiedDate.HasValue)
            {
                result.ModifiedDate = (System.DateTime)entity.ModifiedDate;
            }
            if (entity.ModifiedByID.HasValue)
            {
                result.ModifiedByID = (System.Int64)entity.ModifiedByID;
            }

            return result;
        }
    }

}