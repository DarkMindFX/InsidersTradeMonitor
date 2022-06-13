using ITM.Function.ImportForm4Reports.Helpers;
using ITM.Interfaces.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITM.Function.ImportForm4Reports
{
    public class ImporterTestBase : ITM.Test.Common.TestBase
    {
        #region Support methods
        protected IForm4DalWrapper PrepareForm4DalWrapper()
        {
            IForm4DalWrapper wrapper = new Form4DalWrapper(
                CreateDal<ITM.Services.Dal.Form4ReportDal, ITM.DAL.MSSQL.Form4ReportDal, Form4Report>("DALInitParams"),
                CreateDal<ITM.Services.Dal.DerivativeTransactionDal, ITM.DAL.MSSQL.DerivativeTransactionDal, DerivativeTransaction>("DALInitParams"),
                CreateDal<ITM.Services.Dal.NonDerivativeTransactionDal, ITM.DAL.MSSQL.NonDerivativeTransactionDal, NonDerivativeTransaction>("DALInitParams"),
                CreateDal<ITM.Services.Dal.EntityDal, ITM.DAL.MSSQL.EntityDal, Entity>("DALInitParams"),
                CreateDal<ITM.Services.Dal.EntityTypeDal, ITM.DAL.MSSQL.EntityTypeDal, EntityType>("DALInitParams"),
                CreateDal<ITM.Services.Dal.OwnershipTypeDal, ITM.DAL.MSSQL.OwnershipTypeDal, OwnershipType>("DALInitParams"),
                CreateDal<ITM.Services.Dal.TransactionCodeDal, ITM.DAL.MSSQL.TransactionCodeDal, TransactionCode>("DALInitParams"),
                CreateDal<ITM.Services.Dal.TransactionTypeDal, ITM.DAL.MSSQL.TransactionTypeDal, TransactionType>("DALInitParams")
                );

            return wrapper;
        }

        protected TServiceDal CreateDal<TServiceDal, TDal, TEntity>(string configParamName) where TDal : ITM.Interfaces.IDalBase<TEntity>, new()
                                                                                          where TServiceDal : ITM.Services.Dal.IDalBase<TEntity>
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configParamName).Get<TestDalInitParams>();

            TDal dal = new TDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            TServiceDal serviceDal = (TServiceDal)Activator.CreateInstance(typeof(TServiceDal), dal); ;

            return serviceDal;
        }

        protected void CleanUpReports(IList<long> ids)
        {
            // Cleaning up
            if (ids != null)
            {
                ITM.Services.Dal.Form4ReportDal form4ReportDal = CreateDal<ITM.Services.Dal.Form4ReportDal, ITM.DAL.MSSQL.Form4ReportDal, Form4Report>("DALInitParams");

                foreach (var id in ids)
                {
                    form4ReportDal.Delete(id);
                }
            }
        }

        #endregion
    }
}
