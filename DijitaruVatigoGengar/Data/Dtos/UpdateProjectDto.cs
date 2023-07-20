using DijitaruVatigoGengar.Enums;
using DijitaruVatigoGengar.Extensions;
using DijitaruVatigoGengar.Models;

namespace DijitaruVatigoGengar.Data.Dtos;

public class UpdateProjectDto
{
    public string Name { get; set; }
    public double Budget { get; set; }
    public string TypeText { get; set; }

}
