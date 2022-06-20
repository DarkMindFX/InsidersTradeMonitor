


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
    class ImportRunDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IImportRunDal))]
    public class ImportRunDal: SQLDal, IImportRunDal
    {
        public IInitParams CreateInitParams()
        {
            return new ImportRunDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public ImportRun Get(System.Int64? ID)
        {
            ImportRun result = default(ImportRun);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_ImportRun_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = ImportRunFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64? ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_ImportRun_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }


                public IList<ImportRun> GetByStateID(System.Int64 StateID)
        {
            var entitiesOut = base.GetBy<ImportRun, System.Int64>("p_ImportRun_GetByStateID", StateID, "@StateID", SqlDbType.BigInt, 0, ImportRunFromRow);

            return entitiesOut;
        }
        
        public IList<ImportRun> GetAll()
        {
            IList<ImportRun> result = base.GetAll<ImportRun>("p_ImportRun_GetAll", ImportRunFromRow);

            return result;
        }

        public ImportRun Insert(ImportRun entity) 
        {
            ImportRun entityOut = base.Upsert<ImportRun>("p_ImportRun_Insert", entity, AddUpsertParameters, ImportRunFromRow);

            return entityOut;
        }

        public ImportRun Update(ImportRun entity) 
        {
            ImportRun entityOut = base.Upsert<ImportRun>("p_ImportRun_Update", entity, AddUpsertParameters, ImportRunFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, ImportRun entity)
        {
                SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 
                SqlParameter pTimeStart = new SqlParameter("@TimeStart", System.Data.SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "TimeStart", DataRowVersion.Current, (object)entity.TimeStart != null ? (object)entity.TimeStart : DBNull.Value);   cmd.Parameters.Add(pTimeStart); 
                SqlParameter pTimeEnd = new SqlParameter("@TimeEnd", System.Data.SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "TimeEnd", DataRowVersion.Current, (object)entity.TimeEnd != null ? (object)entity.TimeEnd : DBNull.Value);   cmd.Parameters.Add(pTimeEnd); 
                SqlParameter pRequestJson = new SqlParameter("@RequestJson", System.Data.SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "RequestJson", DataRowVersion.Current, (object)entity.RequestJson != null ? (object)entity.RequestJson : DBNull.Value);   cmd.Parameters.Add(pRequestJson); 
                SqlParameter pStateID = new SqlParameter("@StateID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "StateID", DataRowVersion.Current, (object)entity.StateID != null ? (object)entity.StateID : DBNull.Value);   cmd.Parameters.Add(pStateID); 
        
            return cmd;
        }

        protected ImportRun ImportRunFromRow(DataRow row)
        {
            var entity = new ImportRun();

                    entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64?)row["ID"] : default(System.Int64?);
                    entity.TimeStart = !DBNull.Value.Equals(row["TimeStart"]) ? (System.DateTime)row["TimeStart"] : default(System.DateTime);
                    entity.TimeEnd = !DBNull.Value.Equals(row["TimeEnd"]) ? (System.DateTime?)row["TimeEnd"] : default(System.DateTime?);
                    entity.RequestJson = !DBNull.Value.Equals(row["RequestJson"]) ? (System.String)row["RequestJson"] : default(System.String);
                    entity.StateID = !DBNull.Value.Equals(row["StateID"]) ? (System.Int64)row["StateID"] : default(System.Int64);
        
            return entity;
        }
        
    }
}
