public interface IAlarmSaveData
{
    List<Alarm> GetAll();
    Alarm? GetById(Guid id);
    void Add(Alarm alarm);
    void Update(Alarm alarm);
    void Delete(Guid id);
    void SaveChanges();
}