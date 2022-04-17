


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
    class EntityTypeDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IEntityTypeDal))]
    public class EntityTypeDal: SQLDal, IEntityTypeDal
    {
        public IInitParams CreateInitParams()
        {
            return new EntityTypeDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public EntityType Get(System.Int64? ID)
        {
            EntityType result = default(EntityType);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_EntityType_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = EntityTypeFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64? ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_EntityType_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }


        
        public IList<EntityType> GetAll()
        {
            IList<EntityType> result = base.GetAll<EntityType>("p_EntityType_GetAll", EntityTypeFromRow);

            return result;
        }

        public EntityType Insert(EntityType entity) 
        {
            EntityType entityOut = base.Upsert<EntityType>("p_EntityType_Insert", entity, AddUpsertParameters, EntityTypeFromRow);

            return entityOut;
        }

        public EntityType Update(EntityType entity) 
        {
            EntityType entityOut = base.Upsert<EntityType>("p_EntityType_Update", entity, AddUpsertParameters, EntityTypeFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, EntityType entity)
        {
                SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 
                SqlParameter pTypeName = new SqlParameter("@TypeName", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "TypeName", DataRowVersion.Current, (object)entity.TypeName != null ? (object)entity.TypeName : DBNull.Value);   cmd.Parameters.Add(pTypeName); 
        
            return cmd;
        }

        protected EntityType EntityTypeFromRow(DataRow row)
        {
            var entity = new EntityType();

                    entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64?)row["ID"] : default(System.Int64?);
                    entity.TypeName = !DBNull.Value.Equals(row["TypeName"]) ? (System.String)row["TypeName"] : default(System.String);
        
            return entity;
        }
        
    }
}
