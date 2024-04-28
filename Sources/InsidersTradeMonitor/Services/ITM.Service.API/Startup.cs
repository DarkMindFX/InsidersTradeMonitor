

using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using System.Text;
using ITM.API.Helpers;
using ITM.API.MiddleWare;
using ITM.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ITM.API
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
            PrepareComposition();

            services.AddCors();
            services.AddControllers();

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            AddInjections(services, serviceConfig);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<JwtMiddleware>();

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
                string path = Path.GetDirectoryName(codeBase);
                return Path.Combine(path, "Plugins");
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
