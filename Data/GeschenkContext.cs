using SchedlbergerEkaterina_WPF_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SchedlbergerEkaterina_WPF_.Data
{
    public class GeschenkContext06 : DbContext
    {
        public DbSet<Geschenk> Geschenke { get; set; }
    

    }
}
