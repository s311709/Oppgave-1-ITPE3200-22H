using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Oppgave1App.DAL
{

    public class Oppgave1
    {
        [Key]
        public int Id { get; set; }
        public string Info { get; set; }
    }

    public class Oppgave1Context : DbContext
    {

        //oppretter databasen
        public Oppgave1Context(DbContextOptions<Oppgave1Context> options) :
            base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Oppgave1> Oppgave1er { get; set; }

    }

}
