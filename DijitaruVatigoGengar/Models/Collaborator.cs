using DijitaruVatigoGengar.Enums;
using DijitaruVatigoGengar.Extensions;
using System.Diagnostics.Contracts;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DijitaruVatigoGengar.Models;

public class Collaborator
{
    public int Id { get; set; }
    public string Name { get; set; }

    private string genderText;
    public string GenderText
    {
        get { return genderText; }
        set { genderText = value?.ToLower(); }
    }

    public Gender Gender
    {
        get { return GenderText.GenderToValue(); }
        set { GenderText = value.GenderToString(); }
    }

    public DateTime BirthDate { get; set; }

    private string modalityText;
    public string ModalityText
    {
        get { return modalityText; }
        set { modalityText = value?.ToLower(); }
    }

    public ContractModality Modality
    {
        get { return ModalityText.ModalityToValue(); }
        set { ModalityText = value.ModalityToString(); }
    }

    private string roleText;
    public string RoleText
    {
        get { return roleText; }
        set { roleText = value?.ToLower(); }
    }

    public Role CollaboratorRole
    {
        get { return RoleText.RoleToValue(); }
        set { RoleText = value.RoleToString(); }
    }

    public IList<PendingHour> PendingHours { get; set; }
    public IList<ProjectCollaborator> ProjectCollaborators { get; set; }

    public Collaborator()
    {
        PendingHours = new List<PendingHour>();
        ProjectCollaborators = new List<ProjectCollaborator>();
    }
}