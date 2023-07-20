using DijitaruVatigoGengar.Enums;
using DijitaruVatigoGengar.Extensions;

namespace DijitaruVatigoGengar.Data.Dtos;

public class UpdateApprovedHourDto
{
    public int ApproverId { get; set; }
    public string StatusText { get; set; }

}
