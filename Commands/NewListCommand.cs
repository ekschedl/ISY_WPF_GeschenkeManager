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
    public class NewListCommand : ICommand
    {
        private readonly GeschenkeViewModel _vm;

        public NewListCommand(GeschenkeViewModel vm)
        {
            _vm = vm;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            var result = MessageBox.Show(
                "Aktuelle Geschenke wirklich verwerfen?",
                "Neue Geschenke-Zentrale",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _vm.NewList();   // 👉 DAS ist korrekt
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}