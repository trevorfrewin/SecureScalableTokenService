﻿using SSTS.Library.Common.Data;
using SSTS.Library.Common.Logging;
using SSTS.Library.ConfigurationManagement;
using SSTS.Library.Mongo;

namespace SSTS.Api.Command
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
            IEnumerable<IDatabaseConnectionSet> databaseConnectionSets = new DatabaseConnectionLoader().FromAppSettings(Configuration.GetSection("DatabaseConnectionSet"));
            services.AddSingleton<IEnumerable<IDatabaseConnectionSet>>(databaseConnectionSets);
            services.AddScoped<IDatabaseAccessFactory, MongoDatabaseAccessFactory>();
            services.AddScoped<IConfigurationManagementSource, ConfigurationManagementSource>();

            services.AddLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddProvider(new DocumentLoggerProvider(new MongoDatabaseAccessFactory(databaseConnectionSets)));
                });
            services.AddMvc(option => option.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
