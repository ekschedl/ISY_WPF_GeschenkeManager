using SchedlbergerEkaterina_WPF_.Views;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SchedlbergerEkaterina_WPF_
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenKindView_Click(object sender, RoutedEventArgs e)
        {
            KindView view = new KindView();
            view.ShowDialog();
        }

        private void OpenGeschenkView_Click(object sender, RoutedEventArgs e)
        {
            GeschenkView view = new GeschenkView();
            view.ShowDialog();
        }

        private void OpenGeschenkViewExam_Click(object sender, RoutedEventArgs e)
        {
            GeschenkViewExam view = new GeschenkViewExam();
            view.ShowDialog();
        }
        private void Beenden_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "GeschenkeManager\n\n" +
                "Prüfungsprojekt Interaktive Systeme\n" +
                "WPF • MVVM • Entity Framework\n\n" +
                "Autorin: Ekaterina Schedlberger",
                "Über die Anwendung",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

    }
}
