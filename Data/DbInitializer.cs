using SchedlbergerEkaterina_WPF_.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedlbergerEkaterina_WPF_.Data
{
    // Initialisiert die Datenbank mit Testdaten
    internal class DbInitializer
    {
        // Füllt die Datenbank mit Standard-Geschenken, wenn sie leer ist
        public static void Seed()
        {
            // Datenbankkontext öffnen
            using (var context = new GeschenkContext00())
            {
                // Prüfen, ob bereits Daten vorhanden sind
                if (context.Geschenke.Any())
                    // Wenn ja, nichts machen
                    return;

                // Basis-Pfad der Anwendung ermitteln
                var basePath = AppDomain.CurrentDomain.BaseDirectory;

                // Erstes Test-Geschenk hinzufügen
                context.Geschenke.Add(new Geschenk
                {
                    Name = "Lego",
                    Prioritaet = "super wichtig",
                    // Bildpfad zusammenbauen
                    Bild = Path.Combine(basePath, "Images", "lego.jpg"),
                    IstWichtig = true,
                    Preis = 49.99,
                    Erstellungsdatum = new DateTime(2020, 2, 1, 23, 56, 0)
                });

                // Zweites Test-Geschenk hinzufügen
                context.Geschenke.Add(new Geschenk
                {
                    Name = "Katze",
                    Prioritaet = "weniger wichtig",
                    Bild = Path.Combine(basePath, "Images", "katze.jpg"),
                    IstWichtig = false,
                    Preis = 0.0,
                    Erstellungsdatum = new DateTime(2026, 1, 1, 14, 21, 0)
                });

                // Drittes Test-Geschenk hinzufügen
                context.Geschenke.Add(new Geschenk
                {
                    Name = "Kosmetik",
                    Prioritaet = "wichtig",
                    Bild = Path.Combine(basePath, "Images", "kosmetik.jpg"),
                    IstWichtig = true,
                    Preis = 29.99,
                    Erstellungsdatum = new DateTime(1999, 2, 1, 2, 30, 0)
                });

                // Viertes Test-Geschenk hinzufügen
                context.Geschenke.Add(new Geschenk
                {
                    Name = "Geld",
                    Prioritaet = "weniger wichtig",
                    Bild = Path.Combine(basePath, "Images", "geld.jpg"),
                    IstWichtig = false,
                    Preis = 100.0,
                    Erstellungsdatum = new DateTime(2000, 4, 1, 20, 30, 0)
                });

                // Fünftes Test-Geschenk hinzufügen
                context.Geschenke.Add(new Geschenk
                {
                    Name = "Bike",
                    Prioritaet = "super wichtig",
                    Bild = Path.Combine(basePath, "Images", "bike.jpg"),
                    IstWichtig = true,
                    Preis = 299.99,
                    Erstellungsdatum = new DateTime(2023, 2, 1, 23, 30, 0)
                });

                // Alle Änderungen in die Datenbank speichern
                context.SaveChanges();
            }
        }
    }
}