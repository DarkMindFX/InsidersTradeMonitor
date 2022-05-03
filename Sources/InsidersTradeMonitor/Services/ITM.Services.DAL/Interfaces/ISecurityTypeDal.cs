


using ITM.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITM.Services.Dal
{
    public interface ISecurityTypeDal : IDalBase<SecurityType>
    {
        SecurityType Get(System.Int64? ID);

        bool Delete(System.Int64? ID);

    
        }
}
