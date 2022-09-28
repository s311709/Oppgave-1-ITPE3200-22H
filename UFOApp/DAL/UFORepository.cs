﻿using Microsoft.EntityFrameworkCore;
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

                //Først må du sjekke om observatør finnes fra før, dersom den ikke gjør det lagres en ny observatør
                // sånn som du har gjort. Det må bare pakkes inn i en if-loop.

                //Deretter sjekker du om UFO finnes, sånn som du har gjort, før du lager ny UFO hvis den ikke finnes

                //Deretter lagrer du en ny EnkeltObservasjon med UFOen og Observatøren i attributter inni EnkeltObservasjon

                //Så legger du EnkeltObservasjon inn i listene til UFO og Observatør

                //Til slutt oppdaterer du UFO og Observatør sine atributter antallObservasjoner og sistObservert
                //Har gjort dette i DBInit, basically så skal man inkrementere antallObservasjoner med én for UFO og Observatør
                //og deretter iterere gjennom Observasjoner-listen for å finne mest nylige dato for å få sistObservert



            //  skal vi lagre separate rader for observatører eller skal alt lagres i obervasjon?
            var nyObservatørrad = new Observatør();
            nyObservatørrad.Fornavn = innObservasjon.FornavnObservatør;
            nyObservatørrad.Etternavn = innObservasjon.EtternavnObservatør;
                //OBS telefonnummer kommer inn som null, se på formattering
            nyObservatørrad.Telefon = innObservasjon.TelefonObservatør;
            nyObservatørrad.Epost = innObservasjon.EpostObservatør;
            _db.Observatører.Add(nyObservatørrad);
            await _db.SaveChangesAsync();


             var nyObservasjonsrad = new EnkeltObservasjon();
                // må lage et input for nedtrekkslisten her "type UFO"
                //  nyObservasjonsrad.Modell = innObservasjon.Modell;

                //OBS tidspunkt blir ikke lagret rett, nullstilles en plass mellom innObservasjon og visning på nettsiden
            nyObservasjonsrad.TidspunktObservert = innObservasjon.TidspunktObservert;
            nyObservasjonsrad.KommuneObservert = innObservasjon.KommuneObservert;
            nyObservasjonsrad.BeskrivelseAvObservasjon = innObservasjon.BeskrivelseAvObservasjon;


                //pakket inn i en ekstra try/catch for debugging, prøver var under også hopper til catch, tror UFOer returnerer null og nullobjektet trigger catch
                try {
                    var innKallenavn = innObservasjon.KallenavnUFO;
                    var sjekkUfoModell = await _db.UFOer.FindAsync(innKallenavn);
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
                _db.EnkeltObservasjoner.Add(nyObservasjonsrad);
                }
                catch
                {
                    return false;
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
                    //RUTH id er også fjernet her, auto-increment i UFOContext (se notater der)
                    //Id = UFO.Id,
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
