using ITM.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITM.Interfaces
{
    /// <summary>
    /// Extension for IForm4ReportDal
    /// </summary>
    public interface IForm4ReportExtDal : IInitializable
    {
        /// <summary>
        /// Returns complete data about Form 4 report, including list of all transactions
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        Form4DetailedReport GetComplete(System.Int64 ID);
    }
}
