using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SchedlbergerEkaterina_WPF_.Converters
{
    // Converter wandelt einen Listen-Index (0,1,2,…) in eine
    // benutzerfreundliche Nummer (1,2,3,…) um
    public class IndexToNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int index)
                return index + 1;   // +1, weil Listen bei 0 starten,
                                    // Benutzer aber bei 1 zählen
                                    // Fallback, falls kein gültiger Wert vorhanden ist
            return "";
        }

        // Rückumwandlung wird nicht benötigt,
        // da die Nummer nur zur Anzeige dient
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Absichtlich nicht implementiert
            throw new NotImplementedException();
        }
    }
}
