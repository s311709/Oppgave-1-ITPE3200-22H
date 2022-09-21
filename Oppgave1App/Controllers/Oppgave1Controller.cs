using Microsoft.AspNetCore.Mvc;
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
        public List<Oppgave1> HentAlle()
        {
            var oppgave1ene = new List<Oppgave1>();
            var oppgave1element = new Oppgave1
            {
                Info = "Dette er info"
            };
            var oppgave1element2 = new Oppgave1
            {
                Info = "Dette er info igjen"
            };

            oppgave1ene.Add(oppgave1element);
            oppgave1ene.Add(oppgave1element2);

            return oppgave1ene;

        }
    }
}
