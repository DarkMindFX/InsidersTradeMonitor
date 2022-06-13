

using ITM.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITM.API.Dal
{
    public interface IUserDal : IDalBase<User>
    {
        User Get(System.Int64? ID);

    }
}
