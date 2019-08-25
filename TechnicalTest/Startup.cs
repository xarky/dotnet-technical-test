using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TechnicalTest
{
    using POC.BusinessLogic.Interfaces;
    using POC.BusinessLogic.Managers;
    using Microsoft.EntityFrameworkCore;
    using Model;
    using System;
    using DbService;
    using POC.Common;

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
            ConfigureDatabase(services);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //services.AddSingleton<IRepository, InMemoryRepository>();
            services.AddTransient<IRepository, DbRepository>();
            services.AddTransient<ICustomersManager, CustomersManager>();
            services.AddTransient<IAccountsManager, AccountsManager>();
            services.AddTransient<ILogRepository, DbLogRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            UpdateDatabases(serviceProvider);
        }

        protected virtual void ConfigureDatabase(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                    o =>
                    {
                        o.MigrationsAssembly("Model");
                        o.EnableRetryOnFailure(2);
                    }),
                    ServiceLifetime.Transient);
        }

        public void UpdateDatabases(IServiceProvider services)
        {
            var applictionDbContext = services.GetService<ApplicationDbContext>();
            applictionDbContext.Database.Migrate();
            applictionDbContext.Database.EnsureCreated();
        }
    }
}
