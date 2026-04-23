using System.Text.Json;

public class AlarmSaveData : IAlarmSaveData
{
    private List<Alarm> _alarms = [];
    private readonly string _filePath = "alarms.json";
    private static readonly JsonSerializerOptions _jsonOptions = new() { WriteIndented = true };

    public AlarmSaveData()
    {
        LoadFromFile();
    }

    private void LoadFromFile()
    {
        if (File.Exists(_filePath))
        {
            var json = File.ReadAllText(_filePath);
            _alarms = JsonSerializer.Deserialize<List<Alarm>>(json) ?? [];
        }
        else
        {
            _alarms = [];
        }
    }

    public void SaveChanges()
    {
        var json = JsonSerializer.Serialize(_alarms, _jsonOptions);
        File.WriteAllText(_filePath, json);
    }

    public List<Alarm> GetAll() => _alarms;

    public Alarm? GetById(Guid id) => _alarms.FirstOrDefault(a => a.Id == id);

    public void Add(Alarm alarm)
    {
        _alarms.Add(alarm);
        SaveChanges();
    }

    public void Update(Alarm alarm)
    {
        var index = _alarms.FindIndex(a => a.Id == alarm.Id);
        if (index >= 0)
        {
            _alarms[index] = alarm;
            SaveChanges();
        }
    }

    public void Delete(Guid id)
    {
        _alarms.RemoveAll(a => a.Id == id);
        SaveChanges();
    }
}
