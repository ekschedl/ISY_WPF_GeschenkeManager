# GeschenkeManager - Prüfungsprojekt

## Projektübersicht

Das Prüfungsprojekt beinhaltet die Erstellung einer grafischen Benutzeroberfläche mit **WPF (Windows Presentation Foundation)** und **XAML (Extended Application Markup Language)**. Die Daten, welche in der GUI (Graphical User Interface) erstellt, angezeigt, geändert und gelöscht werden können, werden über **Entity Framework (EF – 6.x)** zur Verfügung gestellt.

Im Rahmen des gesamten Projektes ist das Pattern **MVVM (Model-View-ViewModel)** zu berücksichtigen:
- **Model**: POCO-Klassen (Plain Old CLR Objects)
- **View**: XAML-Dateien
- **ViewModel**: Pro View eine Klasse mit ObservableCollection, Properties, Methoden

---

## Technologien

- **.NET Framework 4.7.2**
- **WPF (Windows Presentation Foundation)**
- **XAML (Extended Application Markup Language)**
- **Entity Framework 6.5.1**
- **MVVM Pattern**
- **C#**
- **SQL Server LocalDB**

---

## Projektstruktur

```
SchedlbergerEkaterina_WPF_/
│
├── Commands/              # ICommand-Implementierungen
│   ├── AddOrSaveCommand.cs
│   ├── CopyGeschenkCommand.cs
│   ├── DeleteGeschenkCommand.cs
│   ├── EditGeschenkCommand.cs
│   ├── NewListCommand.cs
│   ├── SearchCommand.cs
│   └── SpeichernCommand.cs
│
├── Converters/            # Value Converter für XAML
│   ├── EditModeToTextConverter.cs
│   └── IndexToNumberConverter.cs
│
├── Data/                  # Entity Framework Context und Initialisierung
│   ├── DbInitializer.cs   # Seeding der Beispieldaten beim App-Start
│   └── GeschenkContext.cs # DbContext für Entity Framework
│
├── Images/                # Bildressourcen
│   ├── bike.jpg
│   ├── geld.jpg
│   ├── katze.jpg
│   ├── kosmetik.jpg
│   └── lego.jpg
│
├── Models/                # Model-Klassen (POCO)
│   ├── Geschenk.cs        # Geschenk-Entity
│   ├── GeschenkeRepository.cs  # Repository-Pattern für CRUD-Operationen
│   └── Kind.cs            # Kind-Entity mit INotifyPropertyChanged
│
├── Properties/            # Projekt-Eigenschaften
│
├── ViewModels/            # ViewModel-Klassen
│   ├── GeschenkeViewModel.cs  # ViewModel für Geschenk-Verwaltung
│   └── KindViewModel.cs        # ViewModel für Kind-Ansicht
│
├── Views/                 # XAML-Views
│   ├── AddGeschenkDialog.xaml      # Dialog zum Hinzufügen von Geschenken
│   ├── GeschenkView.xaml           # Geschenk-Verwaltung (Freestyle)
│   ├── GeschenkViewExam.xaml       # Geschenk-Verwaltung (Exam-Version)
│   └── KindView.xaml               # Geschenke-Rechner nach Noten
│
├── App.xaml               # Application-Definition mit Styles
├── App.xaml.cs            # Application-Code-Behind (DbInitializer.Seed())
├── MainWindow.xaml        # Hauptfenster mit Navigation
├── MainWindow.xaml.cs     # MainWindow-Code-Behind
│
└── App.config             # Konfiguration (Connection String für LocalDB)


## Datenbank

### Connection String
Die Datenbank wird über **SQL Server LocalDB** bereitgestellt:


### Entity: Geschenk
- `GeschenkId` (int, Primary Key)
- `Name` (string)
- `Prioritaet` (string: "weniger wichtig", "wichtig", "super wichtig")
- `Bild` (string - Pfad zum Bild)
- `IstWichtig` (bool)
- `Preis` (double)
- `Erstellungsdatum` (DateTime)

### Seeding
Beim ersten App-Start werden automatisch 5 Beispieldaten über `DbInitializer.Seed()` in die Datenbank eingefügt:


## MVVM-Architektur
### Model
- **Geschenk.cs**: POCO-Klasse für Geschenk-Entitäten
- **Kind.cs**: Model-Klasse mit `INotifyPropertyChanged` für Datenbindung
- **GeschenkeRepository.cs**: Repository-Pattern für Datenzugriff (CRUD-Operationen)

### View
- **XAML-Dateien**: Deklarative UI-Definitionen
- **Data Binding**: Datenbindung zwischen View und ViewModel
- **Commands**: UI-Aktionen werden über Commands ausgeführt

### ViewModel
- **GeschenkeViewModel.cs**: 
  - `ObservableCollection<Geschenk>` für Geschenke-Listen
  - Properties für Formularfelder (NeuerName, NeuePrioritaet, etc.)
  - Commands für UI-Aktionen (Add, Edit, Delete, Copy, Search)
  - Implementiert `INotifyPropertyChanged`
  
- **KindViewModel.cs**:
  - Hält `Kind`-Model
  - Command für Speichern-Button


## Features

### 1. Geschenke-Verwaltung (GeschenkViewExam)
- ✅ **CRUD-Operationen**: Erstellen, Lesen, Aktualisieren, Löschen von Geschenken
- ✅ **Kopieren**: Geschenke duplizieren
- ✅ **Suche**: Nach Name suchen (Repository-Methode `SearchByName`)
- ✅ **Filter**: "Super wichtige" Geschenke anzeigen (Repository-Methode `Search` mit Predicate)
- ✅ **Automatische Auswahl**: Beim App-Start wird automatisch das erste Geschenk ausgewählt
- ✅ **StatusBar**: Zeigt Anzahl der Geschenke und Benutzer-Nachrichten
- ✅ **Menü & Toolbar**: Navigation und schnelle Aktionen

### 2. Geschenke-Rechner (KindView)
- ✅ **Noten-Rechner**: Berechnet Geschenke-Anspruch basierend auf Durchschnittsnote
- ✅ **Dynamische Hintergrundfarbe**: Ändert sich je nach Note
- ✅ **WarBrav-Bonus**: Zusätzliches Geschenk bei bravem Verhalten
- ✅ **Data Binding**: Slider und CheckBoxes sind per TwoWay-Binding verbunden

### 3. Dialog für neue Geschenke
- ✅ **Separates Fenster**: Modal-Dialog zum Hinzufügen neuer Geschenke
- ✅ **Bildauswahl**: FileDialog zum Auswählen von Bildern
- ✅ **Validierung**: Prüfung auf Pflichtfelder

---

## Repository-Pattern

Das `GeschenkeRepository` implementiert folgende CRUD-Operationen:

### CREATE
- `AddGeschenk(Geschenk geschenk)` - Fügt ein neues Geschenk hinzu

### READ
- `ReadAll()` - Liest alle Geschenke aus der Datenbank
- `SearchByName(string suchtext)` - Sucht nach Geschenken nach Name
- `Search(Func<Geschenk, bool> predicate)` - Generische Suchmethode mit beliebiger Property

### UPDATE
- `UpdateGeschenk(Geschenk geschenk)` - Aktualisiert ein bestehendes Geschenk

### DELETE
- `RemoveGeschenk(int id)` - Löscht ein Geschenk nach ID
- `DeleteAll()` - Löscht alle Geschenke

### COPY
- `Copy(Geschenk original)` - Erstellt eine Kopie eines Geschenks

---

## Commands

Alle UI-Aktionen werden über Commands ausgeführt (ICommand-Implementierungen):

- **AddOrSaveCommand**: Fügt neues Geschenk hinzu oder speichert Änderungen
- **DeleteGeschenkCommand**: Löscht ausgewähltes Geschenk
- **CopyGeschenkCommand**: Kopiert ausgewähltes Geschenk
- **EditGeschenkCommand**: Startet Bearbeitungsmodus
- **SearchCommand**: Führt Suche aus
- **NewListCommand**: Löscht alle Geschenke und startet neue Liste
- **SpeichernCommand**: Speichert Kind-Daten (mit Validierung)

---

## Wichtige Design-Entscheidungen

### 1. Repository-Pattern
**Entscheidung**: Zentralisierter Datenzugriff über `GeschenkeRepository` statt direkter EF-Zugriffe im ViewModel.

**Begründung**: 
- Saubere Trennung von Datenzugriff und Business-Logik
- Einfache Testbarkeit
- Wiederverwendbarkeit

### 2. Zwei Suchmethoden im Repository
**Entscheidung**: 
- `SearchByName(string suchtext)` - Spezifische Suche nach Name
- `Search(Func<Geschenk, bool> predicate)` - Generische Suche mit beliebiger Property

**Begründung**: 
- zum Üben
- Erfüllt Anforderung "beliebige Property als Argument"
- Bietet einfache und flexible Suchmöglichkeiten

### 3. ObservableCollection statt List
**Entscheidung**: Verwendung von `ObservableCollection<Geschenk>` für Geschenke-Listen.

**Begründung**: 
- Automatische UI-Updates bei Änderungen
- MVVM-Standard für Collections
- WPF Data Binding funktioniert nahtlos

### 4. DbInitializer für Seeding
**Entscheidung**: Separate `DbInitializer`-Klasse für Seeding beim App-Start.

**Begründung**: 
- Saubere Trennung von Initialisierung und Business-Logik
- Wird nur einmalig ausgeführt (Prüfung auf vorhandene Daten)
- Erfüllt Anforderung "einmalig beim App-Start"

### 5. Commands statt Event Handlers
**Entscheidung**: Verwendung von ICommand-Implementierungen statt direkter Event Handler.

**Begründung**: 
- MVVM-konform
- Entkopplung von View und ViewModel
- Testbarkeit der Logik ohne UI

### 6. Automatische Auswahl des ersten Geschenks
**Entscheidung**: Beim App-Start wird automatisch das erste Geschenk ausgewählt.

**Begründung**: 
- Bessere User Experience
- Details werden sofort angezeigt
- Keine leere Ansicht beim Start

### 7. Separate Dialog für "Neu" (Menü) vs. Formular (Toolbar)
**Entscheidung**: 
- Menü "Neu" öffnet separaten Dialog (`AddGeschenkDialog`)
- Toolbar "➕ Neu" Dialog (`AddGeschenkDialog`) 
es gibt noch button zum hinzufügen/ändeern verwendet Formular im Hauptfenster

**Begründung**: 
- Flexibilität für verschiedene Anwendungsfälle
- Dialog für fokussierte Eingabe
- Formular für schnelle Eingabe ohne zusätzliches Fenster
- zum Üben

---

## Ausführung

### Voraussetzungen
- Visual Studio 2017 oder höher
- .NET Framework 4.7.2
- SQL Server LocalDB (wird mit Visual Studio installiert)

### Start
1. Projekt in Visual Studio öffnen
2. NuGet-Pakete wiederherstellen (Entity Framework 6.5.1)
3. Projekt kompilieren (F6)
4. Projekt ausführen (F5)

### Datenbank
Die Datenbank wird automatisch beim ersten Start erstellt. Beispieldaten werden über `DbInitializer.Seed()` eingefügt.

---

## Autorin

**Ekaterina Schedlberger**

Prüfungsprojekt für: Interaktive Systeme
Technologie: WPF • MVVM • Entity Framework
