using SchedlbergerEkaterina_WPF_.Commands;
using SchedlbergerEkaterina_WPF_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SchedlbergerEkaterina_WPF_.ViewModels
{
    internal class KindViewModel
    {
        // ViewModel für die Kind-Ansicht
        // Verbindet View (XAML) mit Model (Kind)
        public Kind Kind { get; }


        public GeschenkeRepository GeschenkeRepo { get; }


        // Command für den Speichern-Button
        // Wird im XAML an Button.Command gebunden
        public ICommand SpeichernCommand { get; }

        public KindViewModel()
        {
            // Erzeugt ein neues Kind-Model
            Kind = new Kind
            {
                Vorname = "Irina",
                Durchschnittsnote = 1
            };

            // Erstellt den Command
            // Übergibt das Model an den Command
            SpeichernCommand = new SpeichernCommand(Kind);
            GeschenkeRepo = new GeschenkeRepository();


        }

    }
}

/*
 * 
ViewModel hält Model + Commands

View bindet an ViewModel (kein Code-Behind nötig)

Command enthält die Logik für Button-Aktionen

Model enthält die Daten + Fachlogik

*/