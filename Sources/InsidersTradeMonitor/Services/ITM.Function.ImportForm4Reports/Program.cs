using ITM.Function.ImportForm4Reports.Helpers;
using ITM.Functions.Common;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ITM.Function.V1.ImportForm4Reports
{
    public class Program : FunctionStartupBase
    {
        public void Startup()
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWebApplication()
                .ConfigureServices(services =>
                {
                    base.Configure();

                    services.AddApplicationInsightsTelemetryWorkerService();
                    services.ConfigureFunctionsApplicationInsights();

                    var dalDerivativeTransactionDal = InitDal<Interfaces.IDerivativeTransactionDal>();
                    services.AddSingleton<Interfaces.IDerivativeTransactionDal>(dalDerivativeTransactionDal);
                    services.AddSingleton<ITM.Services.Dal.IDerivativeTransactionDal, ITM.Services.Dal.DerivativeTransactionDal>();

                    var dalNonDerivativeTransactionDal = InitDal<Interfaces.INonDerivativeTransactionDal>();
                    services.AddSingleton<Interfaces.INonDerivativeTransactionDal>(dalNonDerivativeTransactionDal);
                    services.AddSingleton<ITM.Services.Dal.INonDerivativeTransactionDal, ITM.Services.Dal.NonDerivativeTransactionDal>();

                    var dalEntityDal = InitDal<Interfaces.IEntityDal>();
                    services.AddSingleton<Interfaces.IEntityDal>(dalEntityDal);
                    services.AddSingleton<ITM.Services.Dal.IEntityDal, ITM.Services.Dal.EntityDal>();

                    var dalForm4ReportDal = InitDal<Interfaces.IForm4ReportDal>();
                    services.AddSingleton<Interfaces.IForm4ReportDal>(dalForm4ReportDal);
                    services.AddSingleton<ITM.Services.Dal.IForm4ReportDal, ITM.Services.Dal.Form4ReportDal>();

                    var dalEntityTypeDal = InitDal<Interfaces.IEntityTypeDal>();
                    services.AddSingleton<Interfaces.IEntityTypeDal>(dalEntityTypeDal);
                    services.AddSingleton<ITM.Services.Dal.IEntityTypeDal, ITM.Services.Dal.EntityTypeDal>();

                    var dalOwnershipTypeDal = InitDal<Interfaces.IOwnershipTypeDal>();
                    services.AddSingleton<Interfaces.IOwnershipTypeDal>(dalOwnershipTypeDal);
                    services.AddSingleton<ITM.Services.Dal.IOwnershipTypeDal, ITM.Services.Dal.OwnershipTypeDal>();

                    var dalTransactionTypeDal = InitDal<Interfaces.ITransactionTypeDal>();
                    services.AddSingleton<Interfaces.ITransactionTypeDal>(dalTransactionTypeDal);
                    services.AddSingleton<ITM.Services.Dal.ITransactionTypeDal, ITM.Services.Dal.TransactionTypeDal>();

                    var dalTransactionCodeDal = InitDal<Interfaces.ITransactionCodeDal>();
                    services.AddSingleton<Interfaces.ITransactionCodeDal>(dalTransactionCodeDal);
                    services.AddSingleton<ITM.Services.Dal.ITransactionCodeDal, ITM.Services.Dal.TransactionCodeDal>();

                    var dalImportRunDal = InitDal<Interfaces.IImportRunDal>();
                    services.AddSingleton<Interfaces.IImportRunDal>(dalImportRunDal);
                    services.AddSingleton<ITM.Services.Dal.IImportRunDal, ITM.Services.Dal.ImportRunDal>();

                    var dalImportRunForm4ReportDal = InitDal<Interfaces.IImportRunForm4ReportDal>();
                    services.AddSingleton<Interfaces.IImportRunForm4ReportDal>(dalImportRunForm4ReportDal);
                    services.AddSingleton<ITM.Services.Dal.IImportRunForm4ReportDal, ITM.Services.Dal.ImportRunForm4ReportDal>();

                    var dalImportRunStateDal = InitDal<Interfaces.IImportRunStateDal>();
                    services.AddSingleton<Interfaces.IImportRunStateDal>(dalImportRunStateDal);
                    services.AddSingleton<ITM.Services.Dal.IImportRunStateDal, ITM.Services.Dal.ImportRunStateDal>();


                    /** Adding Form4 DAL wrapper **/
                    services.AddSingleton<IForm4DalWrapper, Form4DalWrapper>();

                    /** Adding ImportRun DAL facade **/
                    services.AddSingleton<IImportRunDalFacade, ImportRunDalFacade>();
                })
                .Build();

            host.Run();
        }
        public static void Main(string[] args)
        {
            var function = new Program();
            function.Startup();


        }

    }

}
