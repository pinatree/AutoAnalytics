using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Microsoft.EntityFrameworkCore;
using AutoAnalyticsServer.SqlServerEFModel;

namespace AutoAnalyticsServer
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
            services.AddControllersWithViews();
            //var x = new AutoAnalyticsSqlServerEFContext();
            //x.InitInMemoryValues();

#if DEBUG
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            //services.AddDbContext<AutoAnalyticsServer.SqlServerEFModel.AutoAnalyticsSqlServerEFContext>(ServiceLifetime.Singleton);
            services.AddDbContext<AutoAnalyticsSqlServerEFContext>(opt =>
            {
                opt.UseInMemoryDatabase("TestMemoryDb");
                //opt.UseInternalServiceProvider(serviceProvider);
            });

#else
            services.AddDbContext<AutoAnalyticsServer.SqlServerEFModel.AutoAnalyticsSqlServerEFContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("AutoAnalyticsDB"));
            }, ServiceLifetime.Singleton);
#endif
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
