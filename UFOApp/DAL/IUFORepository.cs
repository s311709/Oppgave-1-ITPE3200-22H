﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UFOApp.Models;

namespace UFOApp.DAL
{
    public interface IUFORepository
    {

        Task<bool> LagreObservasjon(Observasjon innObservasjon);
        Task <List<Observasjon>> HentAlleObservasjoner();
        Task<Observasjon> HentEnObservasjon(int id);
        Task<List<UFO>> HentAlleUFOer();
        Task<UFO> HentEnUFO(string kallenavn);
        Task<List<Observatør>> HentAlleObservatører();
        Task<Observatør> HentEnObservatør(string fornavn, string etternavn);
        Task<bool> EndreObservasjon(Observasjon endreObservasjon);
        Task<bool> SlettObservasjon(int id);

    }

}
