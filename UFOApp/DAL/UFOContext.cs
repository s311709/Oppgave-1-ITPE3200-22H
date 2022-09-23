using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UFOApp.DAL
{

    public class UFO
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

    }

}
