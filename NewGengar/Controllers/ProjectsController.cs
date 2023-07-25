using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewGengar.Data;
using NewGengar.Data.Dtos;
using NewGengar.Models;
using System.Collections;

namespace NewGengar.Controllers;

[ApiController]
[Route("[controller]")]
public class ProjectsController : ControllerBase
{
    private NewGengarContext _context;
    private IMapper _mapper;

    public ProjectsController(NewGengarContext dbContext, IMapper mapper)
    {
        _context = dbContext;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult CreateProject(CreateProjectDto projectDto)
    {
        var newProject = new Project
        {
            Name = projectDto.Name,
            Budget = projectDto.Budget,
            TypeText = projectDto.Type,
            Approvers = new List<Collaborator>()
        };

        if (projectDto.CollaboratorsIds != null && projectDto.CollaboratorsIds.Any())
        {
            foreach (var collaboratorId in projectDto.CollaboratorsIds)
            {
                var existingApprover = _context.Collaborators.FirstOrDefault(collaborator => collaborator.Id == collaboratorId);
                if (existingApprover != null)
                {
                    newProject.Approvers.Add(existingApprover);
                }
            }
        }

        _context.Projects.Add(newProject);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetProject), new { id = newProject.Id }, newProject);
    }

    [HttpPost("AddCoollaboration/{id}")]
    public IActionResult AddCollaboratorToProject(int id, [FromBody] int collaboratorId)
    {
        var project = _context.Projects.FirstOrDefault(project => project.Id == id);

        if (project == null) return NotFound();

        var collaborator = _context.Collaborators.FirstOrDefault(collaborator => collaborator.Id == collaboratorId);

        if (collaborator == null) return NotFound();

        if (project.Collaborators.Any(collaborators => collaborators == collaborator))
            return BadRequest();

        project.Collaborators.Add(collaborator);
        _context.SaveChanges();

        return Ok();
    }

    [HttpGet]
    public IEnumerable GetAllProjects()
    {
        return _mapper.Map<List<ReadProjectDto>>(_context.Projects.ToList());
    }

    [HttpGet("{id}")]
    public IActionResult GetProject(int id)
    {
        Project project = _context.Projects.FirstOrDefault(projects => projects.Id == id);
        if (project != null)
        {
            ReadProjectDto projectDto = _mapper.Map<ReadProjectDto>(project);
            return Ok(projectDto);
        }
        return NotFound();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateProject(int id, [FromBody] UpdateProjectDto projectDto)
    {
        Project project = _context.Projects.FirstOrDefault(project => project.Id == id);
        if (project == null)
        {
            return NotFound();
        }
        _mapper.Map(projectDto, project);
        _context.SaveChanges();
        return Ok(project);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProject(int id)
    {
        Project project = _context.Projects.FirstOrDefault(project => project.Id == id);
        if (project == null)
            return NotFound();

        _context.Remove(project);
        _context.SaveChanges();
        return NoContent();
    }
}