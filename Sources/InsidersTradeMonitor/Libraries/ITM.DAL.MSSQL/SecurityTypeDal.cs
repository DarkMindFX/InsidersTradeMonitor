


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
    class SecurityTypeDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(ISecurityTypeDal))]
    public class SecurityTypeDal: SQLDal, ISecurityTypeDal
    {
        public IInitParams CreateInitParams()
        {
            return new SecurityTypeDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public SecurityType Get(System.Int64? ID)
        {
            SecurityType result = default(SecurityType);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_SecurityType_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = SecurityTypeFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64? ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_SecurityType_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }


        
        public IList<SecurityType> GetAll()
        {
            IList<SecurityType> result = base.GetAll<SecurityType>("p_SecurityType_GetAll", SecurityTypeFromRow);

            return result;
        }

        public SecurityType Insert(SecurityType entity) 
        {
            SecurityType entityOut = base.Upsert<SecurityType>("p_SecurityType_Insert", entity, AddUpsertParameters, SecurityTypeFromRow);

            return entityOut;
        }

        public SecurityType Update(SecurityType entity) 
        {
            SecurityType entityOut = base.Upsert<SecurityType>("p_SecurityType_Update", entity, AddUpsertParameters, SecurityTypeFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, SecurityType entity)
        {
                SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 
                SqlParameter pSecurityTypeName = new SqlParameter("@SecurityTypeName", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "SecurityTypeName", DataRowVersion.Current, (object)entity.SecurityTypeName != null ? (object)entity.SecurityTypeName : DBNull.Value);   cmd.Parameters.Add(pSecurityTypeName); 
        
            return cmd;
        }

        protected SecurityType SecurityTypeFromRow(DataRow row)
        {
            var entity = new SecurityType();

                    entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64?)row["ID"] : default(System.Int64?);
                    entity.SecurityTypeName = !DBNull.Value.Equals(row["SecurityTypeName"]) ? (System.String)row["SecurityTypeName"] : default(System.String);
        
            return entity;
        }
        
    }
}
