using SchedlbergerEkaterina_WPF_.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SchedlbergerEkaterina_WPF_.Commands
{
    internal class DeleteGeschenkCommand : ICommand
    {
        private readonly GeschenkeViewModel _vm;

        public DeleteGeschenkCommand(GeschenkeViewModel vm)
        {
            _vm = vm;

        }

        public event EventHandler CanExecuteChanged;

        // Commands bleiben bewusst immer aktiv.
        // Die fachliche Prüfung erfolgt in der jeweiligen ViewModel-Methode,
        // um dem Benutzer eine verständliche Statusmeldung zu geben.
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _vm.Delete();
        }
    }
}
