using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.FilmOperations.Commands.CreateFilm;
using WebApi.Application.FilmOperations.Commands.DeleteFilm;
using WebApi.Application.FilmOperations.Commands.UpdateFilm;
using WebApi.Application.FilmOperations.Queries.GetFilmDetails;
using WebApi.Application.FilmOperations.Queries.GetFilms;
using WebApi.DBOperations;

namespace WebApi.Controllers;

//[Authorize]
[ApiController]
[Route("[Controller]s")]
public class FilmController : ControllerBase
{
    private readonly IMovieStoreDbContext _context;
    private readonly IMapper _mapper;

    public FilmController(IMapper mapper, IMovieStoreDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    [HttpGet]
    public IActionResult GetMovies()
    {
        GetFilmsQuery query = new GetFilmsQuery(_context,_mapper);
        
        var result = query.Handle();

        return Ok(result);
    }

    [HttpGet("{id}")]
    [return: System.Diagnostics.CodeAnalysis.MaybeNull]
    public IActionResult GetMovieById(int id)
    {
        FilmsViewIdModel result;

        GetFilmDetailsQuery query = new GetFilmDetailsQuery(_context, _mapper);
        query.FilmId = id;

        GetFilmDetailsQueryValidator validator = new GetFilmDetailsQueryValidator();
        validator.ValidateAndThrow(query);

        result = query.Handle();

        return Ok(result);
    }

    [HttpPost]
    public IActionResult AddFilm([FromBody] CreateFilmModel newFilm)
    {
        CreateFilmCommand command = new CreateFilmCommand(_mapper,_context);
        command.Model = newFilm;

        CreateFilmCommandValidator validator = new CreateFilmCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpDelete("id")]
    public IActionResult DeleteFilm(int id)
    {
        DeleteFilmCommand command = new DeleteFilmCommand(_context);
        command.FilmId = id;

        DeleteFilmCommandValidator validator = new DeleteFilmCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpPut("id")]
    public IActionResult UpdateFilm(int id,[FromBody] UpdateFilmModel updateFilm)
    {
        UpdateFilmCommand command = new UpdateFilmCommand(_context);
        command.FilmId = id;
        command.Model = updateFilm;
        
        UpdateFilmCommandValidator validator = new UpdateFilmCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();
        
        return Ok();
    }
}
