


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
    class DerivativeTransactionDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IDerivativeTransactionDal))]
    public class DerivativeTransactionDal: SQLDal, IDerivativeTransactionDal
    {
        public IInitParams CreateInitParams()
        {
            return new DerivativeTransactionDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public DerivativeTransaction Get(System.Int64? ID)
        {
            DerivativeTransaction result = default(DerivativeTransaction);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_DerivativeTransaction_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = DerivativeTransactionFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64? ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_DerivativeTransaction_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }


                public IList<DerivativeTransaction> GetByForm4ReportID(System.Int64 Form4ReportID)
        {
            var entitiesOut = base.GetBy<DerivativeTransaction, System.Int64>("p_DerivativeTransaction_GetByForm4ReportID", Form4ReportID, "@Form4ReportID", SqlDbType.BigInt, 0, DerivativeTransactionFromRow);

            return entitiesOut;
        }
                public IList<DerivativeTransaction> GetByTransactionCodeID(System.Int64 TransactionCodeID)
        {
            var entitiesOut = base.GetBy<DerivativeTransaction, System.Int64>("p_DerivativeTransaction_GetByTransactionCodeID", TransactionCodeID, "@TransactionCodeID", SqlDbType.BigInt, 0, DerivativeTransactionFromRow);

            return entitiesOut;
        }
                public IList<DerivativeTransaction> GetByTransactionTypeID(System.Int64? TransactionTypeID)
        {
            var entitiesOut = base.GetBy<DerivativeTransaction, System.Int64?>("p_DerivativeTransaction_GetByTransactionTypeID", TransactionTypeID, "@TransactionTypeID", SqlDbType.BigInt, 0, DerivativeTransactionFromRow);

            return entitiesOut;
        }
                public IList<DerivativeTransaction> GetByOwnershipTypeID(System.Int64 OwnershipTypeID)
        {
            var entitiesOut = base.GetBy<DerivativeTransaction, System.Int64>("p_DerivativeTransaction_GetByOwnershipTypeID", OwnershipTypeID, "@OwnershipTypeID", SqlDbType.BigInt, 0, DerivativeTransactionFromRow);

            return entitiesOut;
        }
        
        public IList<DerivativeTransaction> GetAll()
        {
            IList<DerivativeTransaction> result = base.GetAll<DerivativeTransaction>("p_DerivativeTransaction_GetAll", DerivativeTransactionFromRow);

            return result;
        }

        public DerivativeTransaction Insert(DerivativeTransaction entity) 
        {
            DerivativeTransaction entityOut = base.Upsert<DerivativeTransaction>("p_DerivativeTransaction_Insert", entity, AddUpsertParameters, DerivativeTransactionFromRow);

            return entityOut;
        }

        public DerivativeTransaction Update(DerivativeTransaction entity) 
        {
            DerivativeTransaction entityOut = base.Upsert<DerivativeTransaction>("p_DerivativeTransaction_Update", entity, AddUpsertParameters, DerivativeTransactionFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, DerivativeTransaction entity)
        {
                SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 
                SqlParameter pForm4ReportID = new SqlParameter("@Form4ReportID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "Form4ReportID", DataRowVersion.Current, (object)entity.Form4ReportID != null ? (object)entity.Form4ReportID : DBNull.Value);   cmd.Parameters.Add(pForm4ReportID); 
                SqlParameter pTitleOfDerivative = new SqlParameter("@TitleOfDerivative", System.Data.SqlDbType.NVarChar, 250, ParameterDirection.Input, false, 0, 0, "TitleOfDerivative", DataRowVersion.Current, (object)entity.TitleOfDerivative != null ? (object)entity.TitleOfDerivative : DBNull.Value);   cmd.Parameters.Add(pTitleOfDerivative); 
                SqlParameter pConversionExercisePrice = new SqlParameter("@ConversionExercisePrice", System.Data.SqlDbType.Decimal, 0, ParameterDirection.Input, false, 0, 0, "ConversionExercisePrice", DataRowVersion.Current, (object)entity.ConversionExercisePrice != null ? (object)entity.ConversionExercisePrice : DBNull.Value);   cmd.Parameters.Add(pConversionExercisePrice); 
                SqlParameter pTransactionDate = new SqlParameter("@TransactionDate", System.Data.SqlDbType.Date, 0, ParameterDirection.Input, false, 0, 0, "TransactionDate", DataRowVersion.Current, (object)entity.TransactionDate != null ? (object)entity.TransactionDate : DBNull.Value);   cmd.Parameters.Add(pTransactionDate); 
                SqlParameter pTransactionCodeID = new SqlParameter("@TransactionCodeID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "TransactionCodeID", DataRowVersion.Current, (object)entity.TransactionCodeID != null ? (object)entity.TransactionCodeID : DBNull.Value);   cmd.Parameters.Add(pTransactionCodeID); 
                SqlParameter pEarlyVoluntarilyReport = new SqlParameter("@EarlyVoluntarilyReport", System.Data.SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "EarlyVoluntarilyReport", DataRowVersion.Current, (object)entity.EarlyVoluntarilyReport != null ? (object)entity.EarlyVoluntarilyReport : DBNull.Value);   cmd.Parameters.Add(pEarlyVoluntarilyReport); 
                SqlParameter pSharesAmount = new SqlParameter("@SharesAmount", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "SharesAmount", DataRowVersion.Current, (object)entity.SharesAmount != null ? (object)entity.SharesAmount : DBNull.Value);   cmd.Parameters.Add(pSharesAmount); 
                SqlParameter pDerivativeSecurityPrice = new SqlParameter("@DerivativeSecurityPrice", System.Data.SqlDbType.Decimal, 0, ParameterDirection.Input, false, 0, 0, "DerivativeSecurityPrice", DataRowVersion.Current, (object)entity.DerivativeSecurityPrice != null ? (object)entity.DerivativeSecurityPrice : DBNull.Value);   cmd.Parameters.Add(pDerivativeSecurityPrice); 
                SqlParameter pTransactionTypeID = new SqlParameter("@TransactionTypeID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "TransactionTypeID", DataRowVersion.Current, (object)entity.TransactionTypeID != null ? (object)entity.TransactionTypeID : DBNull.Value);   cmd.Parameters.Add(pTransactionTypeID); 
                SqlParameter pDateExercisable = new SqlParameter("@DateExercisable", System.Data.SqlDbType.Date, 0, ParameterDirection.Input, false, 0, 0, "DateExercisable", DataRowVersion.Current, (object)entity.DateExercisable != null ? (object)entity.DateExercisable : DBNull.Value);   cmd.Parameters.Add(pDateExercisable); 
                SqlParameter pExpirationDate = new SqlParameter("@ExpirationDate", System.Data.SqlDbType.Date, 0, ParameterDirection.Input, false, 0, 0, "ExpirationDate", DataRowVersion.Current, (object)entity.ExpirationDate != null ? (object)entity.ExpirationDate : DBNull.Value);   cmd.Parameters.Add(pExpirationDate); 
                SqlParameter pUnderlyingTitle = new SqlParameter("@UnderlyingTitle", System.Data.SqlDbType.NVarChar, 250, ParameterDirection.Input, false, 0, 0, "UnderlyingTitle", DataRowVersion.Current, (object)entity.UnderlyingTitle != null ? (object)entity.UnderlyingTitle : DBNull.Value);   cmd.Parameters.Add(pUnderlyingTitle); 
                SqlParameter pUnderlyingSharesAmount = new SqlParameter("@UnderlyingSharesAmount", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "UnderlyingSharesAmount", DataRowVersion.Current, (object)entity.UnderlyingSharesAmount != null ? (object)entity.UnderlyingSharesAmount : DBNull.Value);   cmd.Parameters.Add(pUnderlyingSharesAmount); 
                SqlParameter pAmountFollowingReport = new SqlParameter("@AmountFollowingReport", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "AmountFollowingReport", DataRowVersion.Current, (object)entity.AmountFollowingReport != null ? (object)entity.AmountFollowingReport : DBNull.Value);   cmd.Parameters.Add(pAmountFollowingReport); 
                SqlParameter pOwnershipTypeID = new SqlParameter("@OwnershipTypeID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "OwnershipTypeID", DataRowVersion.Current, (object)entity.OwnershipTypeID != null ? (object)entity.OwnershipTypeID : DBNull.Value);   cmd.Parameters.Add(pOwnershipTypeID); 
                SqlParameter pNatureOfIndirectOwnership = new SqlParameter("@NatureOfIndirectOwnership", System.Data.SqlDbType.NVarChar, 250, ParameterDirection.Input, false, 0, 0, "NatureOfIndirectOwnership", DataRowVersion.Current, (object)entity.NatureOfIndirectOwnership != null ? (object)entity.NatureOfIndirectOwnership : DBNull.Value);   cmd.Parameters.Add(pNatureOfIndirectOwnership); 
        
            return cmd;
        }

        protected DerivativeTransaction DerivativeTransactionFromRow(DataRow row)
        {
            var entity = new DerivativeTransaction();

                    entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64?)row["ID"] : default(System.Int64?);
                    entity.Form4ReportID = !DBNull.Value.Equals(row["Form4ReportID"]) ? (System.Int64)row["Form4ReportID"] : default(System.Int64);
                    entity.TitleOfDerivative = !DBNull.Value.Equals(row["TitleOfDerivative"]) ? (System.String)row["TitleOfDerivative"] : default(System.String);
                    entity.ConversionExercisePrice = !DBNull.Value.Equals(row["ConversionExercisePrice"]) ? (System.Decimal)row["ConversionExercisePrice"] : default(System.Decimal);
                    entity.TransactionDate = !DBNull.Value.Equals(row["TransactionDate"]) ? (System.DateTime)row["TransactionDate"] : default(System.DateTime);
                    entity.TransactionCodeID = !DBNull.Value.Equals(row["TransactionCodeID"]) ? (System.Int64)row["TransactionCodeID"] : default(System.Int64);
                    entity.EarlyVoluntarilyReport = !DBNull.Value.Equals(row["EarlyVoluntarilyReport"]) ? (System.Boolean)row["EarlyVoluntarilyReport"] : default(System.Boolean);
                    entity.SharesAmount = !DBNull.Value.Equals(row["SharesAmount"]) ? (System.Int64?)row["SharesAmount"] : default(System.Int64?);
                    entity.DerivativeSecurityPrice = !DBNull.Value.Equals(row["DerivativeSecurityPrice"]) ? (System.Decimal?)row["DerivativeSecurityPrice"] : default(System.Decimal?);
                    entity.TransactionTypeID = !DBNull.Value.Equals(row["TransactionTypeID"]) ? (System.Int64?)row["TransactionTypeID"] : default(System.Int64?);
                    entity.DateExercisable = !DBNull.Value.Equals(row["DateExercisable"]) ? (System.DateTime?)row["DateExercisable"] : default(System.DateTime?);
                    entity.ExpirationDate = !DBNull.Value.Equals(row["ExpirationDate"]) ? (System.DateTime?)row["ExpirationDate"] : default(System.DateTime?);
                    entity.UnderlyingTitle = !DBNull.Value.Equals(row["UnderlyingTitle"]) ? (System.String)row["UnderlyingTitle"] : default(System.String);
                    entity.UnderlyingSharesAmount = !DBNull.Value.Equals(row["UnderlyingSharesAmount"]) ? (System.Int64)row["UnderlyingSharesAmount"] : default(System.Int64);
                    entity.AmountFollowingReport = !DBNull.Value.Equals(row["AmountFollowingReport"]) ? (System.Int64)row["AmountFollowingReport"] : default(System.Int64);
                    entity.OwnershipTypeID = !DBNull.Value.Equals(row["OwnershipTypeID"]) ? (System.Int64)row["OwnershipTypeID"] : default(System.Int64);
                    entity.NatureOfIndirectOwnership = !DBNull.Value.Equals(row["NatureOfIndirectOwnership"]) ? (System.String)row["NatureOfIndirectOwnership"] : default(System.String);
        
            return entity;
        }
        
    }
}
