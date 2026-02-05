using SchedlbergerEkaterina_WPF_.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SchedlbergerEkaterina_WPF_.Commands
{

    //Command - entweder "Hinzufügen" oder "Speichern"
    // internal heißt, dass nur andere Klassen in der gleichen Assembly darauf zugreifen können
    internal class AddOrSaveCommand : ICommand
{
    // Hier speichern wir das ViewModel, damit wir später darauf zugreifen können
    // readonly bedeutet, dass wir es nur einmal setzen können (im Konstruktor)
    private readonly GeschenkeViewModel _vm;

    // Beim Erstellen der Klasse müssen wir ein ViewModel mitgeben
    public AddOrSaveCommand(GeschenkeViewModel vm)
    {
        // Das übergebene ViewModel speichern wir uns
        _vm = vm;
    }

    // Dieses Event wird ausgelöst, wenn sich ändert, ob der Button klickbar sein soll oder nicht
    // WPF nutzt das, um Buttons automatisch zu aktivieren/deaktivieren
    public event EventHandler CanExecuteChanged;

    // Hier wird geprüft, ob der Command gerade ausgeführt werden kann
    // parameter könnte z.B. ein Objekt sein, das vom Button mitgegeben wird
    public bool CanExecute(object parameter)
    {
        // Im Moment sagen wir einfach immer "ja, kann ausgeführt werden"
        // Später könnte man hier prüfen, ob z.B. alle Felder ausgefüllt sind
        return true; // optional: Validierung einbauen
    }

    // Das ist die Methode, die wirklich ausgeführt wird, wenn der Button geklickt wird
    // parameter könnte wieder was vom Button sein, wird hier aber nicht genutzt
    public void Execute(object parameter)
    {
        // sind wir gerade am Bearbeiten?
        if (_vm.IstEditModus)
            // Ja? Dann speichern wir die Änderungen
            _vm.SaveEdit();
        else
            // Nein? Dann fügen wir einen neuen Eintrag hinzu
            _vm.Add();
    }
}
}