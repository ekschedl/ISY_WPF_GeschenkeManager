using Microsoft.Win32;
using SchedlbergerEkaterina_WPF_.ViewModels;
using System.Windows;

namespace SchedlbergerEkaterina_WPF_.Views
{
    public partial class AddGeschenkDialog : Window
    {
        public AddGeschenkDialog()
        {
            InitializeComponent();
            DataContext = new GeschenkeViewModel();
        }

        private void BildAuswaehlen_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as GeschenkeViewModel;

            OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = "Bilder (*.png;*.jpg)|*.png;*.jpg"
            };

            if (dlg.ShowDialog() == true)
            {
                vm.NeuesBild = dlg.FileName;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as GeschenkeViewModel;

            if (string.IsNullOrWhiteSpace(vm.NeuerName))
            {
                MessageBox.Show(
                    "Bitte geben Sie einen Namen für das Geschenk ein.", 
                    "Fehler", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error);
                return;
            }

            vm.Add();
            DialogResult = true;
            Close();
        }
    }
}
