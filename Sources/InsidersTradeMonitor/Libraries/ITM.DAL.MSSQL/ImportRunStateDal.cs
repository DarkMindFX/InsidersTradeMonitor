


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
    class ImportRunStateDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IImportRunStateDal))]
    public class ImportRunStateDal : SQLDal, IImportRunStateDal
    {
        public IInitParams CreateInitParams()
        {
            return new ImportRunStateDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public ImportRunState Get(System.Int64? ID)
        {
            ImportRunState result = default(ImportRunState);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_ImportRunState_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                AddParameter(cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                               ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);


                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = ImportRunStateFromRow(ds.Tables[0].Rows[0]);
                }
            }

            return result;
        }

        public bool Delete(System.Int64? ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_ImportRunState_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                AddParameter(cmd, "@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);

                var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }



        public IList<ImportRunState> GetAll()
        {
            IList<ImportRunState> result = base.GetAll<ImportRunState>("p_ImportRunState_GetAll", ImportRunStateFromRow);

            return result;
        }

        public ImportRunState Insert(ImportRunState entity)
        {
            ImportRunState entityOut = base.Upsert<ImportRunState>("p_ImportRunState_Insert", entity, AddUpsertParameters, ImportRunStateFromRow);

            return entityOut;
        }

        public ImportRunState Update(ImportRunState entity)
        {
            ImportRunState entityOut = base.Upsert<ImportRunState>("p_ImportRunState_Update", entity, AddUpsertParameters, ImportRunStateFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, ImportRunState entity)
        {
            SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value); cmd.Parameters.Add(pID);
            SqlParameter pName = new SqlParameter("@Name", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "Name", DataRowVersion.Current, (object)entity.Name != null ? (object)entity.Name : DBNull.Value); cmd.Parameters.Add(pName);

            return cmd;
        }

        protected ImportRunState ImportRunStateFromRow(DataRow row)
        {
            return ITM.Utils.Convertors.ImportRunStateConvertor.ImportRunStateFromRow(row);
        }

    }
}
