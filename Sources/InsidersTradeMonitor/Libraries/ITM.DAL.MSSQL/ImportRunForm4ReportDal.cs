


using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.SqlClient;
using ITM.Common;
using ITM.DAL.MSSQL;
using ITM.Interfaces;
using ITM.Interfaces.Entities;

namespace ITM.DAL.MSSQL
{
    class ImportRunForm4ReportDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IImportRunForm4ReportDal))]
    public class ImportRunForm4ReportDal : SQLDal, IImportRunForm4ReportDal
    {
        public IInitParams CreateInitParams()
        {
            return new ImportRunForm4ReportDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public ImportRunForm4Report Get(System.Int64? ID)
        {
            ImportRunForm4Report result = default(ImportRunForm4Report);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_ImportRunForm4Report_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                AddParameter(cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                               ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);


                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = ImportRunForm4ReportFromRow(ds.Tables[0].Rows[0]);
                }
            }

            return result;
        }

        public bool Delete(System.Int64? ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_ImportRunForm4Report_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                AddParameter(cmd, "@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);

                var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }


        public IList<ImportRunForm4Report> GetByImportRunID(System.Int64 ImportRunID)
        {
            var entitiesOut = base.GetBy<ImportRunForm4Report, System.Int64>("p_ImportRunForm4Report_GetByImportRunID", ImportRunID, "@ImportRunID", SqlDbType.BigInt, 0, ImportRunForm4ReportFromRow);

            return entitiesOut;
        }
        public IList<ImportRunForm4Report> GetByForm4ReportID(System.Int64 Form4ReportID)
        {
            var entitiesOut = base.GetBy<ImportRunForm4Report, System.Int64>("p_ImportRunForm4Report_GetByForm4ReportID", Form4ReportID, "@Form4ReportID", SqlDbType.BigInt, 0, ImportRunForm4ReportFromRow);

            return entitiesOut;
        }

        public IList<ImportRunForm4Report> GetAll()
        {
            IList<ImportRunForm4Report> result = base.GetAll<ImportRunForm4Report>("p_ImportRunForm4Report_GetAll", ImportRunForm4ReportFromRow);

            return result;
        }

        public ImportRunForm4Report Insert(ImportRunForm4Report entity)
        {
            ImportRunForm4Report entityOut = base.Upsert<ImportRunForm4Report>("p_ImportRunForm4Report_Insert", entity, AddUpsertParameters, ImportRunForm4ReportFromRow);

            return entityOut;
        }

        public ImportRunForm4Report Update(ImportRunForm4Report entity)
        {
            ImportRunForm4Report entityOut = base.Upsert<ImportRunForm4Report>("p_ImportRunForm4Report_Update", entity, AddUpsertParameters, ImportRunForm4ReportFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, ImportRunForm4Report entity)
        {
            SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value); cmd.Parameters.Add(pID);
            SqlParameter pImportRunID = new SqlParameter("@ImportRunID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ImportRunID", DataRowVersion.Current, (object)entity.ImportRunID != null ? (object)entity.ImportRunID : DBNull.Value); cmd.Parameters.Add(pImportRunID);
            SqlParameter pForm4ReportID = new SqlParameter("@Form4ReportID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "Form4ReportID", DataRowVersion.Current, (object)entity.Form4ReportID != null ? (object)entity.Form4ReportID : DBNull.Value); cmd.Parameters.Add(pForm4ReportID);
            SqlParameter pTimeStarted = new SqlParameter("@TimeStarted", System.Data.SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "TimeStarted", DataRowVersion.Current, (object)entity.TimeStarted != null ? (object)entity.TimeStarted : DBNull.Value); cmd.Parameters.Add(pTimeStarted);
            SqlParameter pTimeCompleted = new SqlParameter("@TimeCompleted", System.Data.SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "TimeCompleted", DataRowVersion.Current, (object)entity.TimeCompleted != null ? (object)entity.TimeCompleted : DBNull.Value); cmd.Parameters.Add(pTimeCompleted);

            return cmd;
        }

        protected ImportRunForm4Report ImportRunForm4ReportFromRow(DataRow row)
        {
            return ITM.Utils.Convertors.ImportRunForm4ReportConvertor.ImportRunForm4ReportFromRow(row);
        }

    }
}
