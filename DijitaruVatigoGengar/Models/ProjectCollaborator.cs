namespace DijitaruVatigoGengar.Models;

public class ProjectCollaborator
{
    public int CollaboratorId { get; set; }
    public int ProjectId { get; set; }

    public Collaborator Collaborator { get; set; }
    public Project Project { get; set; }
}
