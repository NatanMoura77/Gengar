using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewGengar.Data;
using NewGengar.Data.Dtos;
using NewGengar.Enums;
using NewGengar.Models;
using System.Collections;

namespace NewGengar.Controllers;

[ApiController]
[Route("[controller]")]
public class PendingHoursController : ControllerBase
{
    private NewGengarContext _context;
    private IMapper _mapper;

    public PendingHoursController(NewGengarContext dbContext, IMapper mapper)
    {
        _context = dbContext;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AddHours([FromBody] CreatePendingHourDto pendingHourDto)
    {
        var project = _context.Projects
            .FirstOrDefault(project => project.Id == pendingHourDto.ProjectId);

        if (project == null) return NotFound();

        var collaborator = _context.Collaborators
            .FirstOrDefault(collaborator => collaborator.Id == pendingHourDto.CollaboratorId);

        if (collaborator == null) return NotFound();

        if (!project.Collaborators.Any(collaborators => collaborators == collaborator))
            return BadRequest();

        PendingHour pendingHour = _mapper.Map<PendingHour>(pendingHourDto);
        pendingHour.Status = StatusHour.Pending;

        _context.PendingHours.Add(pendingHour);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetPendingHour), new { Id = pendingHour.Id }, pendingHour);
    }

    [HttpGet]
    public IEnumerable GetAllPendingHours()
    {
        return _mapper.Map<List<ReadPendingHourDto>>(_context.PendingHours.ToList());
    }

    [HttpGet("{id}")]
    public IActionResult GetPendingHour(int id)
    {
        PendingHour pendingHour = _context.PendingHours.FirstOrDefault(pendingHour => pendingHour.Id == id);
        if (pendingHour != null)
        {
            ReadPendingHourDto pendingHourDto = _mapper.Map<ReadPendingHourDto>(pendingHour);
            return Ok(pendingHourDto);
        }
        return NotFound();
    }

    [HttpPut("{id}")]
    public IActionResult UpdatePendingHours(int id, [FromBody] UpdatePendingHourDto pendingHourDto)
    {
        //PendingHour pendingHour = _context.PendingHours.FirstOrDefault(pendingHour => pendingHour.Id == id);
        //if (pendingHour == null)
        //{
        //    return NotFound();
        //}
        var project = _context.Projects
          .FirstOrDefault(project => project.Id == pendingHourDto.ProjectId);

        if (project == null) return NotFound();

        var collaborator = _context.Collaborators
            .FirstOrDefault(collaborator => collaborator.Id == pendingHourDto.CollaboratorId);

        if (collaborator == null) return NotFound();

        if (!project.Collaborators.Any(collaborators => collaborators == collaborator))
            return BadRequest();

        PendingHour pendingHour = _mapper.Map<PendingHour>(pendingHourDto);
        pendingHour.Status = StatusHour.Pending;

        _mapper.Map(pendingHourDto, pendingHour);
        _context.SaveChanges();
        return Ok(pendingHour);
    }

    [HttpDelete("{id}")]
    public IActionResult DeletePendingHour(int id)
    {
        PendingHour pendingHour = _context.PendingHours.FirstOrDefault(pendingHour => pendingHour.Id == id);
        if (pendingHour == null)
            return NotFound();

        _context.Remove(pendingHour);
        _context.SaveChanges();
        return NoContent();
    }
}