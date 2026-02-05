using SchedlbergerEkaterina_WPF_.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SchedlbergerEkaterina_WPF_.Commands
{
    // Command zum Suchen
    internal class SearchCommand : ICommand
    {
        // ViewModel-Referenz
        private readonly GeschenkeViewModel _vm;

        // Konstruktor
        public SearchCommand(GeschenkeViewModel vm)
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
            // Suche starten
            _vm.Suchen();
        }

    }
}