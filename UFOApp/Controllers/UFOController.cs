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

        public async Task<bool> LagreObservasjon(Observasjon innObservasjon)
        {
            return await _db.Lagre(innObservasjon);
        }

        public async Task<ActionResult> HentAlleObservasjoner()
        {
            List<Observasjon> Observasjoner = await _db.HentAlleObservasjoner();

            return Ok(Observasjoner);

        }
        public async Task<ActionResult> HentEnObservasjon(int id)
        {
            Observasjon observasjonen = await _db.HentEnObservasjon(id);
            if (observasjonen == null)
            {
                _log.LogInformation("Fant ikke observasjonen");
                return NotFound("Fant ikke observasjonen");
            }
            return Ok(observasjonen);
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

public async Task<ActionResult> HentAlleUFOer()
        {
            List<UFO> UFOer = await _db.HentAlleUFOer();

            return Ok(UFOer);

        }
    }
}
