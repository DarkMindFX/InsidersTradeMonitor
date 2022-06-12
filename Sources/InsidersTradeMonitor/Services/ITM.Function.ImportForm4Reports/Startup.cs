


using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using ITM.Functions.Common;
using ITM.Function.ImportForm4Reports.Helpers;

[assembly: FunctionsStartup(typeof(ITM.Functions.ImportForm4Reports.Startup))]
namespace ITM.Functions.ImportForm4Reports
{
    public class Startup : FunctionStartupBase
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            base.Configure(builder);

            var dalDerivativeTransactionDal = InitDal<Interfaces.IDerivativeTransactionDal>();
            builder.Services.AddSingleton<Interfaces.IDerivativeTransactionDal>(dalDerivativeTransactionDal);
            builder.Services.AddSingleton<ITM.Services.Dal.IDerivativeTransactionDal, ITM.Services.Dal.DerivativeTransactionDal>();

            var dalNonDerivativeTransactionDal = InitDal<Interfaces.INonDerivativeTransactionDal>();
            builder.Services.AddSingleton<Interfaces.INonDerivativeTransactionDal>(dalNonDerivativeTransactionDal);
            builder.Services.AddSingleton<ITM.Services.Dal.INonDerivativeTransactionDal, ITM.Services.Dal.NonDerivativeTransactionDal>();

            var dalEntityDal = InitDal<Interfaces.IEntityDal>();
            builder.Services.AddSingleton<Interfaces.IEntityDal>(dalEntityDal);
            builder.Services.AddSingleton<ITM.Services.Dal.IEntityDal, ITM.Services.Dal.EntityDal>();

            var dalForm4ReportDal = InitDal<Interfaces.IForm4ReportDal>();
            builder.Services.AddSingleton<Interfaces.IForm4ReportDal>(dalForm4ReportDal);
            builder.Services.AddSingleton<ITM.Services.Dal.IForm4ReportDal, ITM.Services.Dal.Form4ReportDal>();

            var dalEntityTypeDal = InitDal<Interfaces.IEntityTypeDal>();
            builder.Services.AddSingleton<Interfaces.IEntityTypeDal>(dalEntityTypeDal);
            builder.Services.AddSingleton<ITM.Services.Dal.IEntityTypeDal, ITM.Services.Dal.EntityTypeDal>();

            var dalOwnershipTypeDal = InitDal<Interfaces.IOwnershipTypeDal>();
            builder.Services.AddSingleton<Interfaces.IOwnershipTypeDal>(dalOwnershipTypeDal);
            builder.Services.AddSingleton<ITM.Services.Dal.IOwnershipTypeDal, ITM.Services.Dal.OwnershipTypeDal>();

            var dalTransactionTypeDal = InitDal<Interfaces.ITransactionTypeDal>();
            builder.Services.AddSingleton<Interfaces.ITransactionTypeDal>(dalTransactionTypeDal);
            builder.Services.AddSingleton<ITM.Services.Dal.ITransactionTypeDal, ITM.Services.Dal.TransactionTypeDal>();

            var dalTransactionCodeDal = InitDal<Interfaces.ITransactionCodeDal>();
            builder.Services.AddSingleton<Interfaces.ITransactionCodeDal>(dalTransactionCodeDal);
            builder.Services.AddSingleton<ITM.Services.Dal.ITransactionCodeDal, ITM.Services.Dal.TransactionCodeDal>();

            /** Adding Form4 DAL wrapper **/
            builder.Services.AddSingleton<IForm4DalWrapper, Form4DalWrapper>();
        }
    }
}