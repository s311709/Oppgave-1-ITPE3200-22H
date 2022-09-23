using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UFOApp.DAL
{
    public class DBInit
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<UFOContext>();

                //sletter og oppretter databasen for seeding
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();


                var observatør1 = new Observatør { Fornavn = "Per", Etternavn = "Nydalen", Telefon = "74295105", Epost = "pnydalen@epost.no", RegistrerteObservasjoner = new List<EnkeltObservasjon>() };
                var observatør2 = new Observatør { Fornavn = "Johanne", Etternavn = "Viken", Telefon = "79376924", Epost = "jviken@epost.no", RegistrerteObservasjoner = new List<EnkeltObservasjon>() };
                var observatør3 = new Observatør { Fornavn = "Erik", Etternavn = "Hansen", Telefon = "67478474", Epost = "ehansen@epost.no", RegistrerteObservasjoner = new List<EnkeltObservasjon>() };

                var ufo1 = new Ufo { Kallenavn = "Nyttårs-UFOen", Modell = "Flyvende tallerken", Observasjoner = new List<EnkeltObservasjon>() };
                var ufo2 = new Ufo { Kallenavn = "Korn-UFOen i Østfold", Modell = "Ukjent", Observasjoner = new List<EnkeltObservasjon>() };

                //Forklaring av datetime:
                //År, måned, dag, time, minutt, sekund
                var observasjon1 = new EnkeltObservasjon
                {
                    TidspunktObservert = new DateTime(2022, 01, 01, 0, 0, 0),
                    KommuneObservert = "Oslo",
                    BeskrivelseAvObservasjon = "UFOen fløy over Stortinget under nyttårsfeiringen",
                    Observatør = observatør1,
                    ObservertUFO = ufo1
                };

                var observasjon2 = new EnkeltObservasjon
                {
                    TidspunktObservert = new DateTime(2005, 01, 01, 0, 0, 0),
                    KommuneObservert = "Narvik",
                    BeskrivelseAvObservasjon = "UFOen kunne sees over sjøen der det ikke var raketter",
                    Observatør = observatør1,
                    ObservertUFO = ufo1
                };

                var observasjon3 = new EnkeltObservasjon
                {
                    TidspunktObservert = new DateTime(2009, 04, 21, 8, 30, 0),
                    KommuneObservert = "Halden",
                    BeskrivelseAvObservasjon = "Sirkelfenomen i åker",
                    Observatør = observatør2,
                    ObservertUFO = ufo2
                };

                var observasjon4 = new EnkeltObservasjon
                {
                    TidspunktObservert = new DateTime(2010, 9, 4, 8, 30, 0),
                    KommuneObservert = "Våler",
                    BeskrivelseAvObservasjon = "Ødelagte avlinger pga UFO-aktivitet",
                    Observatør = observatør3,
                    ObservertUFO = ufo2
                };

                //legger til observasjoner i lister
                ufo1.Observasjoner.Add(observasjon1);
                ufo1.Observasjoner.Add(observasjon2);
                ufo2.Observasjoner.Add(observasjon3);
                ufo2.Observasjoner.Add(observasjon4);

                observatør1.RegistrerteObservasjoner.Add(observasjon1);
                observatør1.RegistrerteObservasjoner.Add(observasjon2);
                observatør2.RegistrerteObservasjoner.Add(observasjon3);
                observatør3.RegistrerteObservasjoner.Add(observasjon4);

                context.Add(observasjon1);
                context.Add(observasjon2);
                context.Add(observasjon3);
                context.Add(observasjon4);

                context.SaveChanges();

            }
        }
    }
}
