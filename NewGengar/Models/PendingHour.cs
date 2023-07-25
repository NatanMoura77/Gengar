using NewGengar.Enums;
using NewGengar.Extensions;
using Newtonsoft.Json;

namespace NewGengar.Models;

public class PendingHour
{
    public int Id { get; set; }
    public int CollaboratorId { get; set; }
    public int ProjectId { get; set; }
    public DateTime TimeMark { get; set; }
    public int HourAmount { get; set; }
    public string StatusText { get; set; } = StatusHour.Pending.StatusToString();
    public StatusHour Status
    {
        get { return StatusText.ToLower().StatusToValue(); }
        set { StatusText = value.StatusToString(); }
    }
    [JsonIgnore]
    public virtual Collaborator Collaborator { get; set; }
    [JsonIgnore]
    public virtual Project Project { get; set; }
}
