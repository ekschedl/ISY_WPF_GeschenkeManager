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
    /// Interaktionslogik für GeschenkViewExam.xaml
    /// </summary>
    public partial class GeschenkViewExam : Window
    {
        public GeschenkViewExam()
        {
            InitializeComponent();
            DataContext = new GeschenkeViewModel();
        }

        // Handler für Menü "Neu" - öffnet Dialog
        private void AddGeschenkDialog_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddGeschenkDialog();
            dialog.Owner = this;
            
            if (dialog.ShowDialog() == true)
            {
                // Dialog wurde mit "Speichern" geschlossen
                // Daten im Hauptfenster neu laden
                var vm = DataContext as GeschenkeViewModel;
                vm?.LoadData();
            }
        }

        private void Startseite_Click(object sender, RoutedEventArgs e)
        {
            // vorhandenes MainWindow holen
            var main = Application.Current.MainWindow as MainWindow;

            if (main != null)
            {
                main.Show();     // wieder anzeigen
                main.Activate(); // in den Vordergrund holen
            }

            this.Close(); // GeschenkView schließen
        }


        // zeigt Info-Fenster / MessageBox
        private void Info_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "Geschenke-Zentrale 🎁\n\n" +
                "Verwalten von Geschenken.\n" +
                "Erstellen, Bearbeiten, Kopieren und Löschen möglich.",
                "Informationen",
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );
        }

        // schließt das aktuelle Fenster
        private void Close_Click(object sender, RoutedEventArgs e)
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

        private void SucheSuperWichtige_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as GeschenkeViewModel;
            vm?.SucheSuperWichtige();
        }

        private void ZeigeAlle_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as GeschenkeViewModel;
            vm?.ZeigeAlle();
        }


    }
}