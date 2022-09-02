using GraphQL;
using GraphQL.Server;
using ITM.API.Helpers;
using ITM.Interfaces;
using ITM.Service.GraphQL.Controllers;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;

namespace ITM.Service.GraphQL
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var serviceConfig = Configuration.GetSection("ServiceConfig").Get<ServiceConfig>();

            Console.WriteLine($"DALType: {serviceConfig.DALType}");
            foreach (var k in serviceConfig.DALInitParams.Keys)
            {
                Console.WriteLine($"{k}: {serviceConfig.DALInitParams[k]}");
            }

            PrepareComposition();

            services.AddScoped<AppSchema>();

            services.AddGraphQL()
                .AddSystemTextJson()
                .AddGraphTypes(typeof(AppSchema), ServiceLifetime.Scoped);


            services.AddControllers()
                .AddNewtonsoftJson(o => o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            AddInjections(services, serviceConfig);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void PrepareComposition()
        {
            AggregateCatalog catalog = new AggregateCatalog();
            var pluginsRoot = PluginsDirectory;
            var dirs = Directory.GetDirectories(pluginsRoot);
            foreach (var pluginDir in dirs)
            {
                DirectoryCatalog directoryCatalog = new DirectoryCatalog(pluginDir);
                catalog.Catalogs.Add(directoryCatalog);
            }
            Container = new CompositionContainer(catalog);
            Container.ComposeParts(this);
        }

        private string PluginsDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().Location;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.Combine(Path.GetDirectoryName(path), "Plugins");
            }
        }

        private CompositionContainer Container
        {
            get;
            set;
        }

        private void AddInjections(IServiceCollection services, ServiceConfig serviceCfg)
        {
            var dalDerivativeTransactionDal = InitDal<IDerivativeTransactionDal>(serviceCfg);
            services.AddSingleton<IDerivativeTransactionDal>(dalDerivativeTransactionDal);
            services.AddSingleton<ITM.Services.Dal.IDerivativeTransactionDal, ITM.Services.Dal.DerivativeTransactionDal>();

            var dalEntityDal = InitDal<IEntityDal>(serviceCfg);
            services.AddSingleton<IEntityDal>(dalEntityDal);
            services.AddSingleton<ITM.Services.Dal.IEntityDal, ITM.Services.Dal.EntityDal>();

            var dalEntityTypeDal = InitDal<IEntityTypeDal>(serviceCfg);
            services.AddSingleton<IEntityTypeDal>(dalEntityTypeDal);
            services.AddSingleton<ITM.Services.Dal.IEntityTypeDal, ITM.Services.Dal.EntityTypeDal>();

            var dalForm4ReportDal = InitDal<IForm4ReportDal>(serviceCfg);
            services.AddSingleton<IForm4ReportDal>(dalForm4ReportDal);
            services.AddSingleton<ITM.Services.Dal.IForm4ReportDal, ITM.Services.Dal.Form4ReportDal>();

            var dalNonDerivativeTransactionDal = InitDal<INonDerivativeTransactionDal>(serviceCfg);
            services.AddSingleton<INonDerivativeTransactionDal>(dalNonDerivativeTransactionDal);
            services.AddSingleton<ITM.Services.Dal.INonDerivativeTransactionDal, ITM.Services.Dal.NonDerivativeTransactionDal>();

            var dalOwnershipTypeDal = InitDal<IOwnershipTypeDal>(serviceCfg);
            services.AddSingleton<IOwnershipTypeDal>(dalOwnershipTypeDal);
            services.AddSingleton<ITM.Services.Dal.IOwnershipTypeDal, ITM.Services.Dal.OwnershipTypeDal>();

            var dalTransactionCodeDal = InitDal<ITransactionCodeDal>(serviceCfg);
            services.AddSingleton<ITransactionCodeDal>(dalTransactionCodeDal);
            services.AddSingleton<ITM.Services.Dal.ITransactionCodeDal, ITM.Services.Dal.TransactionCodeDal>();

            var dalTransactionTypeDal = InitDal<ITransactionTypeDal>(serviceCfg);
            services.AddSingleton<ITransactionTypeDal>(dalTransactionTypeDal);
            services.AddSingleton<ITM.Services.Dal.ITransactionTypeDal, ITM.Services.Dal.TransactionTypeDal>();

            var dalUserDal = InitDal<IUserDal>(serviceCfg);
            services.AddSingleton<IUserDal>(dalUserDal);
            services.AddSingleton<ITM.Services.Dal.IUserDal, ITM.Services.Dal.UserDal>();

            var dalImportRunDal = InitDal<IImportRunDal>(serviceCfg);
            services.AddSingleton<IImportRunDal>(dalImportRunDal);
            services.AddSingleton<ITM.Services.Dal.IImportRunDal, ITM.Services.Dal.ImportRunDal>();

            var dalImportRunForm4ReportDal = InitDal<IImportRunForm4ReportDal>(serviceCfg);
            services.AddSingleton<IImportRunForm4ReportDal>(dalImportRunForm4ReportDal);
            services.AddSingleton<ITM.Services.Dal.IImportRunForm4ReportDal, ITM.Services.Dal.ImportRunForm4ReportDal>();

            var dalImportRunStateDal = InitDal<IImportRunStateDal>(serviceCfg);
            services.AddSingleton<IImportRunStateDal>(dalImportRunStateDal);
            services.AddSingleton<ITM.Services.Dal.IImportRunStateDal, ITM.Services.Dal.ImportRunStateDal>();


            /** Connection Tester for health endpoint **/
            var dalConnTest = InitDal<IConnectionTestDal>(serviceCfg);
            services.AddSingleton<IConnectionTestDal>(dalConnTest);
        }

        private TDal InitDal<TDal>(ServiceConfig serviceCfg) where TDal : IInitializable
        {
            var dal = Container.GetExportedValue<TDal>(serviceCfg.DALType);
            var dalInitParams = dal.CreateInitParams();

            dalInitParams.Parameters = serviceCfg.DALInitParams;
            dal.Init(dalInitParams);

            return dal;

        }
    }
}
