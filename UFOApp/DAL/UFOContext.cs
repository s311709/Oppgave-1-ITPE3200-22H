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
        [Key]
        public int Id { get; set; }
        public DateTime TidspunktObservert { get; set; } //Dato+klokkeslett
        public string KommuneObservert { get; set; }
        public string BeskrivelseAvObservasjon { get; set; }
        virtual public Observatør Observatør { get; set; }
        virtual public Ufo ObservertUFO { get; set; }
    }

    public class Ufo
    {
        [Key]
        public int Id { get; set; }
        public String Kallenavn { get; set; }
        public string Modell { get; set; }

        //Kan eventuelt legges til senere, usikker på hvordan implementere
        //public string GangerObservert { get; set; }
        //public DateTime SistObservert { get; set; }
        public virtual List<EnkeltObservasjon> Observasjoner { get; set; }
    }

    public class Observatør
    {
        [Key]
        public int Id { get; set; }
        public string Fornavn { get; set; }
        public string Etternavn { get; set; }
        public string Telefon { get; set; }
        public string Epost {get; set;}

        //Kan eventuelt legges til senere, usikker på hvordan implementere
        //public int AntallRegistrerteObservasjoner { get; set; }
        public virtual List<EnkeltObservasjon> RegistrerteObservasjoner { get; set; }

    }

    public class UFOContext : DbContext
    {

        //oppretter databasen
        public UFOContext(DbContextOptions<UFOContext> options) :
            base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Ufo> UFOer { get; set; }
        public DbSet<EnkeltObservasjon> EnkeltObservasjoner { get; set; }
        public DbSet<Observatør> Observatører { get; set; }
        
        
        //Denne gjør det mulig å bruke lazy loading til atributtene som er virtual
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        optionsBuilder.UseLazyLoadingProxies();
        }

    }
    
}
