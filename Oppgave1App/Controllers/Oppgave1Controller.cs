using Microsoft.AspNetCore.Mvc;
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

        public Oppgave1Controller(Oppgave1Context db)
        {
            _db = db;
        }

        public List<Oppgave1> HentAlle()
        {
            List<Oppgave1> alleOppgave1ene = _db.Oppgave1er.ToList();

            return alleOppgave1ene;

        }
    }
}
