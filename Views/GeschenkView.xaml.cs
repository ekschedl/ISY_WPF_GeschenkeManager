using Microsoft.Win32;
using SchedlbergerEkaterina_WPF_.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SchedlbergerEkaterina_WPF_.Views
{
    /// <summary>
    /// Interaktionslogik für GeschenkView.xaml
    /// </summary>
    public partial class GeschenkView : Window
    {
        public GeschenkView()
        {
            InitializeComponent();
            DataContext = new GeschenkeViewModel();

        }



        private void Beenden_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BildAuswaehlen_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as GeschenkeViewModel;

            OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = "Bilder (*.png;*.jpg)|*.png;*.jpg"
            };

            if (dlg.ShowDialog() == true)
                vm.NeuesBild = dlg.FileName;
        }


    }
}
