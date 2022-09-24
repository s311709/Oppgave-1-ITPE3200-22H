using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UFOApp.Models;

namespace UFOApp.DAL
{
    public interface IUFORepository
    {
        Task <List<Observasjon>> HentAlleObservasjoner();
        Task<Observasjon> HentEnObservasjon(int id);
        Task<List<UFO>> HentAlleUFOer();


    }

}
