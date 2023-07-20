using DijitaruVatigoGengar.Enums;
using DijitaruVatigoGengar.Extensions;

namespace DijitaruVatigoGengar.Models;

public class PendingHour
{
    public int Id { get; set; }
    public int CollaboratorId { get; set; }
    public int ProjectId { get; set; }
    public DateTime RegisterHour { get; set; }
    public int HourAmount { get; set; }
    private string statusText;
    public string StatusText
    {
        get { return statusText; }
        set { statusText = value?.ToLower(); }
    }
    public StatusHour Status
    {
        get { return StatusText.StatusToValue(); }
        set { StatusText = value.StatusToString(); }
    }

    public Collaborator Collaborator { get; set; }
    public Project Project { get; set; }

    public PendingHour()
    {
        StatusText = StatusHour.Pending.StatusToString();
    }
}
