using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedlbergerEkaterina_WPF_.Models
{
    // Model für ein Geschenk
    // Enthält  Properties 
    public class Geschenk
    {

        public int GeschenkId { get; set; }
        public string Name { get; set; }

        public bool IstWichtig { get; set; }
        public double Preis { get; set; }
        public string Prioritaet { get; set; }

        public string Bild { get; set; }

        public DateTime Erstellungsdatum { get; set; }
    }
}
