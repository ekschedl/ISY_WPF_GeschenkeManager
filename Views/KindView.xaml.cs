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
    /// Interaktionslogik für KindView.xaml
    /// </summary>
    public partial class KindView : Window
    {

        public KindView()
        {
            InitializeComponent();


            DataContext = new KindViewModel();


        }

        private void Beenden_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // Die UI reagiert ausschließlich über DataBinding.
        // Der Button-Click dient nur zur Demonstration einer möglichen Aktion
        // z. B. Absenden der Daten, nicht zur Fachlogik.



    }
}