using DijitaruVatigoGengar.Enums;
using DijitaruVatigoGengar.Models;

namespace DijitaruVatigoGengar.Data.Dtos;

public class ReadProjectDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Budget { get; set; }
    public string TypeText { get; private set; }

}
