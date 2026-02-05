using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SchedlbergerEkaterina_WPF_.Converters
{

    // Converter wandelt den Edit-Modus (bool) in einen Button-Text um
    // true  -> Bearbeiten / Speichern
    // false -> Neues Geschenk hinzufügen
    public class EditModeToTextConverter : IValueConverter
    {
        // Wird von WPF aufgerufen, wenn ein Wert aus dem ViewModel
        // in der View angezeigt werden soll
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Prüft, ob der übergebene Wert ein bool ist
            // und ob der Edit-Modus aktiv ist
            if (value is bool isEdit && isEdit)
                // Text für Edit-Modus
                return "💾 Änderungen speichern";

            // Standard-Text, wenn kein Edit-Modus aktiv ist
            return "➕ Auf den Wunschzettel";
        }

        // Muss implementiert werden wegen IValueConverter
        // Wird hier nicht benötigt (nur Einweg-Konvertierung)
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Keine Rückkonvertierung nötig
            return Binding.DoNothing;
        }
    }
  
}
