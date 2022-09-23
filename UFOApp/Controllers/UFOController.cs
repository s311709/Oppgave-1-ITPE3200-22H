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

        public async Task<ActionResult> HentAlle()
        {
            List<Observasjon> Observasjoner = await _db.HentAlle();

            return Ok(Observasjoner);

        }
    }
}
