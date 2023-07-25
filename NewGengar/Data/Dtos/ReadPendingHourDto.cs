namespace NewGengar.Data.Dtos;

public class ReadPendingHourDto
{
    public int Id { get; set; }
    public int CollaboratorId { get; set; }
    public int ProjectId { get; set; }
    public int HourAmount { get; set; }
    public DateTime TimeMark { get; set; }
    public string StatusText { get; private set; }

}
