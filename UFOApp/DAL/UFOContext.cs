using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UFOApp.DAL
{
    public class EnkeltObservasjon
    {
        //RUTH Kan det gjelde denne også, at ID er unødveldig når man har Key og når de er tilknyttet Observasjon som hovedklasse (som postnummer og kunde)?
        [Key]
        public int Id { get; set; }
        public DateTime TidspunktObservert { get; set; } //Dato+klokkeslett
        public string KommuneObservert { get; set; }
        public string BeskrivelseAvObservasjon { get; set; }
        virtual public Observatør Observatør { get; set; }
        virtual public UFO ObservertUFO { get; set; }
    }

    public class UFO
    {
        [Key]
        /*RUTH: fant problemet med UFO! Kallenavn må være primary key for at man skal kunne søke i objektet
        *notater fra forelesning
        *        //Key, using System,ComponentModel.DataAnnotations; inkrementerer for å gi unik ID
        */
        //   public int Id { get; set; } // er fjernet fra her og fra repo (søk på RUTH)
        public String Kallenavn { get; set; }
        public string Modell { get; set; }
        public virtual List<EnkeltObservasjon> Observasjoner { get; set; }
        //GangerObservert kan ha en counter i lagre/endre/slett som inkrementerer/dekrementerer for hver gang det legges inn/fjernes en observasjon i Observasjoner-listen
        public int GangerObservert { get; set; }
        //SistObservert: For lagre/endre/slett: Er det mulig å iterere gjennom Observasjoner-listen og velge den største dato-tiden blant observasjonene for å få denne atributten?
        public DateTime SistObservert { get; set; }
    }

    public class Observatør
    {
        [Key]
        public int Id { get; set; }
        public string Fornavn { get; set; }
        public string Etternavn { get; set; }
        public string Telefon { get; set; }
        public string Epost {get; set;}

        public virtual List<EnkeltObservasjon> RegistrerteObservasjoner { get; set; }

        //AntallRegistrerteObservasjoner kan ha en counter i lagre/endre/slett som inkrementerer/dekrementerer for hver gang det legges inn/fjernes en observasjon i RegistrerteObservasjoner-listen
        public int AntallRegistrerteObservasjoner { get; set; }
        //SisteObservasjon: For lagre/endre/slett: Er det mulig å iterere gjennom RegistrerteObservasjoner-listen og velge den største dato-tiden blant observasjonene for å få denne atributten?
        public DateTime SisteObservasjon { get; set; }

    }

    public class UFOContext : DbContext
    {
        //oppretter databasen
        public UFOContext(DbContextOptions<UFOContext> options) :
            base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<UFO> UFOer { get; set; }
        public DbSet<EnkeltObservasjon> EnkeltObservasjoner { get; set; }
        public DbSet<Observatør> Observatører { get; set; }
        
        //Denne gjør det mulig å bruke lazy loading til atributtene som er virtual
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        optionsBuilder.UseLazyLoadingProxies();
        }

    }
    
}
