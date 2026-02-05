using SchedlbergerEkaterina_WPF_.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SchedlbergerEkaterina_WPF_.Commands
{
    // Command zum Bearbeiten eines Geschenks
    internal class EditGeschenkCommand : ICommand
    {
        // ViewModel-Referenz
        private readonly GeschenkeViewModel _vm;

        // Konstruktor
        public EditGeschenkCommand(GeschenkeViewModel vm)
        {
            // ViewModel speichern
            _vm = vm;
        }

        // Event für Änderungen der Ausführbarkeit
        public event EventHandler CanExecuteChanged;

        // Prüft, ob Command ausgeführt werden kann
        public bool CanExecute(object parameter)
        {
            // Immer erlaubt
            return true;
        }

        // Führt den Command aus
        public void Execute(object parameter)
        {
            // Bearbeitungsmodus starten
            _vm.EditStart();
        }

    }
}