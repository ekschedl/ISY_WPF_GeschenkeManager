using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedlbergerEkaterina_WPF_.Models
{
    // Model-Klasse
    // Implementiert INotifyPropertyChanged,
    // damit WPF Änderungen an Properties automatisch erkennt
    public class Kind : INotifyPropertyChanged
    {
        // Event, das ausgelöst wird,
        // wenn sich eine Property ändert
        // WPF hört auf dieses Event
        public event PropertyChangedEventHandler PropertyChanged;


        public int Id { get; set; }

        private string _Vorname;
        public string Vorname
        {
            // Kein PropertyChanged nötig,
            // da Vorname keine andere Property beeinflusst
            get { return _Vorname; }
            set { _Vorname = value; }
        }


        private double _Durchschnittsnote;  // Skala 1.0–5.0

        // Durchschnittsnote - Property mit DataBinding
        // Wird vom Slider per TwoWay-DataBinding gesetzt
        // UpdateSourceTrigger=PropertyChanged → Setter wird
        // bei jeder Slider-Bewegung aufgerufen
        public double Durchschnittsnote
        {
            get { return _Durchschnittsnote; }
            set
            {
                // Speichert den neuen Wert
                _Durchschnittsnote = value;

                // Informiert WPF,
                // dass sich abhängige Properties geändert haben
                if (PropertyChanged != null)
                {
                    // GeschenkeText neu berechnen
                    PropertyChanged(this, new PropertyChangedEventArgs("GeschenkeText"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GerundeteNote"));

                    // Hintergrundfarbe neu berechnen
                    PropertyChanged(this, new PropertyChangedEventArgs("Hintergrundfarbe"));

                    // Durchschnittsnote selbst aktualisieren
                    PropertyChanged(this, new PropertyChangedEventArgs("Durchschnittsnote"));
                }
            }
        }

        // WarBrav
        // Wird über CheckBox (IsChecked) per DataBinding gesetzt
        private bool _WarBrav;
        public bool WarBrav
        {
            // Gibt den aktuellen Wert zurück
            get { return _WarBrav; }
            set
            {
                // Speichert den neuen Wert
                _WarBrav = value;

                // WarBrav beeinflusst nur den GeschenkeText
                if (PropertyChanged != null)
                {
                    // GeschenkeText neu berechnen
                    PropertyChanged(this, new PropertyChangedEventArgs("GeschenkeText"));
                }
            }
        }
        // Berechnete Property für die Schulnote
        // Kein Setter → Wert wird immer neu berechnet
        // WPF ruft den Getter nach PropertyChanged automatisch auf
        public int GerundeteNote
        {
            get
            {
                if (Durchschnittsnote < 2) return 1;
                if (Durchschnittsnote < 3) return 2;
                if (Durchschnittsnote < 4) return 3;
                if (Durchschnittsnote < 5) return 4;
                return 5;
            }
        }


        // Text für die Geschenk-Anzeige
        // Wert wird nicht gespeichert,
        // sondern jedes Mal neu berechnet
        public string GeschenkeText
        {
            get
            {
                int geschenke;

                // Anzahl abhängig von der gerundeten Note
                switch (GerundeteNote)
                {
                    case 1: geschenke = 4; break;
                    case 2: geschenke = 3; break;
                    case 3: geschenke = 2; break;
                    case 4: geschenke = 1; break;
                    default: geschenke = 0; break;
                }

                // Bonus-Geschenk, wenn das Kind brav war
                if (WarBrav)
                    geschenke += 1;

                // Kein Geschenk
                if (geschenke == 0)
                    return "❌ Kein Geschenk";

                // Erzeugt Geschenk-Emojis je nach Anzahl
                return $"{string.Concat(Enumerable.Repeat("🎁", geschenke))} ({geschenke})";

            }
        }

        // Hintergrundfarbe des Fensters
        // Wird direkt an Window.Background gebunden
        public string Hintergrundfarbe
        {
            get
            {
                if (GerundeteNote <= 2)
                    return "LightGreen";
                if (GerundeteNote == 3)
                    return "LightYellow";
                return "Tomato";
            }
        }

        private bool _ElternEinverstanden;
        public bool ElternEinverstanden
        {
            get { return _ElternEinverstanden; }
            set
            {
                _ElternEinverstanden = value;

                // Beeinflusst die Button-Aktivierung
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(ElternEinverstanden)));
            }
        }

    }
}
