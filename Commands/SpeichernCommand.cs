using SchedlbergerEkaterina_WPF_.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SchedlbergerEkaterina_WPF_.Commands
{
    // Command zum Speichern - kapselt die "Senden"-Aktion
    internal class SpeichernCommand : ICommand
    {
        // Referenz auf das Kind-Model
        private readonly Kind _kind;

        // Konstruktor bekommt das Model übergeben
        public SpeichernCommand(Kind kind)
        {
            // Model speichern
            this._kind = kind;

            // Auf PropertyChanged vom Model hören,
            // damit Button automatisch neu geprüft wird
            _kind.PropertyChanged += Kind_PropertyChanged;
        }

        // Event für Änderungen der Ausführbarkeit
        // WPF hört darauf, um CanExecute erneut aufzurufen
        public event EventHandler CanExecuteChanged;

        // Prüft, ob Command ausgeführt werden kann
        // Button deaktiviert bei Schulnote 5
        public bool CanExecute(object parameter)
        {
            // Button aktiv:
            // - bei Noten besser als 5
            // - ODER wenn Eltern einverstanden sind
            return _kind.GerundeteNote < 5 || _kind.ElternEinverstanden;
        }

        // Wird beim Klick auf den Button ausgeführt
        // Command greift nur auf das Model zu - keine Logik im View
        public void Execute(object parameter)
        {
            // Daten in MessageBox anzeigen
            MessageBox.Show(
                "Name: " + _kind.Vorname + "\n" +
                "Note: " + _kind.Durchschnittsnote.ToString("F1") + "\n" +
                "Geschenke: " + _kind.GeschenkeText,
                "Daten wurden gespeichert: ",
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );
        }
        // Wird aufgerufen, wenn sich eine Property im Model ändert
        private void Kind_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Reagiert nur auf Änderungen der GerundeteNote oder ElternEinverstanden,
            // da sie für CanExecute relevant sind
            if (e.PropertyName == nameof(Kind.GerundeteNote)
               || e.PropertyName == nameof(Kind.ElternEinverstanden))
            {
                // Event auslösen, damit WPF CanExecute neu prüft
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}

/*
 * 
Command → kapselt die Aktion (Button-Klick)

CanExecute → steuert, ob der Button aktiv ist

PropertyChanged → sorgt dafür, dass sich der Button automatisch aktualisiert

Execute → führt die Aktion aus, ohne Logik im View

 */