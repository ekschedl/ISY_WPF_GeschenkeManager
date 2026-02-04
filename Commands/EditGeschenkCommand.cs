using SchedlbergerEkaterina_WPF_.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SchedlbergerEkaterina_WPF_.Commands
{
    internal class EditGeschenkCommand : ICommand
    {
        private readonly GeschenkeViewModel _vm;

        public EditGeschenkCommand(GeschenkeViewModel vm)
        {
            _vm = vm;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _vm.EditStart();
        }
    }

}
