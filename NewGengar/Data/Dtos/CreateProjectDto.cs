using NewGengar.Models;

namespace NewGengar.Data.Dtos;

public class CreateProjectDto
{
    public string Name { get; set; }
    public decimal Budget { get; set; }
    public string Type { get; set; }
    public List<int> CollaboratorsIds { get; set; }
}
