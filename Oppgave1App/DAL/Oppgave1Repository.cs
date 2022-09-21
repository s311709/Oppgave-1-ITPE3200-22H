using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Oppgave1App.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oppgave1App.DAL
{
    public class Oppgave1Repository : IOppgave1Repository
    {
        private readonly Oppgave1Context _db;
        private ILogger<Oppgave1Controller> _log;


        public Oppgave1Repository(Oppgave1Context db, ILogger<Oppgave1Controller> log)
        {
            _db = db;
            _log = log;
        }

        public async Task <List<Oppgave1>> HentAlle()
        {
            List<Oppgave1> alleOppgave1ene = await _db.Oppgave1er.ToListAsync();
            return alleOppgave1ene;

        }
    }
}
