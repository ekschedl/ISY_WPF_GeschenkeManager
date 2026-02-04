using SchedlbergerEkaterina_WPF_.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SchedlbergerEkaterina_WPF_
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        // Die Initialisierung der Beispieldaten erfolgt einmalig beim App-Start
        // über eine separate DbInitializer-Klasse, um eine saubere Trennung
        // zwischen technischer Initialisierung und Benutzeraktionen einzuhalten.
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            DbInitializer.Seed();
        }
    }
}
