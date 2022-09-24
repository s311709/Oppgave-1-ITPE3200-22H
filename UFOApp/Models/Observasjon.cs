using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UFOApp.Models
{
    public class Observasjon
    {
        public int Id { get; set; }
        public String KallenavnUFO { get; set; }
        public DateTime TidspunktObservert { get; set; }
        public String KommuneObservert { get; set; }
        public String BeskrivelseAvObservasjon { get; set; }
        public String Modell { get; set; }
        public String FornavnObservatør { get; set; }
        public String EtternavnObservatør { get; set; }
        public String TelefonObservatør { get; set; }
        public String EpostObservatør { get; set; }
    }
}
