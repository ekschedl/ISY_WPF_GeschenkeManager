using SchedlbergerEkaterina_WPF_.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SchedlbergerEkaterina_WPF_.Commands
{
    // Command zum Löschen eines Geschenks
    internal class DeleteGeschenkCommand : ICommand
    {
        // ViewModel-Referenz
        private readonly GeschenkeViewModel _vm;

        // Konstruktor
        public DeleteGeschenkCommand(GeschenkeViewModel vm)
        {
            _vm = vm;

        }
        // Event für Änderungen der Ausführbarkeit
        public event EventHandler CanExecuteChanged;

        // Commands bleiben bewusst immer aktiv.
        // Die fachliche Prüfung erfolgt in der jeweiligen ViewModel-Methode,
        // um dem Benutzer eine verständliche Statusmeldung zu geben.
        public bool CanExecute(object parameter)
        {
            // Immer erlaubt - Prüfung passiert im ViewModel
            return true;
        }

        // Führt den Command aus
        public void Execute(object parameter)
        {
            _vm.Delete(); // Geschenk löschen
        }
    }
}
