using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CountryWeb.Data;
using CountryWeb.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CountryWeb
{
    public class Startup
    {
        private string connRoot { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            // Echo:取得連線資訊
            connRoot = UStore.GetUStore(Configuration["ConnectionStrings:Root"], "Root");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Echo : DI
            services.AddDbContext<TgContext>(delegate (DbContextOptionsBuilder options)
            {
                options.UseSqlServer(connRoot);
            });
            services.AddScoped<InewsListsService, newsListsService>();

            services.AddControllersWithViews();
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
