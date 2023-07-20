namespace DijitaruVatigoGengar.Data.Dtos;

public class ReadApprovedHourDto
{
    public int Id { get; set; }
    public int CollaboratorId { get; set; }
    public int ProjectId { get; set; }
    public int HourAmount { get; set; }
    public string StatusText { get; set; }

}
