using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewGengar.Data;
using NewGengar.Data.Dtos;
using NewGengar.Models;
using System.Collections;

namespace NewGengar.Controllers;

[ApiController]
[Route("[controller]")]
public class ApprovedHoursController : ControllerBase
{
    private NewGengarContext _context;
    private IMapper _mapper;

    public ApprovedHoursController(NewGengarContext dbContext, IMapper mapper)
    {
        _context = dbContext;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult CreateApproveedHour([FromBody] CreateApprovedHourDto approvedDto)
    {
        var approver = _context.Collaborators.FirstOrDefault(approver => approver.Id == approvedDto.ApproverId);
        if (approver == null)
        {
            return NotFound();
        }

        if (approver.CollaboratorRole.Equals("normal"))
        {
            return BadRequest();
        }

        var pendingHour = _context.PendingHours.FirstOrDefault(pendingHour => pendingHour.Id == approvedDto.PendingHourId);
        if (pendingHour == null) return NotFound();

        if (approvedDto.StatusText.Equals("pending", StringComparison.OrdinalIgnoreCase))
            return BadRequest();
        

        pendingHour.StatusText = approvedDto.StatusText;

        _context.SaveChanges();
        return NoContent();
    }

    [HttpGet]
    public IEnumerable GetAllApprovedHours()
    {
        var aprovedHours = _context.PendingHours.Where(hours => hours.StatusText == "approved").ToList();
        var aprovedHoursDto = _mapper.Map<List<ReadPendingHourDto>>(aprovedHours);
        return aprovedHoursDto;
    }

    [HttpGet("{id}")]
    public IActionResult GetApprovedHour(int id)
    {
        PendingHour approvedHour = _context.PendingHours.FirstOrDefault(pendingHour => pendingHour.Id == id);
        if (approvedHour != null)
        {
            ReadPendingHourDto approvedHourDto = _mapper.Map<ReadPendingHourDto>(approvedHour);
            if (approvedHourDto.StatusText != "approved")
                return BadRequest();
            return Ok(approvedHourDto);
        }
        return NotFound();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateApprovedHours(int id, [FromBody] UpdateApprovedHourDto approvedHourDto)
    {
        var approver = _context.Collaborators.FirstOrDefault(approver => approver.Id == approvedHourDto.ApproverId);
        if (approver == null)
        {
            return NotFound();
        }

        if (approver.CollaboratorRole.Equals("normal"))
        {
            return BadRequest();
        }

        var approvedHour = _context.PendingHours.FirstOrDefault(pendingHour => pendingHour.Id == id);
        if (approvedHour == null)
        {
            return NotFound();
        }
        _mapper.Map(approvedHourDto, approvedHour);
        _context.SaveChanges();
        return Ok(approvedHour);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteApprovedHour(int id)
    {
        var approvedHour = _context.PendingHours.FirstOrDefault(pendingHour => pendingHour.Id == id);
        if (approvedHour == null)
            return NotFound();

        _context.Remove(approvedHour);
        _context.SaveChanges();
        return NoContent();
    }
}
