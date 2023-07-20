using AutoMapper;
using DijitaruVatigoGengar.Data.Dtos;
using DijitaruVatigoGengar.Data;
using Microsoft.AspNetCore.Mvc;
using DijitaruVatigoGengar.Enums;


namespace DijitaruVatigoGengar.Controllers;

[ApiController]
[Route("Aprovações")]
public class ApprovedHoursController : ControllerBase
{
    private DijitaruVatigoGengarContext _context;
    private IMapper _mapper;

    public ApprovedHoursController(DijitaruVatigoGengarContext dbContext, IMapper mapper)
    {
        _context = dbContext;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult CreateApproved([FromBody] CreateApprovedHourDto approvedDto)
    {
        var approver = _context.Collaborators.FirstOrDefault(c => c.Id == approvedDto.ApproverId);
        if (approver == null)
            return NotFound("Aprovador não encontrado.");

        var pendingHour = _context.PendingHours.FirstOrDefault(ph => ph.Id == approvedDto.PendingHourId && ph.StatusText == "pending");
        if (pendingHour == null)
            return NotFound("Hora pendente não encontrada ou já aprovada/rejeitada.");

        if (approver.CollaboratorRole != Role.Approver && approver.CollaboratorRole != Role.Admin)
            return Forbid("Você não tem permissão para aprovar horas pendentes.");

        if (pendingHour.CollaboratorId == approver.Id)
            return BadRequest("Você não pode aprovar suas próprias horas pendentes.");

        if (approvedDto.StatusText.Equals("approved", StringComparison.OrdinalIgnoreCase) || approvedDto.StatusText.Equals("rejected", StringComparison.OrdinalIgnoreCase))
        {
            pendingHour.StatusText = approvedDto.StatusText;
        }
        else
        {
            return BadRequest("O valor do status fornecido não é válido. Use Approved ou Rejected.");
        }

        _context.SaveChanges();

        var readPendingHourDto = _mapper.Map<ReadPendingHourDto>(pendingHour);
        return Ok(readPendingHourDto);
    }

    [HttpGet]
    public IActionResult GetAllApprovedHours()
    {
        var approvedHours = _context.PendingHours
            .Where(ph => ph.StatusText.ToLower() == "approved")
            .ToList();

        var readApprovedHourDtos = _mapper.Map<List<ReadPendingHourDto>>(approvedHours);
        return Ok(readApprovedHourDtos);
    }

    [HttpGet("{id}")]
    public IActionResult GetApprovedHour(int id)
    {
        var approvedHour = _context.PendingHours.FirstOrDefault(ph => ph.Id == id && ph.StatusText.Equals("approved"));
        if (approvedHour == null)
            return NotFound("Hora aprovada não encontrada.");

        var readApprovedHourDto = _mapper.Map<ReadPendingHourDto>(approvedHour);
        return Ok(readApprovedHourDto);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateApprovalStatus(int id, [FromBody] UpdateApprovedHourDto updateDto)
    {
        var approvedHour = _context.PendingHours.FirstOrDefault(ph => ph.Id == id && ph.StatusText.ToLower() == "approved");

        if (approvedHour != null)
        {
            if (!updateDto.StatusText.Equals("rejected", StringComparison.OrdinalIgnoreCase) && !updateDto.StatusText.Equals("pending", StringComparison.OrdinalIgnoreCase))
                return BadRequest("O valor do status fornecido não é válido. Use Rejected ou Pending.");

            var approverId = approvedHour.CollaboratorId;
            if (updateDto.ApproverId == approverId)
                return BadRequest("Você não pode aprovar suas próprias horas aprovadas.");

            approvedHour.StatusText = updateDto.StatusText.ToLower();
            _context.SaveChanges();

            var readApprovedHourDto = _mapper.Map<ReadPendingHourDto>(approvedHour);
            return Ok(readApprovedHourDto);
        }

        var pendingHour = _context.PendingHours.FirstOrDefault(ph => ph.Id == id && (ph.StatusText.ToLower() == "pending" || ph.StatusText.ToLower() == "rejected"));

        if (pendingHour != null)
        {
            var collaboratorId = pendingHour.CollaboratorId;
            if (updateDto.ApproverId == collaboratorId)
                return BadRequest("Você não pode alterar suas próprias horas pendentes.");

            if (pendingHour.StatusText.Equals("rejected", StringComparison.OrdinalIgnoreCase))
            {
                if (!updateDto.StatusText.Equals("approved", StringComparison.OrdinalIgnoreCase) && !updateDto.StatusText.Equals("pending", StringComparison.OrdinalIgnoreCase))
                    return BadRequest("O valor do status fornecido não é válido. Use Approved ou Pending.");
            }
            else if (pendingHour.StatusText.Equals("pending", StringComparison.OrdinalIgnoreCase))
            {
                if (!updateDto.StatusText.Equals("approved", StringComparison.OrdinalIgnoreCase) && !updateDto.StatusText.Equals("rejected", StringComparison.OrdinalIgnoreCase))
                    return BadRequest("O valor do status fornecido não é válido. Use Approved ou Rejected.");
            }

            pendingHour.StatusText = updateDto.StatusText.ToLower();
            _context.SaveChanges();

            var readPendingHourDto = _mapper.Map<ReadPendingHourDto>(pendingHour);
            return Ok(readPendingHourDto);
        }

        return NotFound("Hora não encontrada.");
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteApprovedHour(int id)
    {
        var approvedHour = _context.PendingHours.FirstOrDefault(ph => ph.Id == id && ph.StatusText.Equals("approved", StringComparison.OrdinalIgnoreCase));
        if (approvedHour == null)
            return NotFound("Hora aprovada não encontrada.");

        _context.PendingHours.Remove(approvedHour);
        _context.SaveChanges();

        return NoContent();
    }
}
