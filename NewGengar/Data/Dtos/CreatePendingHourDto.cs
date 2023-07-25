namespace NewGengar.Data.Dtos;

public class CreatePendingHourDto
{
    public int CollaboratorId { get; set; }
    public int ProjectId { get; set; }
    public int HourAmount { get; set; }
    public DateTime TimeMark { get; set; }

}
