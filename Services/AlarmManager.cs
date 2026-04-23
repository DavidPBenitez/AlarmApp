public class AlarmManager
{
    private readonly IAlarmSaveData _repository;
    private Timer? _checkTimer;

    public AlarmManager(IAlarmSaveData repository)
    {
        _repository = repository;
        StartBackgroundCheck();
    }

    private void StartBackgroundCheck()
    {
        _checkTimer = new Timer(CheckAlarms, null, 0, 60000);
    }

    private void CheckAlarms(object? state)
    {
        var now = DateTime.Now;
        var alarms = _repository.GetAll()
            .Where(a => a.IsActive &&
                   a.Time.Hour == now.Hour &&
                   a.Time.Minute == now.Minute)
            .ToList();

        foreach (var alarm in alarms)
        {
            TriggerAlarm(alarm);
        }
    }

    private void TriggerAlarm(Alarm alarm)
    {
        Console.Beep();
        Console.WriteLine($"\n*** ALARM: {alarm.Message} ***");

        if (!alarm.IsRepeating)
        {
            alarm.IsActive = false;
            _repository.Update(alarm);
        }
    }
}
