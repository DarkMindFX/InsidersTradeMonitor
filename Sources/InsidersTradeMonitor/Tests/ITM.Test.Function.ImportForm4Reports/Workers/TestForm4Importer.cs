using ITM.DAL.MSSQL;
using ITM.Function.ImportForm4Reports.Helpers;
using ITM.Function.ImportForm4Reports.Workers;
using ITM.Interfaces;
using ITM.Interfaces.Entities;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITM.Test.Function.ImportForm4Reports.Workers
{
    public class TestForm4Importer : ITM.Test.Common.TestBase
    {
        public class For4ImporterTestWrapper : Form4Importer
        {
            public For4ImporterTestWrapper(Form4ImporterParams impParams) : base(impParams)
            {    
            }

            public void ImporterThread()
            {
                base.ImporterThread();
            }
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ImportOneDay_Success()
        {
            IList<long> ids = null;
            ISourceInitParams sourceInitParams = null;
            try
            {
                var parser = new ITM.Parser.Form4.Form4Parser();

                var source = new ITM.Source.SEC.SECSource();
                sourceInitParams = source.CreateInitParams();
                sourceInitParams.Logger = new ITM.Logging.NullLogger();

                source.Init(sourceInitParams);

                var form4DalWrapper = PrepareForm4DalWrapper();

                var impParams = new Form4ImporterParams()
                {
                    CIK = "320193",
                    DateFrom = DateTime.Parse("2022/04/19"),
                    DateTo = DateTime.Parse("2022/04/20"),
                    FilingParser = parser,
                    Source = source,
                    Form4DalWrappwer = form4DalWrapper

                };

                var wrapper = new For4ImporterTestWrapper(impParams);
                wrapper.ImporterThread();

                ids = wrapper.ImportedReportsIDs;
            }
            finally
            {
                // Cleaning up
                if(ids != null)
                {
                    ITM.Services.Dal.Form4ReportDal form4ReportDal = CreateDal<ITM.Services.Dal.Form4ReportDal, ITM.DAL.MSSQL.Form4ReportDal, Form4Report>("DALInitParams");

                    foreach (var id in ids)
                    {
                        form4ReportDal.Delete(id);
                    }
                }
            }
        }

        #region Support methods
        private IForm4DalWrapper PrepareForm4DalWrapper()
        {
            IForm4DalWrapper wrapper = new Form4DalWrapper(
                CreateDal<ITM.Services.Dal.Form4ReportDal, ITM.DAL.MSSQL.Form4ReportDal, Form4Report >("DALInitParams"),
                CreateDal<ITM.Services.Dal.DerivativeTransactionDal, ITM.DAL.MSSQL.DerivativeTransactionDal, DerivativeTransaction>("DALInitParams"),
                CreateDal<ITM.Services.Dal.NonDerivativeTransactionDal, ITM.DAL.MSSQL.NonDerivativeTransactionDal, NonDerivativeTransaction>("DALInitParams"),
                CreateDal<ITM.Services.Dal.EntityDal, ITM.DAL.MSSQL.EntityDal, Entity >("DALInitParams"),
                CreateDal<ITM.Services.Dal.EntityTypeDal, ITM.DAL.MSSQL.EntityTypeDal, EntityType >("DALInitParams"),
                CreateDal<ITM.Services.Dal.OwnershipTypeDal, ITM.DAL.MSSQL.OwnershipTypeDal, OwnershipType >("DALInitParams"),
                CreateDal<ITM.Services.Dal.TransactionCodeDal, ITM.DAL.MSSQL.TransactionCodeDal, TransactionCode >("DALInitParams"),
                CreateDal<ITM.Services.Dal.TransactionTypeDal, ITM.DAL.MSSQL.TransactionTypeDal, TransactionType >("DALInitParams")
                );

            return wrapper;
        }

        private TServiceDal CreateDal<TServiceDal, TDal, TEntity>(string configParamName) where TDal : ITM.Interfaces.IDalBase<TEntity>, new()
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

        #endregion
    }
}
