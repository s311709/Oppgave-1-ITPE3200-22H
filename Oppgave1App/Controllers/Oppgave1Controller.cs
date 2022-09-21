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
        private readonly Oppgave1Context _db;
        private ILogger<Oppgave1Controller> _log;


        public Oppgave1Controller(Oppgave1Context db, ILogger<Oppgave1Controller> log)
        {
            _db = db;
            _log = log;
        }

        public List<Oppgave1> HentAlle()
        {
            List<Oppgave1> alleOppgave1ene = _db.Oppgave1er.ToList();

            return alleOppgave1ene;

        }
    }
}
