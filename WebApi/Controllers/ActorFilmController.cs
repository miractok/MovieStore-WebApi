using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.ActorFilmOperations.Commands.CreateActorFilm;
using WebApi.Application.ActorFilmOperations.Commands.DeleteActorFilm;
using WebApi.Application.ActorFilmOperations.Commands.UpdateActorFilm;
using WebApi.Application.ActorFilmOperations.Queries.GetActorFilm;
using WebApi.Application.ActorFilmOperations.Queries.GetActorFilmDetails;
using WebApi.DBOperations;

namespace WebApi.Controllers;
//[Authorize]
[ApiController]
[Route("[Controller]s")]
public class ActorFilmController : ControllerBase
{
    private readonly IMovieStoreDbContext _context;
    private readonly IMapper _mapper;

    public ActorFilmController(IMapper mapper, IMovieStoreDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    [HttpGet]
    public IActionResult GetActorFilms()
    {
        GetActorFilmQuery query = new GetActorFilmQuery(_mapper,_context);

        var result = query.Handle();

        return Ok(result);
    }

    [HttpGet("{id}")]
    [return: System.Diagnostics.CodeAnalysis.MaybeNull]
    public IActionResult GetActorFilmById(int id)
    {
        ActorFilmViewIdModel result;

        GetActorFilmDetailsQuery query = new GetActorFilmDetailsQuery(_context, _mapper);
        query.ActorFilmId = id;

        GetActorFilmDetailsQueryValidator validator = new GetActorFilmDetailsQueryValidator();
        validator.ValidateAndThrow(query);

        result = query.Handle();

        return Ok(result);
    }

    [HttpPost]
    public IActionResult AddActorFilm([FromBody] CreateActorFilmViewModel newActorFilm)
    {
        CreateActorFilmCommand command = new CreateActorFilmCommand(_mapper,_context);
        command.Model = newActorFilm;

        CreateActorFilmCommandValidator validator = new CreateActorFilmCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpDelete("id")]
    public IActionResult DeleteActorFilm(int id)
    {
        DeleteActorFilmCommand command = new DeleteActorFilmCommand(_context);
        command.DataId = id;

        DeleteActorFilmCommandValidator validator = new DeleteActorFilmCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpPut("id")]
    public IActionResult UpdateActorFilm(int id,[FromBody] UpdateActorFilmModel updateActorFilm)
    {
        UpdateActorFilmCommand command = new UpdateActorFilmCommand(_context);
        command.DataId = id;
        command.Model = updateActorFilm;
        
        UpdateActorFilmCommandValidator validator = new UpdateActorFilmCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();
        
        return Ok();
    }
}