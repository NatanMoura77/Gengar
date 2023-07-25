using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewGengar.Data;
using NewGengar.Data.Dtos;
using NewGengar.Models;
using System.Collections;

namespace NewGengar.Controllers;

[ApiController]
[Route("[controller]")]
public class CollaboratorsController : ControllerBase
{
    private NewGengarContext _context;
    private IMapper _mapper;

    public CollaboratorsController(NewGengarContext dbContext, IMapper mapper)
    {
        _context = dbContext;
        _mapper = mapper;
    }
    [HttpPost]
    public IActionResult AddCollaborator([FromBody] CreateCollaboratorDto collaboratorDto)
    {
        var collaborator = _mapper.Map<Collaborator>(collaboratorDto);

        _context.Collaborators.Add(collaborator);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetCollaborator), new { id = collaborator.Id }, collaborator);
    }

    [HttpGet]
    public IEnumerable GetAllCollaborators() 
    {
        return _mapper.Map<List<ReadCollaboratorDto>>(_context.Collaborators.ToList());
    }

    [HttpGet("{id}")]
    public IActionResult GetCollaborator(int id)
    {
        Collaborator collaborator = _context.Collaborators.FirstOrDefault(collaborator => collaborator.Id == id);
        if (collaborator !=null)
        {
            ReadCollaboratorDto collaboratorDto = _mapper.Map<ReadCollaboratorDto>(collaborator);
            return Ok(collaboratorDto);
        }
            return NotFound();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateCollaborator(int id, [FromBody] UpdateCollaboratorDto collaboratorDto)
    {
        Collaborator collaborator = _context.Collaborators.FirstOrDefault(collaborator => collaborator.Id == id);
        if (collaborator == null) 
        {
            return NotFound();
        }
        _mapper.Map(collaboratorDto ,collaborator);
        _context.SaveChanges();
        return Ok(collaborator);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCollaborator(int id) 
    {
        Collaborator collaborator = _context.Collaborators.FirstOrDefault(collaborator => collaborator.Id == id);
        if (collaborator == null)
            return NotFound();

        _context.Remove(collaborator);
        _context.SaveChanges();
        return NoContent();
    }
}