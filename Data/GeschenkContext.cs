using SchedlbergerEkaterina_WPF_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SchedlbergerEkaterina_WPF_.Data
{
    // Datenbankkontext für Geschenke
    // Erbt von DbContext (Entity Framework)
    public class GeschenkContext00 : DbContext
    {
        // DbSet repräsentiert die Geschenke-Tabelle in der Datenbank
        // Wird für CRUD-Operationen verwendet
        public DbSet<Geschenk> Geschenke { get; set; }
    }
}
