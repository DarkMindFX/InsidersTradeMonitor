using ITM.Interfaces;

namespace ITM.Function.ImportForm4Reports.Helpers
{
    public interface IForm4DalWrapper
    {
        /// <summary>
        /// Inserts Form 4 reports data receivied from Form 4 parser into DAL 
        /// </summary>
        /// <param name="form4Statement">Form 4 statement</param>
        /// <returns>Report Id</returns>
        long InsertReport(IStatement form4Statement);
    }
}
