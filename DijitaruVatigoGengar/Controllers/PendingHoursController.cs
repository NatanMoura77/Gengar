using AutoMapper;
using DijitaruVatigoGengar.Data;
using DijitaruVatigoGengar.Data.Dtos;
using DijitaruVatigoGengar.Enums;
using DijitaruVatigoGengar.Models;
using Microsoft.AspNetCore.Mvc;

namespace DijitaruVatigoGengar.Controllers;

[ApiController]
[Route("Registros")]
public class PendingHoursController : ControllerBase
{
    private DijitaruVatigoGengarContext _context;
    private IMapper _mapper;

    public PendingHoursController(DijitaruVatigoGengarContext dbContext, IMapper mapper)
    {
        _context = dbContext;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AddPendingHour([FromBody] CreatePendingHourDto pendingHourDto)
    {
        var pending = _context.ProjectCollaborators
            .FirstOrDefault(pc => pc.ProjectId == pendingHourDto.ProjectId && pc.CollaboratorId == pendingHourDto.CollaboratorId);

        if (pending == null)
        {
            var existingCollaborator = _context.Collaborators.FirstOrDefault(c => c.Id == pendingHourDto.CollaboratorId);
            if (existingCollaborator == null)
                return NotFound("Colaborador não encontrado.");

            pending = new ProjectCollaborator
            {
                ProjectId = pendingHourDto.ProjectId,
                CollaboratorId = pendingHourDto.CollaboratorId
            };
            _context.ProjectCollaborators.Add(pending);
        }

        var existingProject = _context.Projects.FirstOrDefault(p => p.Id == pendingHourDto.ProjectId);
        if (existingProject == null)
            return NotFound("Projeto não encontrado.");

        var pendingHour = _mapper.Map<PendingHour>(pendingHourDto);
        _context.PendingHours.Add(pendingHour);
        _context.SaveChanges();

        var readPendingHourDto = _mapper.Map<ReadPendingHourDto>(pendingHour);
        return CreatedAtAction(nameof(GetPendingHour), new { id = pendingHour.Id }, readPendingHourDto);
    }

    [HttpGet]
    public IActionResult GetAllPendingHours()
    {
        var pendingHours = _context.PendingHours
            .Where(ph => ph.StatusText.Equals("rejected") || ph.StatusText.Equals("pending"))
            .ToList();
        var readPendingHourDtos = _mapper.Map<List<ReadPendingHourDto>>(pendingHours);
        return Ok(readPendingHourDtos);
    }

    [HttpGet("{id}")]
    public IActionResult GetPendingHour(int id)
    {
        var pendingHour = _context.PendingHours.FirstOrDefault(ph => ph.Id == id && (ph.StatusText.Equals("rejected") || ph.StatusText.Equals("pending")));
        if (pendingHour == null)
            return NotFound("Hora pendente não encontrada.");

        var readPendingHourDto = _mapper.Map<ReadPendingHourDto>(pendingHour);
        return Ok(readPendingHourDto);
    }

    [HttpPut("{id}")]
    public IActionResult UpdatePendingHour(int id, [FromBody] UpdatePendingHourDto pendingHourDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existingPendingHour = _context.PendingHours.FirstOrDefault(ph => ph.Id == id);

        if (existingPendingHour == null)
        {
            var newPendingHour = _mapper.Map<PendingHour>(pendingHourDto);

            var existingCollaborator = _context.Collaborators.FirstOrDefault(c => c.Id == pendingHourDto.CollaboratorId);
            if (existingCollaborator == null)
                return NotFound("Colaborador não encontrado.");

            var existingProject = _context.Projects.FirstOrDefault(p => p.Id == pendingHourDto.ProjectId);
            if (existingProject == null)
                return NotFound("Projeto não encontrado.");

            var existingProjectCollaborator = _context.ProjectCollaborators
                .FirstOrDefault(pc => pc.ProjectId == pendingHourDto.ProjectId && pc.CollaboratorId == pendingHourDto.CollaboratorId);

            if (existingProjectCollaborator == null)
            {
                existingProjectCollaborator = new ProjectCollaborator
                {
                    ProjectId = pendingHourDto.ProjectId,
                    CollaboratorId = pendingHourDto.CollaboratorId
                };
                _context.ProjectCollaborators.Add(existingProjectCollaborator);
            }

            _context.PendingHours.Add(newPendingHour);
            _context.SaveChanges();

            var readPendingHourDto = _mapper.Map<ReadPendingHourDto>(newPendingHour);
            return CreatedAtAction(nameof(GetPendingHour), new { id = newPendingHour.Id }, readPendingHourDto);
        }
        else
        {
            _mapper.Map(pendingHourDto, existingPendingHour);

            var existingCollaborator = _context.Collaborators.FirstOrDefault(c => c.Id == pendingHourDto.CollaboratorId);
            if (existingCollaborator == null)
                return NotFound("Colaborador não encontrado.");

            var existingProject = _context.Projects.FirstOrDefault(p => p.Id == pendingHourDto.ProjectId);
            if (existingProject == null)
                return NotFound("Projeto não encontrado.");

            var existingProjectCollaborator = _context.ProjectCollaborators
                .FirstOrDefault(pc => pc.ProjectId == pendingHourDto.ProjectId && pc.CollaboratorId == pendingHourDto.CollaboratorId);

            if (existingProjectCollaborator == null)
            {
                existingProjectCollaborator = new ProjectCollaborator
                {
                    ProjectId = pendingHourDto.ProjectId,
                    CollaboratorId = pendingHourDto.CollaboratorId
                };
                _context.ProjectCollaborators.Add(existingProjectCollaborator);
            }

            _context.SaveChanges();

            var readPendingHourDto = _mapper.Map<ReadPendingHourDto>(existingPendingHour);
            return Ok(readPendingHourDto);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeletePendingHour(int id)
    {
        var pendingHour = _context.PendingHours.FirstOrDefault(ph => ph.Id == id);
        if (pendingHour == null)
            return NotFound("Hora pendente não encontrada.");

        _context.PendingHours.Remove(pendingHour);
        _context.SaveChanges();

        return NoContent();
    }
}


