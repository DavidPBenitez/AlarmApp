public class Alarm
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime Time { get; set; }
    public string Message { get; set; } = "";
    public bool IsActive { get; set; } = true;
    public bool IsRepeating { get; set; } = false;
    public List<DayOfWeek> RepeatDays { get; set; } = new();
}