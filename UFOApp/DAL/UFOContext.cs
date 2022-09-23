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
        public string Info { get; set; }

        //Når denne er virtual lastes observatøren inn samtidig med EnkeltObservasjon med bruk av lazy loading
        virtual public Observatør Observatør { get; set; }

        virtual public UFO UfoObservert { get; set; }
    }
    public class UFO
    {
        [Key]
        public int Id { get; set; }
        public string Info { get; set; }
    }
    public class Observatør
    {
        [Key]
        public int Id { get; set; }
        public string Info { get; set; }
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
        
        
        //Denne gjør det mulig å bruke lazy loading
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        optionsBuilder.UseLazyLoadingProxies();
        }

    }
    
}
