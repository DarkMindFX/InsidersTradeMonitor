


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
    class NonDerivativeTransactionDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(INonDerivativeTransactionDal))]
    public class NonDerivativeTransactionDal: SQLDal, INonDerivativeTransactionDal
    {
        public IInitParams CreateInitParams()
        {
            return new NonDerivativeTransactionDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public NonDerivativeTransaction Get(System.Int64? ID)
        {
            NonDerivativeTransaction result = default(NonDerivativeTransaction);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_NonDerivativeTransaction_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = NonDerivativeTransactionFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64? ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_NonDerivativeTransaction_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }


                public IList<NonDerivativeTransaction> GetByForm4ReportID(System.Int64 Form4ReportID)
        {
            var entitiesOut = base.GetBy<NonDerivativeTransaction, System.Int64>("p_NonDerivativeTransaction_GetByForm4ReportID", Form4ReportID, "@Form4ReportID", SqlDbType.BigInt, 0, NonDerivativeTransactionFromRow);

            return entitiesOut;
        }
                public IList<NonDerivativeTransaction> GetByTransactionCodeID(System.Int64 TransactionCodeID)
        {
            var entitiesOut = base.GetBy<NonDerivativeTransaction, System.Int64>("p_NonDerivativeTransaction_GetByTransactionCodeID", TransactionCodeID, "@TransactionCodeID", SqlDbType.BigInt, 0, NonDerivativeTransactionFromRow);

            return entitiesOut;
        }
                public IList<NonDerivativeTransaction> GetByTransactionTypeID(System.Int64 TransactionTypeID)
        {
            var entitiesOut = base.GetBy<NonDerivativeTransaction, System.Int64>("p_NonDerivativeTransaction_GetByTransactionTypeID", TransactionTypeID, "@TransactionTypeID", SqlDbType.BigInt, 0, NonDerivativeTransactionFromRow);

            return entitiesOut;
        }
                public IList<NonDerivativeTransaction> GetByOwnershipTypeID(System.Int64 OwnershipTypeID)
        {
            var entitiesOut = base.GetBy<NonDerivativeTransaction, System.Int64>("p_NonDerivativeTransaction_GetByOwnershipTypeID", OwnershipTypeID, "@OwnershipTypeID", SqlDbType.BigInt, 0, NonDerivativeTransactionFromRow);

            return entitiesOut;
        }
        
        public IList<NonDerivativeTransaction> GetAll()
        {
            IList<NonDerivativeTransaction> result = base.GetAll<NonDerivativeTransaction>("p_NonDerivativeTransaction_GetAll", NonDerivativeTransactionFromRow);

            return result;
        }

        public NonDerivativeTransaction Insert(NonDerivativeTransaction entity) 
        {
            NonDerivativeTransaction entityOut = base.Upsert<NonDerivativeTransaction>("p_NonDerivativeTransaction_Insert", entity, AddUpsertParameters, NonDerivativeTransactionFromRow);

            return entityOut;
        }

        public NonDerivativeTransaction Update(NonDerivativeTransaction entity) 
        {
            NonDerivativeTransaction entityOut = base.Upsert<NonDerivativeTransaction>("p_NonDerivativeTransaction_Update", entity, AddUpsertParameters, NonDerivativeTransactionFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, NonDerivativeTransaction entity)
        {
                SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 
                SqlParameter pForm4ReportID = new SqlParameter("@Form4ReportID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "Form4ReportID", DataRowVersion.Current, (object)entity.Form4ReportID != null ? (object)entity.Form4ReportID : DBNull.Value);   cmd.Parameters.Add(pForm4ReportID); 
                SqlParameter pTitleOfSecurity = new SqlParameter("@TitleOfSecurity", System.Data.SqlDbType.NVarChar, 250, ParameterDirection.Input, false, 0, 0, "TitleOfSecurity", DataRowVersion.Current, (object)entity.TitleOfSecurity != null ? (object)entity.TitleOfSecurity : DBNull.Value);   cmd.Parameters.Add(pTitleOfSecurity); 
                SqlParameter pTransactionDate = new SqlParameter("@TransactionDate", System.Data.SqlDbType.Date, 0, ParameterDirection.Input, false, 0, 0, "TransactionDate", DataRowVersion.Current, (object)entity.TransactionDate != null ? (object)entity.TransactionDate : DBNull.Value);   cmd.Parameters.Add(pTransactionDate); 
                SqlParameter pDeemedExecDate = new SqlParameter("@DeemedExecDate", System.Data.SqlDbType.Date, 0, ParameterDirection.Input, false, 0, 0, "DeemedExecDate", DataRowVersion.Current, (object)entity.DeemedExecDate != null ? (object)entity.DeemedExecDate : DBNull.Value);   cmd.Parameters.Add(pDeemedExecDate); 
                SqlParameter pTransactionCodeID = new SqlParameter("@TransactionCodeID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "TransactionCodeID", DataRowVersion.Current, (object)entity.TransactionCodeID != null ? (object)entity.TransactionCodeID : DBNull.Value);   cmd.Parameters.Add(pTransactionCodeID); 
                SqlParameter pEarlyVoluntarilyReport = new SqlParameter("@EarlyVoluntarilyReport", System.Data.SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "EarlyVoluntarilyReport", DataRowVersion.Current, (object)entity.EarlyVoluntarilyReport != null ? (object)entity.EarlyVoluntarilyReport : DBNull.Value);   cmd.Parameters.Add(pEarlyVoluntarilyReport); 
                SqlParameter pSharesAmount = new SqlParameter("@SharesAmount", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "SharesAmount", DataRowVersion.Current, (object)entity.SharesAmount != null ? (object)entity.SharesAmount : DBNull.Value);   cmd.Parameters.Add(pSharesAmount); 
                SqlParameter pTransactionTypeID = new SqlParameter("@TransactionTypeID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "TransactionTypeID", DataRowVersion.Current, (object)entity.TransactionTypeID != null ? (object)entity.TransactionTypeID : DBNull.Value);   cmd.Parameters.Add(pTransactionTypeID); 
                SqlParameter pPrice = new SqlParameter("@Price", System.Data.SqlDbType.Decimal, 0, ParameterDirection.Input, false, 0, 0, "Price", DataRowVersion.Current, (object)entity.Price != null ? (object)entity.Price : DBNull.Value);   cmd.Parameters.Add(pPrice); 
                SqlParameter pAmountFollowingReport = new SqlParameter("@AmountFollowingReport", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "AmountFollowingReport", DataRowVersion.Current, (object)entity.AmountFollowingReport != null ? (object)entity.AmountFollowingReport : DBNull.Value);   cmd.Parameters.Add(pAmountFollowingReport); 
                SqlParameter pOwnershipTypeID = new SqlParameter("@OwnershipTypeID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "OwnershipTypeID", DataRowVersion.Current, (object)entity.OwnershipTypeID != null ? (object)entity.OwnershipTypeID : DBNull.Value);   cmd.Parameters.Add(pOwnershipTypeID); 
                SqlParameter pNatureOfIndirectOwnership = new SqlParameter("@NatureOfIndirectOwnership", System.Data.SqlDbType.NVarChar, 250, ParameterDirection.Input, false, 0, 0, "NatureOfIndirectOwnership", DataRowVersion.Current, (object)entity.NatureOfIndirectOwnership != null ? (object)entity.NatureOfIndirectOwnership : DBNull.Value);   cmd.Parameters.Add(pNatureOfIndirectOwnership); 
        
            return cmd;
        }

        protected NonDerivativeTransaction NonDerivativeTransactionFromRow(DataRow row)
        {
            var entity = new NonDerivativeTransaction();

                    entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64?)row["ID"] : default(System.Int64?);
                    entity.Form4ReportID = !DBNull.Value.Equals(row["Form4ReportID"]) ? (System.Int64)row["Form4ReportID"] : default(System.Int64);
                    entity.TitleOfSecurity = !DBNull.Value.Equals(row["TitleOfSecurity"]) ? (System.String)row["TitleOfSecurity"] : default(System.String);
                    entity.TransactionDate = !DBNull.Value.Equals(row["TransactionDate"]) ? (System.DateTime)row["TransactionDate"] : default(System.DateTime);
                    entity.DeemedExecDate = !DBNull.Value.Equals(row["DeemedExecDate"]) ? (System.DateTime?)row["DeemedExecDate"] : default(System.DateTime?);
                    entity.TransactionCodeID = !DBNull.Value.Equals(row["TransactionCodeID"]) ? (System.Int64)row["TransactionCodeID"] : default(System.Int64);
                    entity.EarlyVoluntarilyReport = !DBNull.Value.Equals(row["EarlyVoluntarilyReport"]) ? (System.Boolean)row["EarlyVoluntarilyReport"] : default(System.Boolean);
                    entity.SharesAmount = !DBNull.Value.Equals(row["SharesAmount"]) ? (System.Int64?)row["SharesAmount"] : default(System.Int64?);
                    entity.TransactionTypeID = !DBNull.Value.Equals(row["TransactionTypeID"]) ? (System.Int64)row["TransactionTypeID"] : default(System.Int64);
                    entity.Price = !DBNull.Value.Equals(row["Price"]) ? (System.Decimal)row["Price"] : default(System.Decimal);
                    entity.AmountFollowingReport = !DBNull.Value.Equals(row["AmountFollowingReport"]) ? (System.Int64)row["AmountFollowingReport"] : default(System.Int64);
                    entity.OwnershipTypeID = !DBNull.Value.Equals(row["OwnershipTypeID"]) ? (System.Int64)row["OwnershipTypeID"] : default(System.Int64);
                    entity.NatureOfIndirectOwnership = !DBNull.Value.Equals(row["NatureOfIndirectOwnership"]) ? (System.String)row["NatureOfIndirectOwnership"] : default(System.String);
        
            return entity;
        }
        
    }
}
