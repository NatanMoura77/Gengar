using DijitaruVatigoGengar.Enums;
using DijitaruVatigoGengar.Extensions;
using DijitaruVatigoGengar.Models;

namespace DijitaruVatigoGengar.Data.Dtos;

public class UpdateCollaboratorDto
{
    public string Name { get; set; }
    public string GenderText { get; set; }
    public DateTime BirthDate { get; set; }
    public string ModalityText { get; set; }
    public string RoleText { get; set; }

}
