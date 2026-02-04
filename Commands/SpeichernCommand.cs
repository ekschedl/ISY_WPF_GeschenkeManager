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
    // Manuelle Implementierung von ICommand
    // Der Command kapselt die Aktion „Senden“
    internal class SpeichernCommand : ICommand
    {
        // auf dessen Daten der Command arbeitet
        // Referenz auf das Model
        private readonly Kind _kind;

        // Konstruktor bekommt das Model übergeben
        public SpeichernCommand(Kind kind)
        {
            this._kind = kind;

            // hören auf PropertyChanged vom Model,
            // damit der Button automatisch neu geprüft wird,
            // wenn sich Daten ändern
            _kind.PropertyChanged += Kind_PropertyChanged;
        }


        // WPF hört auf dieses Event,
        // um CanExecute erneut aufzurufen
        public event EventHandler CanExecuteChanged;

        // Button deaktiviert bei Schulnote 5
        public bool CanExecute(object parameter)
        {
            // Button aktiv:
            // - bei Noten besser als 5
            // - ODER wenn Eltern einverstanden sind
            return _kind.GerundeteNote < 5 || _kind.ElternEinverstanden;
        }

        // Wird beim Klick auf den Button ausgeführt
        // Der Command greift nur auf das Model zu
        //keine Logik im View
        public void Execute(object parameter)
        {
            MessageBox.Show(
                "Name: " + _kind.Vorname + "\n" +
                "Note: " + _kind.Durchschnittsnote.ToString("F1") + "\n" +
                "Geschenke: " + _kind.GeschenkeText,
                "Daten wurden gespeichert: ",
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );
        }
        // Diese Methode wird aufgerufen,
        // wenn sich eine Property im Model ändert
        private void Kind_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Reagiert nur auf Änderungen der GerundeteNote,
            // da sie für CanExecute relevant ist
            if (e.PropertyName == nameof(Kind.GerundeteNote)
               || e.PropertyName == nameof(Kind.ElternEinverstanden))
            {
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