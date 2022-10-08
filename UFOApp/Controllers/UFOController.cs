using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UFOApp.DAL;
using UFOApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UFOApp.Controllers
{

    [Route("[controller]/[action]")]
    public class UFOController : ControllerBase
    {
        private readonly IUFORepository _db;
        private ILogger<UFOController> _log;


        public UFOController(IUFORepository db, ILogger<UFOController> log)
        {
            _db = db;
            _log = log;
        }

        public async Task<ActionResult> LagreObservasjon(Observasjon innObservasjon)
        {
            bool returOK = await _db.LagreObservasjon(innObservasjon);
            if (!returOK)
            {
                _log.LogInformation("Observasjonen kunne ikke lagres!");
                return BadRequest("Observasjonen kunne ikke lagres");
            }
            return Ok("Observasjon lagret");
        }

        public async Task<ActionResult> HentAlleObservasjoner()
        {
            List<Observasjon> Observasjoner = await _db.HentAlleObservasjoner();

            return Ok(Observasjoner); //Returnerer tomt array hvis tabellen er tom

        }
        public async Task<ActionResult> HentEnObservasjon(int id)
        {
            Observasjon observasjonen = await _db.HentEnObservasjon(id);
            
            return Ok(observasjonen); //Denne returnerer alltid OK, returnerer en tom observasjon dersom den ikke blir funnet
        }

        public async Task<ActionResult> HentAlleUFOer()
        {
            List<UFO> UFOer = await _db.HentAlleUFOer();

            return Ok(UFOer); //Returnerer tomt array hvis tabellen er tom

        }
        public async Task<ActionResult> HentEnUFO(string kallenavn)
        {
            UFO UFO = await _db.HentEnUFO(kallenavn);
            if (UFO == null)
            {
                _log.LogInformation("Fant ikke UFOen");
                return NotFound("Fant ikke UFOen");
            }
            return Ok(UFO);
        }
        public async Task<ActionResult> HentAlleObservatører()
        {
            List<Observatør> Observatører = await _db.HentAlleObservatører();

            return Ok(Observatører);  //Returnerer tomt array hvis tabellen er tom
        }

        public async Task<ActionResult> HentEnObservatør(string fornavn, string etternavn)
        {
            Observatør observatør = await _db.HentEnObservatør(fornavn, etternavn);
            if (observatør == null)
            {
                _log.LogInformation("Fant ikke observatøren");
                return NotFound("Fant ikke observatøren");
            }
            return Ok(observatør);
        }
        
        //EndreObservasjon
        public async Task<ActionResult> EndreObservasjon(Observasjon endreObservasjon)
        {
            bool returnOK = await _db.EndreObservasjon(endreObservasjon);
            if(!returnOK)
            {
                _log.LogInformation("Endringene kunne ikke utføres");
                return NotFound("Endringene av Observasjonen kunne ikke utføres");
            }
            return Ok("Observasjon endret");
        }
        public async Task<ActionResult> SlettObservasjon(int id)
        {

            bool returOK = await _db.SlettObservasjon(id);
            if (!returOK)
            {
                _log.LogInformation("Sletting av observasjon ble ikke utført");
                return NotFound("Sletting av observasjon ble ikke utført");
            }
            return Ok("Observasjon slettet.");
        }


    }
}
