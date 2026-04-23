# AlarmApp

En konsolbaserad alarm-applikation byggd i C# för att träna på CRUD och god arkitektur.

## Syfte

Detta projekt demonstrerar:

CRUD-implementering (Create, Read, Update, Delete)

Interface-baserad design.

Persistent storage med JSON.

## Filstruktur / Vad gör vad?

```
Program.cs     # Startpunkt. Hanterar menyn och användarinput.

Models
  Alarm.cs     # Datamodell för ett alarm (tid, meddelande och upprepning)

Interfaces
  IAlarmSaveData.cs     # Interface som sätter krav vad en lagringstjänst måste kunna göra.

Services
  AlarmSaveData.cs     # Sparar och läser alarm från en JSON-fil (alarms.json).
  AlarmManager.cs     # Kör i bakgrunden och utlöser alarm när klockan stämmer.
```
## Hur kan du starta/prova appen?
1. **Klona projektet:**
```bash
   git clone https://github.com/DavidPBenitez/AlarmApp.git
   cd AlarmApp
```

2. **Kör applikationen:**
```bash
   dotnet run
```

3. **Alternativt i VS Code:**
   - Öppna mappen i VS Code
   - Tryck `F5` för att starta med debugger
