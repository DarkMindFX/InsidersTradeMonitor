


using ITM.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITM.Services.Dal
{
    public interface IUserDal : IDalBase<User>
    {
        User Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

            IList<User> GetByModifiedByID(System.Int64? ModifiedByID);
    
        }
}
