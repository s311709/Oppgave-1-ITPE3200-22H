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

        public async Task <List<Observasjon>> HentAlle()
        {
            List<UFO> UFOer = await _db.UFOer.ToListAsync();

            List<Observasjon> alleObservasjoner = new List<Observasjon>();

            foreach (var UFO in UFOer)
            {
                var enObservasjon = new Observasjon
                {
                    Info = UFO.Info
                };
                alleObservasjoner.Add(enObservasjon);
            }

            return alleObservasjoner;

        }
    }
}
