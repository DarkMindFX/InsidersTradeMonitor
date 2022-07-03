


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
    class UserDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IUserDal))]
    public class UserDal : SQLDal, IUserDal
    {
        public IInitParams CreateInitParams()
        {
            return new UserDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public User Get(System.Int64? ID)
        {
            User result = default(User);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_User_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                AddParameter(cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                               ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);


                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = UserFromRow(ds.Tables[0].Rows[0]);
                }
            }

            return result;
        }

        public bool Delete(System.Int64? ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_User_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                AddParameter(cmd, "@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);

                var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }


        public IList<User> GetByModifiedByID(System.Int64? ModifiedByID)
        {
            var entitiesOut = base.GetBy<User, System.Int64?>("p_User_GetByModifiedByID", ModifiedByID, "@ModifiedByID", SqlDbType.BigInt, 0, UserFromRow);

            return entitiesOut;
        }

        public IList<User> GetAll()
        {
            IList<User> result = base.GetAll<User>("p_User_GetAll", UserFromRow);

            return result;
        }

        public User Insert(User entity)
        {
            User entityOut = base.Upsert<User>("p_User_Insert", entity, AddUpsertParameters, UserFromRow);

            return entityOut;
        }

        public User Update(User entity)
        {
            User entityOut = base.Upsert<User>("p_User_Update", entity, AddUpsertParameters, UserFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, User entity)
        {
            SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value); cmd.Parameters.Add(pID);
            SqlParameter pLogin = new SqlParameter("@Login", System.Data.SqlDbType.NVarChar, 250, ParameterDirection.Input, false, 0, 0, "Login", DataRowVersion.Current, (object)entity.Login != null ? (object)entity.Login : DBNull.Value); cmd.Parameters.Add(pLogin);
            SqlParameter pPwdHash = new SqlParameter("@PwdHash", System.Data.SqlDbType.NVarChar, 250, ParameterDirection.Input, false, 0, 0, "PwdHash", DataRowVersion.Current, (object)entity.PwdHash != null ? (object)entity.PwdHash : DBNull.Value); cmd.Parameters.Add(pPwdHash);
            SqlParameter pSalt = new SqlParameter("@Salt", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "Salt", DataRowVersion.Current, (object)entity.Salt != null ? (object)entity.Salt : DBNull.Value); cmd.Parameters.Add(pSalt);
            SqlParameter pFirstName = new SqlParameter("@FirstName", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "FirstName", DataRowVersion.Current, (object)entity.FirstName != null ? (object)entity.FirstName : DBNull.Value); cmd.Parameters.Add(pFirstName);
            SqlParameter pMiddleName = new SqlParameter("@MiddleName", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "MiddleName", DataRowVersion.Current, (object)entity.MiddleName != null ? (object)entity.MiddleName : DBNull.Value); cmd.Parameters.Add(pMiddleName);
            SqlParameter pLastName = new SqlParameter("@LastName", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "LastName", DataRowVersion.Current, (object)entity.LastName != null ? (object)entity.LastName : DBNull.Value); cmd.Parameters.Add(pLastName);
            SqlParameter pFriendlyName = new SqlParameter("@FriendlyName", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "FriendlyName", DataRowVersion.Current, (object)entity.FriendlyName != null ? (object)entity.FriendlyName : DBNull.Value); cmd.Parameters.Add(pFriendlyName);
            SqlParameter pCreatedDate = new SqlParameter("@CreatedDate", System.Data.SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "CreatedDate", DataRowVersion.Current, (object)entity.CreatedDate != null ? (object)entity.CreatedDate : DBNull.Value); cmd.Parameters.Add(pCreatedDate);
            SqlParameter pModifiedDate = new SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "ModifiedDate", DataRowVersion.Current, (object)entity.ModifiedDate != null ? (object)entity.ModifiedDate : DBNull.Value); cmd.Parameters.Add(pModifiedDate);
            SqlParameter pModifiedByID = new SqlParameter("@ModifiedByID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ModifiedByID", DataRowVersion.Current, (object)entity.ModifiedByID != null ? (object)entity.ModifiedByID : DBNull.Value); cmd.Parameters.Add(pModifiedByID);

            return cmd;
        }

        protected User UserFromRow(DataRow row)
        {
            return ITM.Utils.Convertors.UserConvertor.UserFromRow(row);
        }

    }
}
