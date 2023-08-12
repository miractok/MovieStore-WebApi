using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.DirectorFilmOperations.Commands.CreateDirectorFilm;
using WebApi.Application.DirectorFilmOperations.Commands.DeleteDirectorFilm;
using WebApi.Application.DirectorFilmOperations.Commands.UpdateDirectorFilm;
using WebApi.Application.DirectorFilmOperations.Queries.GetDirectorFilm;
using WebApi.Application.DirectorFilmOperations.Queries.GetDirectorFilmDetails;
using WebApi.DBOperations;

namespace WebApi.Controllers;

//[Authorize]
[ApiController]
[Route("[Controller]s")]
public class DirectorFilmController : ControllerBase
{
    private readonly IMovieStoreDbContext _context;
    private readonly IMapper _mapper;

    public DirectorFilmController(IMapper mapper, IMovieStoreDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    [HttpGet]
    public IActionResult GetDirectorFilms()
    {
        GetDirectorFilmQuery query = new GetDirectorFilmQuery(_mapper,_context);

        var result = query.Handle();

        return Ok(result);
    }

    [HttpGet("{id}")]
    [return: System.Diagnostics.CodeAnalysis.MaybeNull]
    public IActionResult GetDirectorFilmById(int id)
    {
        DirectorFilmViewIdModel result;

        GetDirectorFilmDetailsQuery query = new GetDirectorFilmDetailsQuery(_context, _mapper);
        query.DirectorFilmId = id;

        GetDirectorFilmDetailsQueryValidator validator = new GetDirectorFilmDetailsQueryValidator();
        validator.ValidateAndThrow(query);

        result = query.Handle();

        return Ok(result);
    }

    [HttpPost]
    public IActionResult AddDirectorFilm([FromBody] CreateDirectorFilmViewModel newDirectorFilm)
    {
        CreateDirectorFilmCommand command = new CreateDirectorFilmCommand(_mapper,_context);
        command.Model = newDirectorFilm;

        CreateDirectorFilmCommandValidator validator = new CreateDirectorFilmCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpDelete("id")]
    public IActionResult DeleteDirectorFilm(int id)
    {
        DeleteDirectorFilmCommand command = new DeleteDirectorFilmCommand(_context);
        command.DataId = id;

        DeleteDirectorFilmCommandValidator validator = new DeleteDirectorFilmCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpPut("id")]
    public IActionResult UpdateDirectorFilm(int id,[FromBody] UpdateDirectorFilmModel updateDirectorFilm)
    {
        UpdateDirectorFilmCommand command = new UpdateDirectorFilmCommand(_context);
        command.DataId = id;
        command.Model = updateDirectorFilm;
        
        UpdateDirectorFilmCommandValidator validator = new UpdateDirectorFilmCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();
        
        return Ok();
    }
}