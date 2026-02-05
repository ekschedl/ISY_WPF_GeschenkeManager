using SchedlbergerEkaterina_WPF_.Commands;
using SchedlbergerEkaterina_WPF_.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SchedlbergerEkaterina_WPF_.ViewModels
{
    // ViewModel für Geschenke-Verwaltung
    // Implementiert INotifyPropertyChanged für automatische UI-Updates
    public class GeschenkeViewModel : INotifyPropertyChanged
    {
        // INotifyPropertyChanged - für Datenbindung
        // Event wird ausgelöst, wenn sich eine Property ändert
        public event PropertyChangedEventHandler PropertyChanged;

        // Hilfsmethode zum Auslösen des PropertyChanged-Events
        private void OnPropertyChanged(string p)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));


        // Repository für Datenzugriff
        // readonly = kann nur im Konstruktor gesetzt werden
        private readonly GeschenkeRepository _rep = new GeschenkeRepository();


        // Alle Geschenke komplette Liste
        public ObservableCollection<Geschenk> MeineGeschenke { get; set; }
            = new ObservableCollection<Geschenk>();

        // Gefilterte Geschenke
        public ObservableCollection<Geschenk> MeineGeschenkeFiltered { get; set; }
            = new ObservableCollection<Geschenk>();


        //merkt sich: welches Geschenk gerade bearbeitet wird 
        private Geschenk _editGeschenk;

        // Ausgewähltes Geschenk in der Liste
        private Geschenk _ausgewGeschenk;
        public Geschenk AusgewGeschenk
        {
            get => _ausgewGeschenk;
            set
            {
                _ausgewGeschenk = value;
                OnPropertyChanged(nameof(AusgewGeschenk));
            }
        }

        // Suchtext für Filter
        private string _suchtext;
        public string Suchtext
        {
            get => _suchtext;
            set
            {
                _suchtext = value;
                OnPropertyChanged(nameof(Suchtext));
                Suchen();
            }
        }

        // Editmodus (true = bearbeiten, false = neu hinzufügen)
        private bool _istEditModus;
        public bool IstEditModus
        {
            get => _istEditModus;
            set
            {
                _istEditModus = value;
                OnPropertyChanged(nameof(IstEditModus));
            }
        }

        // Nachricht an den User
        private string _userMessage;
        public string UserMessage
        {
            get => _userMessage;
            set
            {
                _userMessage = value;
                OnPropertyChanged(nameof(UserMessage));
            }
        }

        // Name, Formularfelder für neues/bearbeitetes Geschenk
        private string _neuerName;
        public string NeuerName
        {
            get => _neuerName;
            set { _neuerName = value; OnPropertyChanged(nameof(NeuerName)); }
        }

        // Prio, Formularfelder  für neues/bearbeitetes Geschenk
        private string _neuePrioritaet;
        public string NeuePrioritaet
        {
            get => _neuePrioritaet;
            set { _neuePrioritaet = value; OnPropertyChanged(nameof(NeuePrioritaet)); }
        }

        // Bild, Formularfelder  für neues/bearbeitetes Geschenk
        private string _neuesBild;
        public string NeuesBild
        {
            get => _neuesBild;
            set { _neuesBild = value; OnPropertyChanged(nameof(NeuesBild)); }
        }

        // IstWichtig, Formularfeld für neues/bearbeitetes Geschenk
        private bool _istWichtig;
        public bool IstWichtig
        {
            get => _istWichtig;
            set { _istWichtig = value; OnPropertyChanged(nameof(IstWichtig)); }
        }

        // Preis, Formularfeld für neues/bearbeitetes Geschenk
        private double _preis;
        public double Preis
        {
            get => _preis;
            set { _preis = value; OnPropertyChanged(nameof(Preis)); }
        }

        // Berechnet die Anzahl der gefilterten Geschenke und speichert Ergebnis in String
        public string AnzahlGeschenke => $"Geschenke total: {MeineGeschenkeFiltered.Count}";

        // Commands für UI-Interaktionen (Button-Klicks)
        public ICommand DeleteCommand { get; }
        public ICommand CopyCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand AddOrSaveCommand { get; }
        public ICommand SearchCommand { get; }

        public ICommand NewListCommand { get; }





        // Konstruktor lädt Daten aus Repository und initialisiert Commands
        public GeschenkeViewModel()
        {
            AddOrSaveCommand = new AddOrSaveCommand(this);
           
            DeleteCommand = new DeleteGeschenkCommand(this);
            CopyCommand = new CopyGeschenkCommand(this);
            EditCommand = new EditGeschenkCommand(this);
            SearchCommand = new SearchCommand(this);

            NewListCommand = new NewListCommand(this);

            LoadData();
        }

        // Lädt Daten aus Repository und aktualisiert die Anzeige
        public void LoadData()
        {
            // Alle Geschenke aus Repository laden
            MeineGeschenke.Clear();
            foreach (var g in _rep.ReadAll())
                MeineGeschenke.Add(g);

            // Filter anwenden (zeigt gefilterte Liste)
            Suchen();

            // Automatisch das erste Geschenk auswählen, wenn Geschenke vorhanden sind
            if (MeineGeschenkeFiltered.Count > 0 && AusgewGeschenk == null)
            {
                AusgewGeschenk = MeineGeschenkeFiltered[0];
            }

            // Anzahl aktualisieren (UI wird benachrichtigt)
            OnPropertyChanged(nameof(AnzahlGeschenke));
        }

        // Formularfelder leeren
        private void ClearForm()
        {
            NeuerName = "";
            NeuePrioritaet = "";
            NeuesBild = null;
            IstWichtig = false;
            Preis = 0.0;
        }

        // Fügt neues Geschenk hinzu
        public void Add()
        {
            // Neues Geschenk im Repository speichern
            _rep.AddGeschenk(new Geschenk
            {
                Name = NeuerName,
                Prioritaet = NeuePrioritaet,
                Bild = NeuesBild,
                IstWichtig = IstWichtig,
                Preis = Preis,
                Erstellungsdatum = DateTime.Now
            });

            // Daten neu laden 
            LoadData();

            // Erfolgsmeldung anzeigen
            UserMessage = "✅ Geschenk wurde hinzugefügt";
            // Nach 3 Sekunden Meldung automatisch wieder löschen
            Task.Delay(3000).ContinueWith(_ => UserMessage = "");

            // Editmodus zurücksetzen und Formular leeren
            IstEditModus = false;
            ClearForm();
        }

        // Löscht ausgewähltes Geschenk
            public void Delete()
        {

            // Prüfen, ob überhaupt ein Geschenk ausgewählt ist
            if (AusgewGeschenk == null)
            {
                UserMessage = "⚠️ Bitte zuerst ein Geschenk auswählen";
                Task.Delay(3000).ContinueWith(_ => UserMessage = "");
                return;
            }

            // Geschenk aus Repository löschen
            _rep.RemoveGeschenk(AusgewGeschenk.GeschenkId);

            // Daten neu laden 
            LoadData();

            // Erfolgsmeldung anzeigen
            UserMessage = "🗑️ Geschenk wurde gelöscht";
            Task.Delay(3000).ContinueWith(_ => UserMessage = "");
        }

        // startet den Bearbeitungsmodus: nur Formular füllen, kopiert Daten ins Formular, speichert noch nichts
        public void EditStart()
        {
            if (AusgewGeschenk == null)
            {
                UserMessage = "⚠️ Bitte zuerst ein Geschenk auswählen";
                Task.Delay(3000).ContinueWith(_ => UserMessage = "");
                return;
            }

            // Geschenk merken (damit wir später wissen, welches bearbeitet wird)
            _editGeschenk = AusgewGeschenk;

            // Daten des Geschenks ins Formular kopieren
            NeuerName = _editGeschenk.Name;
            NeuePrioritaet = _editGeschenk.Prioritaet;
            NeuesBild = _editGeschenk.Bild;
            IstWichtig = _editGeschenk.IstWichtig;
            Preis = _editGeschenk.Preis;

            // Editmodus aktivieren
            IstEditModus = true;
        }
        // Speichert Änderungen am bearbeiteten Geschenk
        public void SaveEdit()
        {
            if (_editGeschenk == null) return;

            // Änderungen vom Formular ins Geschenk übernehmen
            _editGeschenk.Name = NeuerName;
            _editGeschenk.Prioritaet = NeuePrioritaet;
            _editGeschenk.Bild = NeuesBild;
            _editGeschenk.IstWichtig = IstWichtig;
            _editGeschenk.Preis = Preis;
            // Im Repository speichern
            _rep.UpdateGeschenk(_editGeschenk);

            // Erfolgsmeldung anzeigen
            UserMessage = "✏️ Änderungen wurden gespeichert";
            Task.Delay(3000).ContinueWith(_ => UserMessage = "");

            // Editmodus zurücksetzen
            IstEditModus = false;
            _editGeschenk = null;

            // Daten neu laden (zeigt die Änderungen in der Liste)
            LoadData();
            // Formular leeren
            ClearForm();
        }

        // Sucht nach Geschenken
        public void Suchen()
        {
            // Verwendet Repository Variante 1: SearchByName (nach Name suchen)
            var ergebnis = _rep.SearchByName(Suchtext);

            // Gefilterte Liste aktualisieren
            MeineGeschenkeFiltered.Clear();
            foreach (var g in ergebnis)
                MeineGeschenkeFiltered.Add(g);

            // Anzahl aktualisieren (UI wird benachrichtigt)
            OnPropertyChanged(nameof(AnzahlGeschenke));
        }


        // Suche nach "super wichtigen" Geschenken
        // Verwendet Repository Variante 2: Search() mit Lambda-Ausdruck
        public void SucheSuperWichtige()
        {
            // Verwendet Repository Variante 2: Search() mit beliebiger Property (Prioritaet)
            var ergebnis = _rep.Search(g => g.Prioritaet == "super wichtig");

            // Gefilterte Liste aktualisieren
            MeineGeschenkeFiltered.Clear();
            foreach (var g in ergebnis)
                MeineGeschenkeFiltered.Add(g);

            // Anzahl aktualisieren
            OnPropertyChanged(nameof(AnzahlGeschenke));

            // Erfolgsmeldung anzeigen
            UserMessage = "⭐ Super wichtige Geschenke gefunden";
            Task.Delay(3000).ContinueWith(_ => UserMessage = "");
        }

        // Zeige alle Geschenke wieder an - setzt Suchtext zurück, dann zeigt Suchen() automatisch alle
        public void ZeigeAlle()
        {
            // Suchtext zurücksetzen - Suchen() wird automatisch aufgerufen und zeigt alle
            Suchtext = "";
        }

        // Kopiert das ausgewählte Geschenk
        public void Copy()
        {
            // Prüfen, ob überhaupt ein Geschenk ausgewählt ist
            if (AusgewGeschenk == null)
            {
                UserMessage = "⚠️ Bitte zuerst ein Geschenk auswählen";
                Task.Delay(3000).ContinueWith(_ => UserMessage = "");
                return;
            }

            // Kopie erstellen (Repository erstellt neues Geschenk mit gleichen Daten)
            var kopie = _rep.Copy(AusgewGeschenk);

            // Daten neu laden (Kopie erscheint in der Liste)
            LoadData();

            // Kopie automatisch auswählen
            AusgewGeschenk = kopie;

            // Erfolgsmeldung anzeigen
            UserMessage = "📄 Geschenk wurde kopiert";
            Task.Delay(3000).ContinueWith(_ => UserMessage = "");
        }

        // Erstellt eine neue leere Liste (löscht alle Geschenke)
        public void NewList()
        {
            // Neue Liste bedeutet: bisherige Geschenke verwerfen
            // und mit einer leeren Sammlung neu starten

            // Alle Geschenke im Repository löschen
            _rep.DeleteAll();

            // Listen leeren
            MeineGeschenke.Clear();
            MeineGeschenkeFiltered.Clear();

            // Auswahl zurücksetzen
            AusgewGeschenk = null;

            // Erfolgsmeldung anzeigen
            UserMessage = "📂 Neue Geschenke-Zentrale gestartet";
            Task.Delay(3000).ContinueWith(_ => UserMessage = "");

            // Anzahl aktualisieren (zeigt "Geschenke total: 0")
            OnPropertyChanged(nameof(AnzahlGeschenke));
        }

    }
}
