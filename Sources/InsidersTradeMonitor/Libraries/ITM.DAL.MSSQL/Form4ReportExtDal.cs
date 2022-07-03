using ITM.Common;
using ITM.Interfaces;
using ITM.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITM.DAL.MSSQL
{
    class Form4ReportExtDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IForm4ReportExtDal))]
    public class Form4ReportExtDal : SQLDal, IForm4ReportExtDal
    {

        public IInitParams CreateInitParams()
        {
            return new Form4ReportExtDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public Form4DetailedReport GetComplete(long ID)
        {
            Form4DetailedReport result = default(Form4DetailedReport);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_Form4Report_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                AddParameter(cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                               ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);


                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if ((bool)pFound.Value)
                {
                    if (ds.Tables.Count > 0)
                    {
                        result = Form4DetailedReportFromDataset(ds);
                    }
                }
            }

            return result;
        }

        protected Form4DetailedReport Form4DetailedReportFromDataset(DataSet ds)
        {
            var result = new Form4DetailedReport();

            const int idxForm4Details = 0;
            const int idxNonDerivTransactions = 1;
            const int idxDerivTransactions = 2;

            // Report details
            var rowForm4Report = ds.Tables[idxForm4Details].Rows[0];
            result.ReportDetails = ITM.Utils.Convertors.Form4ReportConvertor.Form4ReportFromRow(rowForm4Report);

            // Non-deriv transactions
            result.NonDerivativeTransactions = new List<NonDerivativeTransaction>();
            foreach (var ndt in ds.Tables[idxNonDerivTransactions].Rows)
            {
                result.NonDerivativeTransactions.Add(ITM.Utils.Convertors.NonDerivativeTransactionConvertor.NonDerivativeTransactionFromRow((DataRow)ndt));
            }

            // Deriv transactions
            result.DerivativeTransactions = new List<DerivativeTransaction>();
            foreach (var dt in ds.Tables[idxDerivTransactions].Rows)
            {
                result.DerivativeTransactions.Add(ITM.Utils.Convertors.DerivativeTransactionConvertor.DerivativeTransactionFromRow((DataRow)dt));
            }

            return result;
        }
    }
}
