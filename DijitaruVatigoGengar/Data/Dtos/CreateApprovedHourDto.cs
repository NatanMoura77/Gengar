using DijitaruVatigoGengar.Enums;
using DijitaruVatigoGengar.Extensions;

namespace DijitaruVatigoGengar.Data.Dtos;

public class CreateApprovedHourDto
{
    public int ApproverId { get; set; }
    public int PendingHourId { get; set; }
    public string StatusText { get; set; }

}
