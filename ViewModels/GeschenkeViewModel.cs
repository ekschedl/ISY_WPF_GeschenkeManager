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
    public class GeschenkeViewModel : INotifyPropertyChanged
    {
        // ************* INotifyPropertyChanged 
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string p)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));


        // ************* Repository für Datenzugriff
        private readonly GeschenkeRepository _rep = new GeschenkeRepository();


        // Collections
        public ObservableCollection<Geschenk> MeineGeschenke { get; set; }
            = new ObservableCollection<Geschenk>();

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

        public string AnzahlGeschenke => $"Geschenke total: {MeineGeschenkeFiltered.Count}";

        // Commands
        // =========================
       
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

        // Hilfsmethoden Anzahl Geschenke für Anzeige im View
        public void LoadData()
        {
            MeineGeschenke.Clear();
            foreach (var g in _rep.ReadAll())
                MeineGeschenke.Add(g);

            Suchen();

            OnPropertyChanged(nameof(AnzahlGeschenke));
        }

        // Hilfsmethoden für Formularfelder
        private void ClearForm()
        {
            NeuerName = "";
            NeuePrioritaet = "";
            NeuesBild = null;
            IstWichtig = false;
            Preis = 0.0;
        }

        // ******** Logik, Methoden für Commands ********
        public void Add()
        {
            _rep.AddGeschenk(new Geschenk
            {
                Name = NeuerName,
                Prioritaet = NeuePrioritaet,
                Bild = NeuesBild,
                IstWichtig = IstWichtig,
                Preis = Preis,
                Erstellungsdatum = DateTime.Now
            });

            LoadData();
            UserMessage = "✅ Geschenk wurde hinzugefügt";
            Task.Delay(3000).ContinueWith(_ => UserMessage = "");

            IstEditModus = false;
            ClearForm();
        }

        public void Delete()
        {
            if (AusgewGeschenk == null)
            {
                UserMessage = "⚠️ Bitte zuerst ein Geschenk auswählen";
                Task.Delay(3000).ContinueWith(_ => UserMessage = "");
                return;
            }

            _rep.RemoveGeschenk(AusgewGeschenk.GeschenkId);
            LoadData();
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

            _editGeschenk = AusgewGeschenk;

            NeuerName = _editGeschenk.Name;
            NeuePrioritaet = _editGeschenk.Prioritaet;
            NeuesBild = _editGeschenk.Bild;
            IstWichtig = _editGeschenk.IstWichtig;
            Preis = _editGeschenk.Preis;

            IstEditModus = true;
        }

        public void SaveEdit()
        {
            if (_editGeschenk == null) return;

            _editGeschenk.Name = NeuerName;
            _editGeschenk.Prioritaet = NeuePrioritaet;
            _editGeschenk.Bild = NeuesBild;
            _editGeschenk.IstWichtig = IstWichtig;
            _editGeschenk.Preis = Preis;
            _rep.UpdateGeschenk(_editGeschenk);

            UserMessage = "✏️ Änderungen wurden gespeichert";
            Task.Delay(3000).ContinueWith(_ => UserMessage = "");

            IstEditModus = false;
            _editGeschenk = null;
            LoadData();
            ClearForm();
        }

        public void Suchen()
        {
            // Verwendet Repository Variante 1: SearchByName (nach Name suchen)
            var ergebnis = _rep.SearchByName(Suchtext);

            MeineGeschenkeFiltered.Clear();
            foreach (var g in ergebnis)
                MeineGeschenkeFiltered.Add(g);

            // Anzahl aktualisieren
            OnPropertyChanged(nameof(AnzahlGeschenke));
        }

        // Suche nach "super wichtigen" Geschenken - verwendet Repository Variante 2: Search()
        public void SucheSuperWichtige()
        {
            // Verwendet Repository Variante 2: Search() mit beliebiger Property (Prioritaet)
            var ergebnis = _rep.Search(g => g.Prioritaet == "super wichtig");

            MeineGeschenkeFiltered.Clear();
            foreach (var g in ergebnis)
                MeineGeschenkeFiltered.Add(g);

            // Anzahl aktualisieren
            OnPropertyChanged(nameof(AnzahlGeschenke));

            UserMessage = "⭐ Super wichtige Geschenke gefunden";
            Task.Delay(3000).ContinueWith(_ => UserMessage = "");
        }

        // Zeige alle Geschenke wieder an - setzt Suchtext zurück, dann zeigt Suchen() automatisch alle
        public void ZeigeAlle()
        {
            // Suchtext zurücksetzen - Suchen() wird automatisch aufgerufen und zeigt alle
            Suchtext = "";
        }


        public void Copy()
        {
            if (AusgewGeschenk == null)
            {
                UserMessage = "⚠️ Bitte zuerst ein Geschenk auswählen";
                Task.Delay(3000).ContinueWith(_ => UserMessage = "");
                return;
            }
            var kopie = _rep.Copy(AusgewGeschenk);

            LoadData();
            AusgewGeschenk = kopie;
            UserMessage = "📄 Geschenk wurde kopiert";
            Task.Delay(3000).ContinueWith(_ => UserMessage = "");
        }


        public void NewList()
        {
            // Neue Liste bedeutet: bisherige Geschenke verwerfen
            // und mit einer leeren Sammlung neu starten

            _rep.DeleteAll();
            MeineGeschenke.Clear();
            MeineGeschenkeFiltered.Clear();
            AusgewGeschenk = null;

            UserMessage = "📂 Neue Geschenke-Zentrale gestartet";
            Task.Delay(3000).ContinueWith(_ => UserMessage = "");
            OnPropertyChanged(nameof(AnzahlGeschenke));
        }


    }
}
