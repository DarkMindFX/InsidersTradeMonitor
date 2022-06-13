


using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using ITM.Functions.Common;


[assembly: FunctionsStartup(typeof(ITM.Functions.ImportForm4Reports.Startup))]
namespace ITM.Functions.ImportForm4Reports
{
    public class Startup : FunctionStartupBase
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            base.Configure(builder);

            var dalDerivativeTransactionDal = InitDal<Interfaces.IDerivativeTransactionDal>();
            
        }
    }
}