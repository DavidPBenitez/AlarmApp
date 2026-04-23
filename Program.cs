using System.Globalization;

class Program
{
    static void Main(string[] args)
    {
        IAlarmSaveData repository = new AlarmSaveData();
        AlarmManager manager = new AlarmManager(repository);

        while (true)
        {
            ShowMenu();
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1": CreateAlarm(repository); break;
                case "2": ShowAlarms(repository); break;
                case "3": UpdateAlarm(repository); break;
                case "4": DeleteAlarm(repository); break;
                case "5": return;
            }
        }
    }

    static void ShowMenu()
    {
        Console.WriteLine("\n=== Alarm App ===");
        Console.WriteLine("1. Skapa alarm");
        Console.WriteLine("2. Visa alarm");
        Console.WriteLine("3. Uppdatera alarm");
        Console.WriteLine("4. Ta bort alarm");
        Console.WriteLine("5. Avsluta");
        Console.Write("Val: ");
    }

    static void CreateAlarm(IAlarmSaveData repository)
    {
        Console.Write("Tid (HH:mm): ");
        if (!DateTime.TryParseExact(Console.ReadLine(), "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var time))
        {
            Console.WriteLine("Ogiltigt format.");
            return;
        }

        Console.Write("Meddelande: ");
        var message = Console.ReadLine() ?? "";

        Console.Write("Upprepa? (j/n): ");
        var repeating = Console.ReadLine()?.Trim().ToLower() == "j";

        var alarm = new Alarm
        {
            Time = DateTime.Today.Add(time.TimeOfDay),
            Message = message,
            IsRepeating = repeating
        };

        repository.Add(alarm);
        Console.WriteLine("Alarm skapat!");
    }

    static void ShowAlarms(IAlarmSaveData repository)
    {
        var alarms = repository.GetAll();
        if (alarms.Count == 0)
        {
            Console.WriteLine("Inga alarm.");
            return;
        }

        foreach (var alarm in alarms)
        {
            var status = alarm.IsActive ? "Aktiv" : "Inaktiv";
            var repeat = alarm.IsRepeating ? "Ja" : "Nej";
            Console.WriteLine($"[{alarm.Id}] {alarm.Time:HH:mm} - {alarm.Message} | {status} | Upprepa: {repeat}");
        }
    }

    static void UpdateAlarm(IAlarmSaveData repository)
    {
        ShowAlarms(repository);
        Console.Write("Ange ID på alarm att uppdatera: ");
        if (!Guid.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Ogiltigt ID.");
            return;
        }

        var alarm = repository.GetById(id);
        if (alarm == null)
        {
            Console.WriteLine("Alarm hittades inte.");
            return;
        }

        Console.Write($"Ny tid (HH:mm) [{alarm.Time:HH:mm}]: ");
        var input = Console.ReadLine();
        if (DateTime.TryParseExact(input, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var newTime))
            alarm.Time = DateTime.Today.Add(newTime.TimeOfDay);

        Console.Write($"Nytt meddelande [{alarm.Message}]: ");
        var newMessage = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newMessage))
            alarm.Message = newMessage;

        Console.Write($"Aktiv? (j/n) [{(alarm.IsActive ? "j" : "n")}]: ");
        var activeInput = Console.ReadLine()?.Trim().ToLower();
        if (activeInput == "j") alarm.IsActive = true;
        else if (activeInput == "n") alarm.IsActive = false;

        repository.Update(alarm);
        Console.WriteLine("Alarm uppdaterat!");
    }

    static void DeleteAlarm(IAlarmSaveData repository)
    {
        ShowAlarms(repository);
        Console.Write("Ange ID på alarm att ta bort: ");
        if (!Guid.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Ogiltigt ID.");
            return;
        }

        repository.Delete(id);
        Console.WriteLine("Alarm borttaget!");
    }
}
