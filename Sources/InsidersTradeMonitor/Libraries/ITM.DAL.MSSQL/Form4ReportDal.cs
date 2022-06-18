


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
    class Form4ReportDalInitParams : InitParamsImpl
    {
    }

    [Export("MSSQL", typeof(IForm4ReportDal))]
    public class Form4ReportDal: SQLDal, IForm4ReportDal
    {
        public IInitParams CreateInitParams()
        {
            return new Form4ReportDalInitParams();
        }

        public void Init(IInitParams initParams)
        {
            InitDbConnection(initParams.Parameters["ConnectionString"]);
        }

        public Form4Report Get(System.Int64? ID)
        {
            Form4Report result = default(Form4Report);

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_Form4Report_GetDetails", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0,
                                ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
            
                var pFound = AddParameter(cmd, "@Found", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    result = Form4ReportFromRow(ds.Tables[0].Rows[0]);                    
                }
            }

            return result;
        }

        public bool Delete(System.Int64? ID)
        {
            bool result = false;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("p_Form4Report_Delete", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            AddParameter(   cmd, "@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, ID);
            
                            var pFound = AddParameter(cmd, "@Removed", SqlDbType.Bit, 0, ParameterDirection.Output, false, 0, 0, string.Empty, DataRowVersion.Current, 0);

                cmd.ExecuteNonQuery();

                result = (bool)pFound.Value;
            }

            return result;
        }


                public IList<Form4Report> GetByIssuerID(System.Int64 IssuerID)
        {
            var entitiesOut = base.GetBy<Form4Report, System.Int64>("p_Form4Report_GetByIssuerID", IssuerID, "@IssuerID", SqlDbType.BigInt, 0, Form4ReportFromRow);

            return entitiesOut;
        }
                public IList<Form4Report> GetByReporterID(System.Int64 ReporterID)
        {
            var entitiesOut = base.GetBy<Form4Report, System.Int64>("p_Form4Report_GetByReporterID", ReporterID, "@ReporterID", SqlDbType.BigInt, 0, Form4ReportFromRow);

            return entitiesOut;
        }
        
        public IList<Form4Report> GetAll()
        {
            IList<Form4Report> result = base.GetAll<Form4Report>("p_Form4Report_GetAll", Form4ReportFromRow);

            return result;
        }

        public Form4Report Insert(Form4Report entity) 
        {
            Form4Report entityOut = base.Upsert<Form4Report>("p_Form4Report_Insert", entity, AddUpsertParameters, Form4ReportFromRow);

            return entityOut;
        }

        public Form4Report Update(Form4Report entity) 
        {
            Form4Report entityOut = base.Upsert<Form4Report>("p_Form4Report_Update", entity, AddUpsertParameters, Form4ReportFromRow);

            return entityOut;
        }

        protected SqlCommand AddUpsertParameters(SqlCommand cmd, Form4Report entity)
        {
                SqlParameter pID = new SqlParameter("@ID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, (object)entity.ID != null ? (object)entity.ID : DBNull.Value);   cmd.Parameters.Add(pID); 
                SqlParameter pIssuerID = new SqlParameter("@IssuerID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "IssuerID", DataRowVersion.Current, (object)entity.IssuerID != null ? (object)entity.IssuerID : DBNull.Value);   cmd.Parameters.Add(pIssuerID); 
                SqlParameter pReporterID = new SqlParameter("@ReporterID", System.Data.SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "ReporterID", DataRowVersion.Current, (object)entity.ReporterID != null ? (object)entity.ReporterID : DBNull.Value);   cmd.Parameters.Add(pReporterID); 
                SqlParameter pReportID = new SqlParameter("@ReportID", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "ReportID", DataRowVersion.Current, (object)entity.ReportID != null ? (object)entity.ReportID : DBNull.Value);   cmd.Parameters.Add(pReportID); 
                SqlParameter pIsOfficer = new SqlParameter("@IsOfficer", System.Data.SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsOfficer", DataRowVersion.Current, (object)entity.IsOfficer != null ? (object)entity.IsOfficer : DBNull.Value);   cmd.Parameters.Add(pIsOfficer); 
                SqlParameter pIsDirector = new SqlParameter("@IsDirector", System.Data.SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsDirector", DataRowVersion.Current, (object)entity.IsDirector != null ? (object)entity.IsDirector : DBNull.Value);   cmd.Parameters.Add(pIsDirector); 
                SqlParameter pIs10PctHolder = new SqlParameter("@Is10PctHolder", System.Data.SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "Is10PctHolder", DataRowVersion.Current, (object)entity.Is10PctHolder != null ? (object)entity.Is10PctHolder : DBNull.Value);   cmd.Parameters.Add(pIs10PctHolder); 
                SqlParameter pIsOther = new SqlParameter("@IsOther", System.Data.SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsOther", DataRowVersion.Current, (object)entity.IsOther != null ? (object)entity.IsOther : DBNull.Value);   cmd.Parameters.Add(pIsOther); 
                SqlParameter pOtherText = new SqlParameter("@OtherText", System.Data.SqlDbType.NVarChar, 250, ParameterDirection.Input, false, 0, 0, "OtherText", DataRowVersion.Current, (object)entity.OtherText != null ? (object)entity.OtherText : DBNull.Value);   cmd.Parameters.Add(pOtherText); 
                SqlParameter pOfficerTitle = new SqlParameter("@OfficerTitle", System.Data.SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "OfficerTitle", DataRowVersion.Current, (object)entity.OfficerTitle != null ? (object)entity.OfficerTitle : DBNull.Value);   cmd.Parameters.Add(pOfficerTitle); 
                SqlParameter pDate = new SqlParameter("@Date", System.Data.SqlDbType.Date, 0, ParameterDirection.Input, false, 0, 0, "Date", DataRowVersion.Current, (object)entity.Date != null ? (object)entity.Date : DBNull.Value);   cmd.Parameters.Add(pDate); 
                SqlParameter pDateSubmitted = new SqlParameter("@DateSubmitted", System.Data.SqlDbType.Date, 0, ParameterDirection.Input, false, 0, 0, "DateSubmitted", DataRowVersion.Current, (object)entity.DateSubmitted != null ? (object)entity.DateSubmitted : DBNull.Value);   cmd.Parameters.Add(pDateSubmitted); 
        
            return cmd;
        }

        protected Form4Report Form4ReportFromRow(DataRow row)
        {
            var entity = new Form4Report();

                    entity.ID = !DBNull.Value.Equals(row["ID"]) ? (System.Int64?)row["ID"] : default(System.Int64?);
                    entity.IssuerID = !DBNull.Value.Equals(row["IssuerID"]) ? (System.Int64)row["IssuerID"] : default(System.Int64);
                    entity.ReporterID = !DBNull.Value.Equals(row["ReporterID"]) ? (System.Int64)row["ReporterID"] : default(System.Int64);
                    entity.ReportID = !DBNull.Value.Equals(row["ReportID"]) ? (System.String)row["ReportID"] : default(System.String);
                    entity.IsOfficer = !DBNull.Value.Equals(row["IsOfficer"]) ? (System.Boolean)row["IsOfficer"] : default(System.Boolean);
                    entity.IsDirector = !DBNull.Value.Equals(row["IsDirector"]) ? (System.Boolean)row["IsDirector"] : default(System.Boolean);
                    entity.Is10PctHolder = !DBNull.Value.Equals(row["Is10PctHolder"]) ? (System.Boolean)row["Is10PctHolder"] : default(System.Boolean);
                    entity.IsOther = !DBNull.Value.Equals(row["IsOther"]) ? (System.Boolean)row["IsOther"] : default(System.Boolean);
                    entity.OtherText = !DBNull.Value.Equals(row["OtherText"]) ? (System.String)row["OtherText"] : default(System.String);
                    entity.OfficerTitle = !DBNull.Value.Equals(row["OfficerTitle"]) ? (System.String)row["OfficerTitle"] : default(System.String);
                    entity.Date = !DBNull.Value.Equals(row["Date"]) ? (System.DateTime)row["Date"] : default(System.DateTime);
                    entity.DateSubmitted = !DBNull.Value.Equals(row["DateSubmitted"]) ? (System.DateTime)row["DateSubmitted"] : default(System.DateTime);
        
            return entity;
        }
        
    }
}
