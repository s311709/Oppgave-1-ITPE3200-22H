using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Oppgave1App.DAL;
using Oppgave1App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oppgave1App.Controllers
{

    [Route("[controller]/[action]")]
    public class Oppgave1Controller : ControllerBase
    {
        private readonly IOppgave1Repository _db;
        private ILogger<Oppgave1Controller> _log;


        public Oppgave1Controller(IOppgave1Repository db, ILogger<Oppgave1Controller> log)
        {
            _db = db;
            _log = log;
        }

        public async Task<ActionResult> HentAlle()
        {
            List<Oppgave1> alleOppgave1ene = await _db.HentAlle();

            return Ok(alleOppgave1ene);

        }
    }
}
