

using ITM.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace ITM.Services.Dal
{
    [Export(typeof(IUserDal))]
    public class UserDal : DalBaseImpl<User, Interfaces.IUserDal>, IUserDal
    {

        public UserDal(Interfaces.IUserDal dalImpl) : base(dalImpl)
        {
        }

        public User Get(System.Int64? ID)
        {
            return _dalImpl.Get(            ID);
        }

        public bool Delete(System.Int64? ID)
        {
            return _dalImpl.Delete(            ID);
        }


        public IList<User> GetByModifiedByID(System.Int64? ModifiedByID)
        {
            return _dalImpl.GetByModifiedByID(ModifiedByID);
        }
            }
}
