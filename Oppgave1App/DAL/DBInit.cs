using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oppgave1App.DAL
{
    public class DBInit
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<Oppgave1Context>();

                //sletter og oppretter databasen for seeding
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var oppgave1 = new Oppgave1
                {
                    Info = "dette er 1"
                };
                var oppgave2 = new Oppgave1
                {
                    Info = "dette er 2"
                };

                context.Add(oppgave1);
                context.Add(oppgave2);

                context.SaveChanges();

            }
        }
    }
}
