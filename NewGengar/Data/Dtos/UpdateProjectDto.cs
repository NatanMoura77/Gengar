using NewGengar.Models;

namespace NewGengar.Data.Dtos;

public class UpdateProjectDto
{
    public string Name { get; set; }
    public double Budget { get; set; }
    public string Type { get; set; }
    public List<int> CollaboratorsIds { get; set; }

}
