using SchedlbergerEkaterina_WPF_.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SchedlbergerEkaterina_WPF_.Commands
{
    internal class AddOrSaveCommand : ICommand
    {
        private readonly GeschenkeViewModel _vm;

        public AddOrSaveCommand(GeschenkeViewModel vm)
        {
            _vm = vm;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true; // optional: Validierung einbauen
        }

        public void Execute(object parameter)
        {
            if (_vm.IstEditModus)
                _vm.SaveEdit();
            else
                _vm.Add();
        }
    }
}
