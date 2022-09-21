using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Oppgave1App.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oppgave1App
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
            services.AddDbContext<Oppgave1Context>(options =>
                            options.UseSqlite("Data Source = Oppgave1.db"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
