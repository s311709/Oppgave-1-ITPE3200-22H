using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UFOApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UFOApp.Models;

namespace UFOApp.DAL
{
    public class UFORepository : IUFORepository
    {
        private readonly UFOContext _db;
        private ILogger<UFOController> _log;


        public UFORepository(UFOContext db, ILogger<UFOController> log)
        {
            _db = db;
            _log = log;
        }

        public async Task<bool> Lagre(Observasjon innObservasjon)
        {
            try
            {

                //Først sjekkes det om observatør finnes fra før, dersom den ikke gjør det lagres en ny observatør

                Observatør funnetObservatør = await _db.Observatører.FirstOrDefaultAsync(o => o.Etternavn == innObservasjon.EtternavnObservatør /*&& o.Fornavn = innObservasjon.FornavnObservatør*/);

                if (funnetObservatør == null)
                {
                    var nyObservatørrad = new Observatør
                    {
                        Fornavn = innObservasjon.FornavnObservatør,
                        Etternavn = innObservasjon.EtternavnObservatør,
                        Telefon = innObservasjon.TelefonObservatør,
                        Epost = innObservasjon.EpostObservatør,
                        RegistrerteObservasjoner = new List<EnkeltObservasjon>(),
                        AntallRegistrerteObservasjoner = 0,
                        SisteObservasjon = new DateTime()
                    };
                    _db.Observatører.Add(nyObservatørrad);
                    await _db.SaveChangesAsync();
                    funnetObservatør = await _db.Observatører.FirstOrDefaultAsync(o => o.Etternavn == innObservasjon.EtternavnObservatør /*&& o.Fornavn = innObservasjon.FornavnObservatør*/);

                }

                //Deretter sjekkes det om UFO finnes før det lages en ny UFO hvis den ikke finnes

                UFO funnetUFO = await _db.UFOer.FirstOrDefaultAsync(u => u.Kallenavn == innObservasjon.KallenavnUFO);

                if (funnetUFO == null)
                {
                    var nyUFOrad = new UFO
                    {
                        Modell = innObservasjon.Modell,
                        Kallenavn = innObservasjon.KallenavnUFO,
                        Observasjoner = new List<EnkeltObservasjon>(),
                        GangerObservert = 0,
                        SistObservert = new DateTime()
                    };
                    _db.UFOer.Add(nyUFOrad);
                    await _db.SaveChangesAsync();
                    //hva gjør denne?
                    funnetUFO = await _db.UFOer.FirstOrDefaultAsync(u => u.Kallenavn == innObservasjon.KallenavnUFO);
                    
                }
                
                //Deretter lages en ny EnkeltObservasjon med UFOen og Observatøren i attributter inni EnkeltObservasjon

                EnkeltObservasjon nyEnkeltObservasjonRad = new EnkeltObservasjon
                {
                    TidspunktObservert = innObservasjon.TidspunktObservert,
                    KommuneObservert = innObservasjon.KommuneObservert,
                    BeskrivelseAvObservasjon = innObservasjon.BeskrivelseAvObservasjon,
                    ObservertUFO = funnetUFO,
                    Observatør = funnetObservatør
                };

                _db.EnkeltObservasjoner.Add(nyEnkeltObservasjonRad);
                await _db.SaveChangesAsync();

                //Så legges EnkeltObservasjon inn i listene til UFO og Observatør

                // funnetUFO.Observasjoner.Add(nyEnkeltObservasjonRad);
                // funnetObservatør.RegistrerteObservasjoner.Add(nyEnkeltObservasjonRad);

                //Til slutt oppdateres UFO og Observatør sine atributter antallObservasjoner og sistObservert

                funnetObservatør.AntallRegistrerteObservasjoner++;
                funnetUFO.GangerObservert++;

                foreach (EnkeltObservasjon observasjon in funnetUFO.Observasjoner)
                {
                    
                    //setter SistObservert-atributten
                    if (observasjon.TidspunktObservert > funnetUFO.SistObservert)
                    {
                        funnetUFO.SistObservert = observasjon.TidspunktObservert;
                    }
                }

                foreach (EnkeltObservasjon observasjon in funnetObservatør.RegistrerteObservasjoner)
                {
                   
                    //setter SistObservert-atributten
                    if (observasjon.TidspunktObservert > funnetObservatør.SisteObservasjon)
                    {
                        funnetObservatør.SisteObservasjon = observasjon.TidspunktObservert;
                    }
                }
               
                await _db.SaveChangesAsync();

                return true;

            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Observasjon>> HentAlleObservasjoner()
        {
            try
            {
                List<EnkeltObservasjon> alleEnkeltObservasjoner = await _db.EnkeltObservasjoner.ToListAsync();

                List<Observasjon> alleObservasjoner = new List<Observasjon>();
                //note to self: sist observert er ikke koda, og observatør er null inni alleEnkeltObservasjoner under. Den som skaper exception? mangler jeg noe
                foreach (var enkeltObservasjon in alleEnkeltObservasjoner)
                {
                    //Har valgt å ikke ta med alle atributtene, kan dette være i en egen siden hvor man får mer info om hver observasjon?
                    //RUTH får tidvis en nullpointersxception her når jeg lagrer et nytt objekt og den skal gå tilbake til index.html for å vise det

                    var enObservasjon = new Observasjon
                    {
                        Id = enkeltObservasjon.Id,
                        KallenavnUFO = enkeltObservasjon.ObservertUFO.Kallenavn,
                        TidspunktObservert = enkeltObservasjon.TidspunktObservert,
                        KommuneObservert = enkeltObservasjon.KommuneObservert,
                        BeskrivelseAvObservasjon = enkeltObservasjon.BeskrivelseAvObservasjon,
                        FornavnObservatør = enkeltObservasjon.Observatør.Fornavn,
                        EtternavnObservatør = enkeltObservasjon.Observatør.Etternavn
                    };
                    alleObservasjoner.Add(enObservasjon);
                }
                return alleObservasjoner;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return null;
            }
        }

        public async Task<Observasjon> HentEnObservasjon(int id)
        {
            try
            {
                EnkeltObservasjon enkeltObservasjon = await _db.EnkeltObservasjoner.FindAsync(id);

                var enObservasjon = new Observasjon
                {
                    Id = enkeltObservasjon.Id,
                    KallenavnUFO = enkeltObservasjon.ObservertUFO.Kallenavn,
                    TidspunktObservert = enkeltObservasjon.TidspunktObservert,
                    KommuneObservert = enkeltObservasjon.KommuneObservert,
                    BeskrivelseAvObservasjon = enkeltObservasjon.BeskrivelseAvObservasjon,
                    Modell = enkeltObservasjon.ObservertUFO.Modell,
                    FornavnObservatør = enkeltObservasjon.Observatør.Fornavn,
                    EtternavnObservatør = enkeltObservasjon.Observatør.Etternavn,
                    TelefonObservatør = enkeltObservasjon.Observatør.Telefon,
                    EpostObservatør = enkeltObservasjon.Observatør.Epost
                };
                return enObservasjon;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return null;
            }

        }


        public async Task<List<UFO>> HentAlleUFOer()
        {
            List<UFO> alleUFOer = await _db.UFOer.ToListAsync();

            List<UFO> returUFOer = new List<UFO>();

            foreach (var UFO in alleUFOer)
            {
                var returUFO = new UFO
                {
                    Id = UFO.Id,
                    Kallenavn = UFO.Kallenavn,
                    Modell = UFO.Modell,
                    SistObservert = UFO.SistObservert,
                    GangerObservert = UFO.GangerObservert
                };
                returUFOer.Add(returUFO);
            }

            return returUFOer;
        }

        public async Task<UFO> HentEnUFO(string kallenavn)
        {
                     
            //Får ikke til å ta inn noe fra klient
            kallenavn = "Nyttårs-UFOen";
            try
            {
                UFO funnetUFO = await _db.UFOer.FirstOrDefaultAsync(u => u.Kallenavn == kallenavn);

                var returUFO = new UFO
                {
                    Id = funnetUFO.Id,
                    Kallenavn = funnetUFO.Kallenavn,
                    Modell = funnetUFO.Modell,
                    SistObservert = funnetUFO.SistObservert,
                    GangerObservert = funnetUFO.GangerObservert
                };

                return returUFO;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return null;
            }

        }

    }
}
