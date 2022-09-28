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
            try {

            //  skal vi lagre separate rader for observatører eller skal alt lagres i obervasjon?
            var nyObservatørrad = new Observatør();
            nyObservatørrad.Fornavn = innObservasjon.FornavnObservatør;
            nyObservatørrad.Etternavn = innObservasjon.EtternavnObservatør;
            nyObservatørrad.Telefon = innObservasjon.TelefonObservatør;
            nyObservatørrad.Epost = innObservasjon.EpostObservatør;
            _db.Observatører.Add(nyObservatørrad);
            await _db.SaveChangesAsync();


             var nyObservasjonsrad = new EnkeltObservasjon();
                // må lage et input for nedtrekkslisten her "type UFO"
                //  nyObservasjonsrad.Modell = innObservasjon.Modell;
            nyObservasjonsrad.TidspunktObservert = innObservasjon.TidspunktObservert;
            nyObservasjonsrad.KommuneObservert = innObservasjon.KommuneObservert;
            nyObservasjonsrad.BeskrivelseAvObservasjon = innObservasjon.BeskrivelseAvObservasjon;
                //observatør
                /*observert Ufo: et ufo-objekt man velger i nedtrekkslisten
                hva gjør man hvis det er en ny ufo? må da fylle inn en ny ufo, lag sjekk
                som i postnummerliste
                 */

                // sjekk om ufo er allerede observert
                //ved å se om det er merket på nedtrekkslisten
                //ellers lag nytt objekt
                var sjekkUfoModell = await _db.UFOer.FindAsync(innObservasjon.KallenavnUFO);

                if (sjekkUfoModell == null)
                {
                    var nyUFO = new UFO();
                    nyUFO.Modell = innObservasjon.Modell;
                    //kallenavn burde bare skje med nytt objekt, flytt
                    nyUFO.Kallenavn = innObservasjon.KallenavnUFO;
                    //legg den nye UFOen til i enkeltobservasjon
                    nyObservasjonsrad.ObservertUFO = nyUFO;
                }
                else
                {
                    //legg observatøren til i observasjonsobjektet
                    nyObservasjonsrad.Observatør = nyObservatørrad;
                    // hvis allerede sett, lagre UFOen som allerede er i DB
                    nyObservasjonsrad.ObservertUFO = sjekkUfoModell;
                }

                /*
                //trenger ikke den over, legg inn en "ingen av de over" for å trigge nytt objekt
                if (innObservasjon.Modell == "ikke på listen")
                {
                    var nyUFO = new UFO();
                    nyUFO.Modell = innObservasjon.Modell;
                    nyUFO.Kallenavn = innObservasjon.KallenavnUFO;
                    //lagrer et nytt ufo-objekt

                 //   nyObservasjonsrad skal dette inn i observasjon somehow?
                 //hvordan legge til Observasjoner = new List<EnkeltObservasjon>(), GangerObservert = 0 };
                }
                else
                {
                    //øk count på sighting på UFOen med 1

                    //lagre ufoen som den sett i enkeltobservasjonsobjektet
                    nyObservasjonsrad.ObservertUFO = innObservasjon.KallenavnUFO;
                }
                */

                
                //kallenavn
                //modell

                // kallenavn og modell er i observasjon ikke enkeltobservasjon, må kodes inn


                // db har enkeltobservasjon og observatør ikke obervasjon

                //enkeltobservasjon vs observasjon?
                _db.EnkeltObservasjoner.Add(nyObservasjonsrad);
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

                foreach (var enkeltObservasjon in alleEnkeltObservasjoner)
                {
                    //Har valgt å ikke ta med alle atributtene, kan dette være i en egen siden hvor man får mer info om hver observasjon?
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

    }
}
