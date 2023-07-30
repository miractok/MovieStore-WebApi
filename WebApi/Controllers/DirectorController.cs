using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.DirectorOperations.Commands.CreateDirector;
using WebApi.Application.DirectorOperations.Commands.DeleteDirector;
using WebApi.Application.DirectorOperations.Commands.UpdateDirector;
using WebApi.Application.DirectorOperations.Queries.GetDirectors;
using WebApi.Application.DirectorOperations.Queries.GetDirectorDetails;
using WebApi.DBOperations;

namespace WebApi.Controllers;

[ApiController]
[Route("[Controller]s")]
public class DirectorController : ControllerBase
{
    private readonly IMovieStoreDbContext _context;
    private readonly IMapper _mapper;

    public DirectorController(IMapper mapper, IMovieStoreDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    [HttpGet]
    public IActionResult GetDirectors()
    {
        GetDirectorsQuery query = new GetDirectorsQuery(_mapper,_context);
        
        var result = query.Handle();

        return Ok(result);
    }

    [HttpGet("id")]
    public IActionResult GetDirectorById(int id)
    {
        DirectorViewIdModel result;

        GetDirectorDetailsQuery query = new GetDirectorDetailsQuery(_mapper,_context);
        query.DirectorId = id;

        GetDirectorDetailsQueryValidator validator = new GetDirectorDetailsQueryValidator();
        validator.ValidateAndThrow(query);

        result = query.Handle();

        return Ok(result);
    }

    [HttpPost]
    public IActionResult AddDirector([FromBody] CreateDirectorViewModel newDirector)
    {
        CreateDirectorCommand command = new CreateDirectorCommand(_mapper,_context);
        command.Model = newDirector;

        CreateDirectorCommandValidator validator = new CreateDirectorCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpDelete("id")]
    public IActionResult DeleteDirector(int id)
    {
        DeleteDirectorCommand command = new DeleteDirectorCommand(_context);
        command.DirectorId = id;

        DeleteDirectorCommandValidator validator = new DeleteDirectorCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();
    
        return Ok();
    }

    [HttpPut("id")]
    public IActionResult UpdateDirector(int id,[FromBody] UpdateDirectorViewModel updateDirector)
    {
        UpdateDirectorCommand command = new UpdateDirectorCommand(_context);
        command.DirectorId = id;
        command.Model = updateDirector;
        
        UpdateDirectorCommandValidator validator = new UpdateDirectorCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();
        
        return Ok();
    }
}