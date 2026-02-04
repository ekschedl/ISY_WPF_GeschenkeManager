using SchedlbergerEkaterina_WPF_.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedlbergerEkaterina_WPF_.Models
{
    //Repository = CRUD + EF + SaveChanges
    internal class GeschenkeRepository
    {
        private readonly GeschenkContext06 _context;

        public GeschenkeRepository()
        {
            _context = new GeschenkContext06();
        }

        // READ
        public List<Geschenk> ReadAll()
        {
            return _context.Geschenke.ToList();
        }

        // CREATE
        public void AddGeschenk(Geschenk geschenk)
        {
            _context.Geschenke.Add(geschenk);
            _context.SaveChanges();

        }

        // DELETE
        public void RemoveGeschenk(int id)
        {
            var geschenk = _context.Geschenke.FirstOrDefault(x => x.GeschenkId == id);
            if (geschenk == null) return;

            _context.Geschenke.Remove(geschenk);
            _context.SaveChanges();

        }

        // DELETE ALL
        public void DeleteAll()
        {
            _context.Geschenke.RemoveRange(_context.Geschenke);
            _context.SaveChanges();
        }

        // UPDATE
        public void UpdateGeschenk(Geschenk geschenk)
        {
            var dbGeschenk = _context.Geschenke
                .Single(x => x.GeschenkId == geschenk.GeschenkId);

            dbGeschenk.Name = geschenk.Name;
            dbGeschenk.Prioritaet = geschenk.Prioritaet;
            dbGeschenk.Bild = geschenk.Bild;
            dbGeschenk.IstWichtig = geschenk.IstWichtig;
            dbGeschenk.Preis = geschenk.Preis;

            _context.SaveChanges();
        }


        //COPY
        public Geschenk Copy(Geschenk original)
        {
            var kopie = new Geschenk
            {
                Name = original.Name + " (Kopie)",
                Prioritaet = original.Prioritaet,
                Bild = original.Bild,
                IstWichtig = original.IstWichtig,
                Preis = original.Preis,
                Erstellungsdatum = DateTime.Now
            };

            AddGeschenk(kopie);
            return kopie;
        }

        // SEARCH - Variante 1: Suchmethode nach Name (Property: Name)
        public List<Geschenk> SearchByName(string suchtext)
        {
            if (string.IsNullOrWhiteSpace(suchtext))
                return _context.Geschenke.ToList();
            
            return _context.Geschenke
                .Where(g => g.Name != null && g.Name.ToLower().Contains(suchtext.ToLower()))
                .ToList();
        }

        // SEARCH - Variante 2: Generische Suchmethode mit beliebiger Property als Argument
        // Verwendet ein Predicate (Func<Geschenk, bool>), um nach beliebigen Properties zu suchen
        public List<Geschenk> Search(Func<Geschenk, bool> predicate)
        {
            return _context.Geschenke.Where(predicate).ToList();
        }

    }
}
