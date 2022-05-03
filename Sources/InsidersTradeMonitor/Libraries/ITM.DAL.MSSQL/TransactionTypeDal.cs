


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
    class TransactionTypeDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(ITransactionTypeDal))]
    public class TransactionTypeDal: SQLDal, ITransactionTypeDal
    {
        public IInitParams CreateInitParams()
        {
            return new TransactionTypeDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public TransactionType Get(System.Int64? ID)
        {
            TransactionType result = default(TransactionType);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_TransactionType_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = TransactionTypeFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64? ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_TransactionType_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }


        
        public IList<TransactionType> GetAll()
        {
            IList<TransactionType> result = base.GetAll<TransactionType>("p_TransactionType_GetAll", TransactionTypeFromRow);

            return result;
        }

        public TransactionType Insert(TransactionType entity) 
        {
            TransactionType entityOut = base.Upsert<TransactionType>("p_TransactionType_Insert", entity, AddUpsertParameters, TransactionTypeFromRow);

            return entityOut;
        }

        public TransactionType Update(TransactionType entity) 
        {
            TransactionType entityOut = base.Upsert<TransactionType>("p_TransactionType_Update", entity, AddUpsertParameters, TransactionTypeFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, TransactionType entity)
        {
                SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 
                SqlParameter pCode = new SqlParameter("@Code", System.Data.SqlDbType.NVarChar, 10, ParameterDirection.Input, false, 0, 0, "Code", DataRowVersion.Current, (object)entity.Code != null ? (object)entity.Code : DBNull.Value);   cmd.Parameters.Add(pCode); 
                SqlParameter pDescription = new SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 250, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Current, (object)entity.Description != null ? (object)entity.Description : DBNull.Value);   cmd.Parameters.Add(pDescription); 
        
            return cmd;
        }

        protected TransactionType TransactionTypeFromRow(DataRow row)
        {
            var entity = new TransactionType();

                    entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64?)row["ID"] : default(System.Int64?);
                    entity.Code = !DBNull.Value.Equals(row["Code"]) ? (System.String)row["Code"] : default(System.String);
                    entity.Description = !DBNull.Value.Equals(row["Description"]) ? (System.String)row["Description"] : default(System.String);
        
            return entity;
        }
        
    }
}
