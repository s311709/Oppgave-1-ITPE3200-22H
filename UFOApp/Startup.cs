using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using UFOApp.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UFOApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Denne lar oss bruke controlleren
            services.AddControllers();
            //Denne lar oss bruke contexten//databasen
            services.AddDbContext<UFOContext>(options =>
                            options.UseSqlite("Data Source = UFO.db"));
            //Denne gjør at vi kan bruke IRepository
            services.AddScoped<IUFORepository, UFORepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //Denne oppdretter en logg under /Logs
                loggerFactory.AddFile("Logs/UFOLogg.txt");
                //Dette initialiserer databasen med DBInit
                DBInit.Initialize(app); 
            }

            app.UseRouting();

            //Denne lar oss bruke filene under wwwroot
            app.UseStaticFiles();

            //Denne lar oss bruke controlleren
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
