using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oppgave1App.DAL
{
    public interface IOppgave1Repository
    {
        Task <List<Oppgave1>> HentAlle();
    }
}
