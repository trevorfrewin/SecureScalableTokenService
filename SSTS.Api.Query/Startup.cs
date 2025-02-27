using SSTS.Library.Common.Data;
using SSTS.Library.ConfigurationManagement;
using SSTS.Library.Mongo;

namespace SSTS.Api.Query
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
            services.AddScoped<IConfigurationManagementSource, ConfigurationManagementSource>();
            services.AddScoped<IEnumerable<IDatabaseConnectionSet>>(f => { return new DatabaseConnectionLoader().FromAppSettings(Configuration.GetSection("DatabaseConnectionSet")); });
            services.AddScoped<IDatabaseReader, DatabaseReader>();
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
