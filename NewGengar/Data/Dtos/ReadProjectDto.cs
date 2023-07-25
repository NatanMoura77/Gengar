using NewGengar.Models;

namespace NewGengar.Data.Dtos;

public class ReadProjectDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Budget { get; set; }
    public string TypeText { get; set; }
    public virtual IList<Collaborator> Approvers { get; set; }

}
