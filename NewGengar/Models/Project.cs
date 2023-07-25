using NewGengar.Enums;
using NewGengar.Extensions;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewGengar.Models;

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Budget { get; set; }
    public string TypeText { get; set; }
    public ProjectType Type
    {
        get { return TypeText.ToLower().ProjectToValue(); }
        set { TypeText = value.ProjectToString(); }
    }
    [JsonIgnore]
    public virtual IList<Collaborator> Collaborators { get; set; }
    [JsonIgnore]
    public virtual IList<Collaborator> Approvers { get; set; }
    [JsonIgnore]
    public virtual IList<PendingHour> PendingHours { get; set; }
    [NotMapped]
    public List<int> CollaboratorsIds { get; set; }
    public Project()
    {
        CollaboratorsIds = new List<int>();
        Collaborators = new List<Collaborator>();
        Approvers = new List<Collaborator>();
        PendingHours = new List<PendingHour>();
    }
}
