
Autorin:
Ekaterina Schedlberger
Prüfungsprojekt für: Interaktive Systeme
Technologie: WPF • MVVM • Entity Framework

?Projektübersicht

Das Prüfungsprojekt beinhaltet die Erstellung einer grafischen Benutzeroberfläche mit **WPF (Windows Presentation Foundation)** und **XAML (Extended Application Markup Language)**. Die Daten, welche in der GUI (Graphical User Interface) erstellt, angezeigt, geändert und gelöscht werden können, werden über **Entity Framework (EF – 6.x)** zur Verfügung gestellt.

Im Rahmen des gesamten Projektes ist das Pattern **MVVM (Model-View-ViewModel)** zu berücksichtigen:
- **Model**: POCO-Klassen (Plain Old CLR Objects)
- **View**: XAML-Dateien
- **ViewModel**: Pro View eine Klasse mit ObservableCollection, Properties, Methoden

-------------------------------------------------------------

? Technologien

- **.NET Framework 4.7.2**
- **WPF (Windows Presentation Foundation)**
- **XAML (Extended Application Markup Language)**
- **Entity Framework 6.5.1**
- **MVVM Pattern**
- **C#**
- **SQL Server LocalDB**

-------------------------------------------------------------

? Projektstruktur

SchedlbergerEkaterina_WPF_/
?
??? Commands/              # ICommand-Implementierungen
?   ??? AddOrSaveCommand.cs
?   ??? CopyGeschenkCommand.cs
?   ??? DeleteGeschenkCommand.cs
?   ??? EditGeschenkCommand.cs
?   ??? NewListCommand.cs
?   ??? SearchCommand.cs
?   ??? SpeichernCommand.cs
?
??? Converters/            # Value Converter für XAML
?   ??? EditModeToTextConverter.cs
?   ??? IndexToNumberConverter.cs
?
??? Data/                  # Entity Framework Context und Initialisierung
?   ??? DbInitializer.cs   # Seeding der Beispieldaten beim App-Start
?   ??? GeschenkContext.cs # DbContext für Entity Framework
?
??? Images/                # Bildressourcen
?   ??? bike.jpg
?   ??? geld.jpg
?   ??? katze.jpg
?   ??? kosmetik.jpg
?   ??? lego.jpg
?
??? Models/                # Model-Klassen (POCO)
?   ??? Geschenk.cs        # Geschenk-Entity
?   ??? GeschenkeRepository.cs  # Repository-Pattern für CRUD-Operationen
?   ??? Kind.cs            # Kind-Entity mit INotifyPropertyChanged
?
??? Properties/            # Projekt-Eigenschaften
?
??? ViewModels/            # ViewModel-Klassen
?   ??? GeschenkeViewModel.cs  # ViewModel für Geschenk-Verwaltung
?   ??? KindViewModel.cs        # ViewModel für Kind-Ansicht
?
??? Views/                 # XAML-Views
?   ??? AddGeschenkDialog.xaml      # Dialog zum Hinzufügen von Geschenken
?   ??? GeschenkView.xaml           # Geschenk-Verwaltung (Freestyle, zum Üben, experementieren)
?   ??? GeschenkViewExam.xaml       # Geschenk-Verwaltung (Exam-Version)
?   ??? KindView.xaml               # Geschenke-Rechner nach Noten
?
??? App.xaml               # Application-Definition mit Styles
??? App.xaml.cs            # Application-Code-Behind (DbInitializer.Seed())
??? MainWindow.xaml        # Hauptfenster mit Navigation
??? MainWindow.xaml.cs     # MainWindow-Code-Behind
?
??? App.config             # Konfiguration (Connection String für LocalDB)
-------------------------------------------------------------
**Workflow: Button-Klick bis Datenbank und zurück**

Beispiel: Benutzer klickt auf Löschen-Button

1. Button-Klick im View (GeschenkViewExam.xaml)
   - Button hat Command-Binding: Command="{Binding DeleteCommand}"
   - WPF ruft automatisch Command.Execute auf

2. Command wird ausgeführt (Commands/DeleteGeschenkCommand.cs)
   - DeleteGeschenkCommand.Execute wird aufgerufen
   - Command ruft ViewModel-Methode auf: _vm.Delete()

3. ViewModel führt Logik aus (ViewModels/GeschenkeViewModel.cs)
   - Delete() prüft ob Geschenk ausgewählt ist
   - Ruft Repository-Methode auf: _rep.RemoveGeschenk(AusgewGeschenk.GeschenkId)
   - Ruft LoadData() auf um Daten neu zu laden

4. Repository greift auf Datenbank zu (Models/GeschenkeRepository.cs)
   - RemoveGeschenk verwendet DbContext
   - Findet Geschenk in Datenbank: _context.Geschenke.FirstOrDefault()
   - Entfernt Geschenk: _context.Geschenke.Remove(geschenk)
   - Speichert Änderungen: _context.SaveChanges()

5. DbContext kommuniziert mit Datenbank (Data/GeschenkContext.cs)
   - Entity Framework übersetzt C#-Code in SQL
   - Führt DELETE-Befehl in SQL Server LocalDB aus
   - Daten werden aus Datenbank gelöscht

6. ViewModel aktualisiert ObservableCollection
   - LoadData() ruft Repository.ReadAll() auf
   - Lädt alle Geschenke neu aus Datenbank
   - MeineGeschenke.Clear() und MeineGeschenke.Add() für jedes Geschenk
   - ObservableCollection löst automatisch CollectionChanged Event aus

7. UI wird automatisch aktualisiert (durch DataBinding)
   - WPF hört auf CollectionChanged Event
   - ListBox.ItemsSource ist an MeineGeschenkeFiltered gebunden
   - ListBox zeigt automatisch aktualisierte Liste an
   - Gelöschtes Geschenk verschwindet aus der Anzeige

8. StatusBar zeigt Feedback
   - UserMessage wird gesetzt: "??? Geschenk wurde gelöscht"
   - StatusBar ist an UserMessage gebunden
   - Nachricht wird automatisch angezeigt

**Zwei Varianten im Projekt:**

Variante 1 - Command-Binding (MVVM-konform):
- Button Command="{Binding DeleteCommand}"
- Kein Code-Behind nötig
- Logik komplett im ViewModel
- Beispiel: Löschen, Kopieren, Bearbeiten Buttons

Variante 2 - Click-Event-Handler (Code-Behind):
- Button Click="AddGeschenkDialog_Click"
- Handler in GeschenkViewExam.xaml.cs
- Wird verwendet für Dialog-Öffnung, Datei-Auswahl
- Beispiel: Neu-Button öffnet Dialog, Bild-Auswahl 

-------------------------------------------------------------

? Aufgabe 1 – WPF GUI 

Menüleiste
- Datei: Startseite, Neue Geschenke-Zentrale, Informationen, Beenden
- Bearbeiten: Neu, Ändern, Kopieren, Löschen
- Implementiert in: GeschenkViewExam.xaml

Symbolleiste (ToolBar)
- Schnellzugriff auf häufig genutzte Funktionen: Neu, Kopieren, Löschen, Beenden
- Implementiert in: GeschenkViewExam.xaml

StatusBar
- Zeigt die Anzahl der Geschenke (AnzahlGeschenke)
- Zeigt Benutzer-Nachrichten (UserMessage) für Feedback bei Aktionen
- Implementiert in: GeschenkViewExam.xaml
- Datenbindung an GeschenkeViewModel Properties

Bedienung
- Navigation: Über Menü oder Toolbar zu verschiedenen Funktionen
- Auswahl: Klick auf Geschenk in der Liste zeigt Details rechts an
- Eingabe: Formularfelder für neue/bearbeitete Geschenke
- Feedback: StatusBar informiert über aktuelle Aktionen und Anzahl

-------------------------------------------------------------

? Aufgabe 2 – Entity & DbContext

Model-Klasse: Geschenk.cs, Properties/Eigenschaften implementiert:
- GeschenkId: int (Primary Key) -Entity Framework erkennt GeschenkId automatisch als Primary Key
- Name: string
- Prioritaet: string
- Bild: string
- IstWichtig: bool
- Preis: double
- Erstellungsdatum: DateTime

Datentypen: 5 unterschiedliche Datentypen 
- int: GeschenkId
- string: Name, Prioritaet, Bild
- double: Preis
- bool: IstWichtig
- DateTime: Erstellungsdatum

DbContext-Klasse: GeschenkContext.cs
- Klasse erbt von DbContext (Entity Framework)
- Verwendet NuGet-Paket Entity Framework 6.5.1
- Enthält DbSet Geschenke vom Typ Geschenk
- DbSet ermöglicht Datenbankzugriff auf Geschenk-Entitäten

Default-Werte: DbInitializer.cs
- Erzeugt beim App-Start automatisch 5 Beispieldaten über Seed-Methode
- Wird in App.xaml.cs OnStartup aufgerufen

-------------------------------------------------------------

? Aufgabe 3 – Repository / CRUD

Repository-Klasse: GeschenkeRepository.cs
Repository = CRUD + EF + SaveChanges (Create, Read, Update, Delete, Copy, Search)

Methoden:
- AddGeschenk - CREATE
- ReadAll - READ
- UpdateGeschenk - UPDATE
- RemoveGeschenk - DELETE (einzelnes Geschenk)
- DeleteAll - DELETE ALL (alle Geschenke)
- Copy - Kopiert Geschenk mit Kopie im Namen
- SearchByName - Sucht nach Name
- Search mit Predicate - Generische Suche mit beliebiger Property

Getestet: Über GeschenkeViewModel und UI in GeschenkViewExam

-------------------------------------------------------------

? Aufgabe 4 – ViewModel

ViewModel-Klasse: GeschenkeViewModel.cs

ObservableCollection: sorgt für automatische UI-Updates bei Änderungen
- MeineGeschenke: Enthält alle Geschenke aus Datenbank 
- MeineGeschenkeFiltered: Enthält gefilterte Geschenke für Anzeige 

SelectedItem-Property:
- AusgewGeschenk: Ausgewähltes Geschenk in der Liste, Typ Geschenk
- Implementiert PropertyChanged-Notification über OnPropertyChanged
- Wird an ListBox.SelectedItem gebunden
- Bei Änderung wird Detailansicht automatisch aktualisiert

Properties für die View:
- Formularfelder: NeuerName, NeuePrioritaet, NeuesBild, IstWichtig, Preis
- Suchtext: Suchtext mit automatischem Suchen bei Änderung
- Status: UserMessage, AnzahlGeschenke als berechnete Property
- Modus: IstEditModus, true bedeutet bearbeiten, false bedeutet neu hinzufügen
- Alle Properties implementieren PropertyChanged-Notification

Methoden für die View:
- LoadData: Lädt alle Geschenke aus Repository
- Add: Fügt neues Geschenk hinzu
- Delete: Löscht ausgewähltes Geschenk
- Copy: Kopiert ausgewähltes Geschenk
- EditStart: Startet Bearbeitungsmodus
- SaveEdit: Speichert Änderungen
- Suchen: Filtert Geschenke nach Suchtext
- SucheSuperWichtige: Filtert nach Prioritaet
- ZeigeAlle: Zeigt alle Geschenke wieder an
- NewList: Löscht alle Geschenke
- ClearForm: Leert Formularfelder

Commands für UI-Aktionen MVVM-Pattern:
- DeleteCommand, CopyCommand, EditCommand, AddOrSaveCommand, SearchCommand, NewListCommand
- Commands werden im Konstruktor initialisiert
- Alle Commands implementieren ICommand-Interface
- Entkoppeln View von ViewModel-Logik

MVVM-Pattern Umsetzung:
- INotifyPropertyChanged: Klasse implementiert Interface
- PropertyChanged Event vorhanden
- OnPropertyChanged Methode vorhanden
- View bindet an ViewModel-Properties, kein Code-Behind für Logik
- ViewModel ruft Repository-Methoden auf, Trennung von Datenzugriff
- Commands für UI-Aktionen statt Event-Handler im Code-Behind

-------------------------------------------------------------

? Aufgabe 5 – Itemscontrol & Details 

Implementiert in: GeschenkViewExam.xaml (nicht MainWindow)

ItemsControl zur Anzeige der Liste:
- ListBox in GeschenkViewExam.xaml
- ItemsSource gebunden an MeineGeschenkeFiltered
- SelectedItem gebunden an AusgewGeschenk
- ItemTemplate zeigt Name und Prioritaet für jedes Geschenk

Detailanzeige bei Auswahl:
- Wenn Eintrag in ListBox ausgewählt wird, wird AusgewGeschenk gesetzt
- Details werden rechts in GroupBox angezeigt
- Zeigt Name, ID, Datum, IstWichtig, Preis, Bild
- Buttons für Aktionen: Kopieren, Bearbeiten, Löschen

StatusBar mit sinnvollen Informationen:
- Zeigt AnzahlGeschenke: Geschenke total: X
- Zeigt UserMessage: Feedback bei Aktionen wie Hinzufügen, Löschen, Kopieren
- Beide per DataBinding an ViewModel-Properties gebunden

Verbindungen zwischen Klassen:

Repository zu DbContext:
- GeschenkeRepository.cs Zeile 17: _context = new GeschenkContext06()
  Repository erstellt DbContext im Konstruktor für Datenbankzugriff
- Repository verwendet DbSet Geschenke für Datenbankzugriff
- SaveChanges speichert Änderungen in der Datenbank

ViewModel zu Repository:
- GeschenkeViewModel.cs Zeile 23: private readonly GeschenkeRepository _rep = new GeschenkeRepository()
  Repository wird als privates Feld im ViewModel erstellt, kann in allen ViewModel-Methoden verwendet werden
- ViewModel ruft Repository-Methoden auf: ReadAll, AddGeschenk, RemoveGeschenk, UpdateGeschenk, Copy, SearchByName
- Daten werden in ObservableCollection gespeichert

View zu ViewModel:
- GeschenkViewExam.xaml.cs setzt DataContext = new GeschenkeViewModel()
- XAML bindet Controls an ViewModel-Properties: {Binding MeineGeschenkeFiltered}, {Binding AusgewGeschenk}, {Binding AnzahlGeschenke}
- UI aktualisiert sich automatisch bei PropertyChanged Events

-------------------------------------------------------------

? Aufgabe 6 – Neues Objekt anlegen

Dialogfenster zum Anlegen eines neuen Objekts:
- AddGeschenkDialog.xaml: Separates Window als Modal-Dialog
- Enthält Formularfelder für Name, Prioritaet, IstWichtig, Preis, Bild
- Dialog wird über AddGeschenkDialog_Click geöffnet

Öffnen über Menüpunkt Neu:
- GeschenkViewExam.xaml : 
MenuItem Header="_Neu" Click="AddGeschenkDialog_Click"
- GeschenkViewExam.xaml.cs: AddGeschenkDialog_Click öffnet den Dialog

Öffnen über Toolbar-Button Neu:
- GeschenkViewExam.xaml:
Button Content="+ Neu" Click="AddGeschenkDialog_Click"
- Ruft dieselbe Methode wie Menüpunkt auf

Speichern fügt Objekt über Repository / ViewModel hinzu:
- AddGeschenkDialog.xaml.cs Save_Click: ruft vm.Add() auf
- GeschenkeViewModel.Add(): erstellt neues Geschenk-Objekt
- GeschenkeViewModel.Add(): ruft _rep.AddGeschenk auf
- GeschenkeRepository.AddGeschenk(): fügt Objekt in Datenbank ein via DbContext
- Nach Dialog-Schließen: GeschenkViewExam.xaml.cs ruft vm.LoadData() auf

Neues Objekt erscheint im ItemsControl:
- LoadData() lädt alle Geschenke neu aus Repository
- MeineGeschenkeFiltered wird aktualisiert
- ListBox ItemsSource ist an MeineGeschenkeFiltered gebunden
- Neues Geschenk erscheint automatisch in der Liste

-------------------------------------------------------------

? Aufgabe 7 – Suchen

Suchfunktion in der GUI vorhanden:
- TextBox in GeschenkViewExam.xaml
- Text gebunden an Suchtext mit UpdateSourceTrigger PropertyChanged
- Bei jeder Eingabe wird automatisch gesucht

Filterung der Objekte möglich:
- GeschenkeViewModel.Suchen ruft Repository.SearchByName auf
- Aktualisiert MeineGeschenkeFiltered
- ListBox zeigt nur gefilterte Geschenke an

Umsetzung der Suchfunktion:
- TextBox-Eingabe wird an ViewModel-Property Suchtext gebunden
- Bei Änderung wird Suchen automatisch aufgerufen
- ViewModel ruft Repository-Methode auf
- Repository filtert Daten aus Datenbank
- Gefilterte Liste wird in MeineGeschenkeFiltered gespeichert
- UI aktualisiert sich automatisch durch DataBinding

Zwei Suchmethoden im Repository:

SearchByName: Sucht spezifisch nach Name-Property
- Einfache Implementierung für TextBox-Suche
- Verwendet in ViewModel.Suchen für normale Suche

Search mit Predicate: Generische Suche mit beliebiger Property
- Flexibel: kann nach jeder Property suchen
- Verwendet Lambda-Ausdruck: g => g.Prioritaet == "super wichtig"
- Verwendet für Button Super wichtige Geschenke

Warum zwei Suchmethoden:
- Demonstriert verschiedene Suchansätze
- Zeigt Lambda-Ausdrücke und Predicates in der Praxis
- zum Üben

-------------------------------------------------------------

? Aufgabe 8 – Löschen


DeleteGeschenkCommand.cs ruft GeschenkeViewModel.Delete auf

Repository:
- GeschenkeRepository.RemoveGeschenk löscht einzelnes Geschenk aus Datenbank
- GeschenkeRepository.DeleteAll löscht alle Geschenke (wird von NewListCommand verwendet)

UI-Update:
- Nach LoadData wird MeineGeschenkeFiltered aktualisiert
- Objekt verschwindet aus Liste

Verfügbar:
- Über Menü Bearbeiten Löschen (einzelnes Geschenk)
- Toolbar-Button Löschen (einzelnes Geschenk)
- Button in Detailansicht (einzelnes Geschenk)
- Menü Datei ? Neue Geschenke-Zentrale (löscht alle via DeleteAll)

-------------------------------------------------------------

? Aufgabe 9 – Kopieren

- CopyGeschenkCommand.cs ruft GeschenkeViewModel.Copy auf
- Verfügbar über Menü Bearbeiten, Toolbar-Button und Button in Detailansicht

Neues Objekt wird aus bestehendem erzeugt:
- GeschenkeViewModel.Copy prüft ob Geschenk ausgewählt ist
- Ruft Repository.Copy auf und übergibt ausgewähltes Geschenk
- Repository erstellt neues Geschenk-Objekt mit kopierten Properties
- Name wird erweitert: original.Name + " (Kopie)"
- Neues Erstellungsdatum wird gesetzt: DateTime.Now
- GeschenkId wird nicht kopiert, wird automatisch von Entity Framework vergeben

Umsetzung der Kopierfunktion:
- Repository.Copy erstellt Kopie mit allen Properties außer GeschenkId
- Repository speichert Kopie in Datenbank via AddGeschenk
- ViewModel lädt Daten neu mit LoadData
- Kopie wird automatisch in ListBox angezeigt
- Kopie wird automatisch als ausgewähltes Geschenk gesetzt
- StatusBar zeigt Bestätigungsmeldung

-------------------------------------------------------------

? Aufgabe 10 – Tooltip & weitere Controls

Tooltip:
- TextBox in GeschenkViewExam.xaml hat ToolTip
- Text: Suche nach Geschenken
- MainWIndow hat ToolTip für Expander

CheckBox:
-  AddGeschenkDialog.xaml
-  GeschenkViewExam.xaml
-  MainWindow.xaml

GroupBox:
- Zwei GroupBox-Controls in GeschenkViewExam.xaml
- Eine für Ausgewähltes Geschenk
- Eine für Neues Geschenk

Slider:
- In KindView.xaml für Durchschnittsnote
- Minimum 1, Maximum 5

Expander:
- MainWindow.xaml: Was passiert mit meinen Daten

-------------------------------------------------------------

? Aufgabe 11 – Styles

Styles definiert: App.xaml
- Application.Resources enthält mehrere Button-Styles

Button-Styles:
- BaseButtonStyle - Basis-Style für alle Buttons
- DefaultButtonStyle - Grauer Gradient
- PrimaryButtonStyle - Grüner Gradient
- SecondaryButtonStyle - Blauer Gradient
- DangerButtonStyle - Roter Gradient

Menu-Styles:
- Style für Menu und MenuItem
- Grünes Farbschema

Verwendung:
- Buttons in Views verwenden StaticResource PrimaryButtonStyle

-------------------------------------------------------------

? Wichtige Design-Entscheidungen


Repository-Pattern: Zentralisierter Datenzugriff über GeschenkeRepository
- Trennung von Datenzugriff und Business-Logik
- Einfache Testbarkeit und Wiederverwendbarkeit

Zwei Suchmethoden: SearchByName und Search mit Predicate
- Demonstriert verschiedene Suchansätze
- Erfüllt Anforderung beliebige Property als Argument

ObservableCollection statt List: Automatische UI-Updates bei Änderungen
- MVVM-Standard für Collections
- WPF Data Binding funktioniert nahtlos

DbInitializer für Seeding: Separate Klasse für Beispieldaten
- Wird einmalig beim App-Start ausgeführt
- Prüft ob Daten bereits vorhanden sind

Commands statt Event Handlers: MVVM-konform
- Entkopplung von View und ViewModel
- Testbarkeit der Logik ohne UI

Automatische Auswahl: Beim Start wird erstes Geschenk ausgewählt
- Bessere User Experience
- Details werden sofort angezeigt

Zwei Varianten zum Hinzufügen: Dialog und Inline-Formular
- Demonstriert verschiedene WPF-Patterns
- Flexibilität für verschiedene Anwendungsfälle


