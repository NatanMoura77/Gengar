using DijitaruVatigoGengar.Enums;
using DijitaruVatigoGengar.Extensions;

namespace DijitaruVatigoGengar.Models;

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Budget { get; set; }
    private string typeText;
    public string TypeText
    {
        get { return typeText; }
        set { typeText = value?.ToLower(); }
    }
    public ProjectType Type
    {
        get { return TypeText.ProjectToValue(); }
        set { TypeText = value.ProjectToString(); }
    }

    public IList<PendingHour> PendingHours { get; set; }
    public IList<ProjectCollaborator> ProjectCollaborators { get; set; }

    public Project()
    {
        PendingHours = new List<PendingHour>();
        ProjectCollaborators = new List<ProjectCollaborator>();
    }
}