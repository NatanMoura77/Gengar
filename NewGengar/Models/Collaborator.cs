using NewGengar.Enums;
using NewGengar.Extensions;
using Newtonsoft.Json;

namespace NewGengar.Models;

public class Collaborator
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }

    public string GenderText { get; set; }
    public Gender Gender
    {
        get { return GenderText.ToLower().GenderToValue(); }
        set { GenderText = value.GenderToString(); }
    }

    public string RoleText { get; set; }
    public Role CollaboratorRole
    {
        get { return RoleText.ToLower().RoleToValue(); }
        set { RoleText = value.RoleToString(); }
    }

    public string ModalityText { get; set; }
    public ContractModality Modality
    {
        get { return ModalityText.ToLower().ModalityToValue(); }
        set { ModalityText = value.ModalityToString(); }
    }
    [JsonIgnore]
    public virtual IList<Project> Projects { get; set; }
    [JsonIgnore]
    public virtual IList<Project> ApproversProject { get; set; }
    [JsonIgnore]
    public virtual IList<PendingHour> PendingHours { get; set; }

    public Collaborator()
    {
        Projects = new List<Project>();
        ApproversProject = new List<Project>();
        PendingHours = new List<PendingHour>();
    }
}
