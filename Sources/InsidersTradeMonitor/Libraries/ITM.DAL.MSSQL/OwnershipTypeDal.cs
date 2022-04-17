


using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.SqlClient;
using PPT.Common;
using PPT.DAL.MSSQL;
using PPT.Interfaces;
using PPT.Interfaces.Entities;

namespace PPT.DAL.MSSQL 
{
    class OwnershipTypeDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IOwnershipTypeDal))]
    public class OwnershipTypeDal: SQLDal, IOwnershipTypeDal
    {
        public IInitParams CreateInitParams()
        {
            return new OwnershipTypeDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public OwnershipType Get(System.Int64? ID)
        {
            OwnershipType result = default(OwnershipType);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_OwnershipType_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = OwnershipTypeFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64? ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_OwnershipType_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }


        
        public IList<OwnershipType> GetAll()
        {
            IList<OwnershipType> result = base.GetAll<OwnershipType>("p_OwnershipType_GetAll", OwnershipTypeFromRow);

            return result;
        }

        public OwnershipType Insert(OwnershipType entity) 
        {
            OwnershipType entityOut = base.Upsert<OwnershipType>("p_OwnershipType_Insert", entity, AddUpsertParameters, OwnershipTypeFromRow);

            return entityOut;
        }

        public OwnershipType Update(OwnershipType entity) 
        {
            OwnershipType entityOut = base.Upsert<OwnershipType>("p_OwnershipType_Update", entity, AddUpsertParameters, OwnershipTypeFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, OwnershipType entity)
        {
                SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 
                SqlParameter pCode = new SqlParameter("@Code", System.Data.SqlDbType.NChar, 1, ParameterDirection.Input, false, 0, 0, "Code", DataRowVersion.Current, (object)entity.Code != null ? (object)entity.Code : DBNull.Value);   cmd.Parameters.Add(pCode); 
                SqlParameter pDescription = new SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Current, (object)entity.Description != null ? (object)entity.Description : DBNull.Value);   cmd.Parameters.Add(pDescription); 
        
            return cmd;
        }

        protected OwnershipType OwnershipTypeFromRow(DataRow row)
        {
            var entity = new OwnershipType();

                    entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64?)row["ID"] : default(System.Int64?);
                    entity.Code = !DBNull.Value.Equals(row["Code"]) ? (System.String)row["Code"] : default(System.String);
                    entity.Description = !DBNull.Value.Equals(row["Description"]) ? (System.String)row["Description"] : default(System.String);
        
            return entity;
        }
        
    }
}
