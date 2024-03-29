﻿using ITM.DAL.MSSQL;
using ITM.Interfaces;
using ITM.Interfaces.Entities;
using ITM.Service.DataImporter.Helpers;
using ITM.Service.DataImporter.Workers;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITM.Test.Service.DataImporter.Workers
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
                    Form4ReportDal form4ReportDal = CreateDal<Form4ReportDal, Form4Report>("DALInitParams");

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
                CreateDal<Form4ReportDal, Form4Report>("DALInitParams"),
                CreateDal<DerivativeTransactionDal, DerivativeTransaction>("DALInitParams"),
                CreateDal<NonDerivativeTransactionDal, NonDerivativeTransaction>("DALInitParams"),
                CreateDal<EntityDal, Entity>("DALInitParams"),
                CreateDal<EntityTypeDal, EntityType>("DALInitParams"),
                CreateDal<OwnershipTypeDal, OwnershipType>("DALInitParams"),
                CreateDal<TransactionCodeDal, TransactionCode>("DALInitParams"),
                CreateDal<TransactionTypeDal, TransactionType>("DALInitParams")
                );

            return wrapper;
        }

        private TDal CreateDal<TDal, TEntity>(string configParamName) where TDal : IDalBase<TEntity>, new()
        {
            IConfiguration config = GetConfiguration();
            var initParams = config.GetSection(configParamName).Get<TestDalInitParams>();

            TDal dal = new TDal();
            var dalInitParams = dal.CreateInitParams();
            dalInitParams.Parameters["ConnectionString"] = initParams.ConnectionString;
            dal.Init(dalInitParams);

            return dal;
        }

        #endregion
    }
}
