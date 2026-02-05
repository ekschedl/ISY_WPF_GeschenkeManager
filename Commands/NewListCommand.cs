using SchedlbergerEkaterina_WPF_.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SchedlbergerEkaterina_WPF_.Commands
{
    // Command zum Erstellen einer neuen Liste
    public class NewListCommand : ICommand
    {
        // ViewModel-Referenz
        private readonly GeschenkeViewModel _vm;

        // Konstruktor
        public NewListCommand(GeschenkeViewModel vm)
        {
            // ViewModel speichern
            _vm = vm;
        }

        // Event für Änderungen der Ausführbarkeit
        public event EventHandler CanExecuteChanged;
        // Prüft, ob Command ausgeführt werden kann
        public bool CanExecute(object parameter) => true;

        // Führt den Command aus
        public void Execute(object parameter)
        {
            // Benutzer fragen, ob aktuelle Liste verworfen werden soll
            var result = MessageBox.Show(
                "Aktuelle Geschenke wirklich verwerfen?",
                "Neue Geschenke-Zentrale",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            // Wenn Benutzer ja sagt, neue Liste erstellen
            if (result == MessageBoxResult.Yes)
            {
                _vm.NewList();   
            }

        }
    }
}
