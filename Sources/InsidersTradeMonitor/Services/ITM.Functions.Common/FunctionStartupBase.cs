﻿using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using ITM.Interfaces;
using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;

namespace ITM.Functions.Common
{
    public class FunctionStartupBase : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            this.PrepareComposition();
        }

        public void Configure()
        {
            this.PrepareComposition();
        }

        public CompositionContainer Container
        {
            get;
            set;
        }

        public TDal InitDal<TDal>() where TDal : IInitializable
        {
            var funHelper = new FunctionHelper();

            var dal = Container.GetExportedValue<TDal>(funHelper.GetEnvironmentVariable<string>(Constants.ENV_DAL_TYPE));
            var dalInitParams = dal.CreateInitParams();

            dalInitParams.Parameters["ConnectionString"] = funHelper.GetEnvironmentVariable<string>(Constants.ENV_SQL_CONNECTION_STRING);

            dalInitParams.Parameters = dalInitParams.Parameters;
            dal.Init(dalInitParams);

            return dal;
        }

        #region Support methods

        private void PrepareComposition()
        {
            AggregateCatalog catalog = new AggregateCatalog();
            var pluginsRoot = PluginsDirectory;

            if (Directory.Exists(pluginsRoot))
            {
                var dirs = Directory.GetDirectories(pluginsRoot);
                foreach (var pluginDir in dirs)
                {
                    DirectoryCatalog directoryCatalog = new DirectoryCatalog(pluginDir);
                    catalog.Catalogs.Add(directoryCatalog);
                }
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

        #endregion
    }
}
