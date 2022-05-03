


using ITM.Interfaces.Entities;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace ITM.Services.Dal
{
    [Export(typeof(ISecurityTypeDal))]
    public class SecurityTypeDal : DalBaseImpl<SecurityType, Interfaces.ISecurityTypeDal>, ISecurityTypeDal
    {

        public SecurityTypeDal(Interfaces.ISecurityTypeDal dalImpl) : base(dalImpl)
        {
        }

        public SecurityType Get(System.Int64? ID)
        {
            return _dalImpl.Get(            ID);
        }

        public bool Delete(System.Int64? ID)
        {
            return _dalImpl.Delete(            ID);
        }


            }
}
