


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
    class EntityDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IEntityDal))]
    public class EntityDal: SQLDal, IEntityDal
    {
        public IInitParams CreateInitParams()
        {
            return new EntityDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public Entity Get(System.Int64? ID)
        {
            Entity result = default(Entity);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_Entity_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = EntityFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64? ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_Entity_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }


                public IList<Entity> GetByEntityTypeID(System.Int64 EntityTypeID)
        {
            var entitiesOut = base.GetBy<Entity, System.Int64>("p_Entity_GetByEntityTypeID", EntityTypeID, "@EntityTypeID", SqlDbType.BigInt, 0, EntityFromRow);

            return entitiesOut;
        }
        
        public IList<Entity> GetAll()
        {
            IList<Entity> result = base.GetAll<Entity>("p_Entity_GetAll", EntityFromRow);

            return result;
        }

        public Entity Insert(Entity entity) 
        {
            Entity entityOut = base.Upsert<Entity>("p_Entity_Insert", entity, AddUpsertParameters, EntityFromRow);

            return entityOut;
        }

        public Entity Update(Entity entity) 
        {
            Entity entityOut = base.Upsert<Entity>("p_Entity_Update", entity, AddUpsertParameters, EntityFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, Entity entity)
        {
                SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 
                SqlParameter pEntityTypeID = new SqlParameter("@EntityTypeID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "EntityTypeID", DataRowVersion.Current, (object)entity.EntityTypeID != null ? (object)entity.EntityTypeID : DBNull.Value);   cmd.Parameters.Add(pEntityTypeID); 
                SqlParameter pCIK = new SqlParameter("@CIK", System.Data.SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "CIK", DataRowVersion.Current, (object)entity.CIK != null ? (object)entity.CIK : DBNull.Value);   cmd.Parameters.Add(pCIK); 
                SqlParameter pName = new SqlParameter("@Name", System.Data.SqlDbType.NVarChar, 250, ParameterDirection.Input, false, 0, 0, "Name", DataRowVersion.Current, (object)entity.Name != null ? (object)entity.Name : DBNull.Value);   cmd.Parameters.Add(pName); 
                SqlParameter pTradingSymbol = new SqlParameter("@TradingSymbol", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "TradingSymbol", DataRowVersion.Current, (object)entity.TradingSymbol != null ? (object)entity.TradingSymbol : DBNull.Value);   cmd.Parameters.Add(pTradingSymbol); 
                SqlParameter pIsMonitored = new SqlParameter("@IsMonitored", System.Data.SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsMonitored", DataRowVersion.Current, (object)entity.IsMonitored != null ? (object)entity.IsMonitored : DBNull.Value);   cmd.Parameters.Add(pIsMonitored); 
        
            return cmd;
        }

        protected Entity EntityFromRow(DataRow row)
        {
            var entity = new Entity();

                    entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64?)row["ID"] : default(System.Int64?);
                    entity.EntityTypeID = !DBNull.Value.Equals(row["EntityTypeID"]) ? (System.Int64)row["EntityTypeID"] : default(System.Int64);
                    entity.CIK = !DBNull.Value.Equals(row["CIK"]) ? (System.Int32)row["CIK"] : default(System.Int32);
                    entity.Name = !DBNull.Value.Equals(row["Name"]) ? (System.String)row["Name"] : default(System.String);
                    entity.TradingSymbol = !DBNull.Value.Equals(row["TradingSymbol"]) ? (System.String)row["TradingSymbol"] : default(System.String);
                    entity.IsMonitored = !DBNull.Value.Equals(row["IsMonitored"]) ? (System.Boolean)row["IsMonitored"] : default(System.Boolean);
        
            return entity;
        }
        
    }
}
