using SchedlbergerEkaterina_WPF_.Commands;
using SchedlbergerEkaterina_WPF_.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SchedlbergerEkaterina_WPF_.Commands
{
    // Command zum Kopieren eines Geschenks
    internal class CopyGeschenkCommand : ICommand
    {
        // ViewModel-Referenz
        private readonly GeschenkeViewModel _vm;

        public CopyGeschenkCommand(GeschenkeViewModel vm)
        {
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
            // Geschenk kopieren
            _vm.Copy();
        }
    }
}



