namespace NewGengar.Data.Dtos;

public class ReadCollaboratorDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string GenderText { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string ModalityText { get; set; }
    public string RoleText { get; set; }

}