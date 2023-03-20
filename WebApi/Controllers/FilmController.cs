using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.FilmOperations.Queries.GetMovies;
using WebApi.Application.FilmOperations.Queries.GetMovieDetails;
using WebApi.DBOperations;

namespace WebApi.Controllers;

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
        GetMoviesQuery query = new GetMoviesQuery(_context,_mapper);
        
        var result = query.Handle();

        return Ok(result);
    }

    [HttpGet("{id}")]
    [return: System.Diagnostics.CodeAnalysis.MaybeNull]
    public IActionResult GetMovieById(int id)
    {
        FilmsViewIdModel result;

        GetMovieDetailQuery query = new GetMovieDetailQuery(_context, _mapper);
        query.FilmId = id;

        GetMovieDetailQueryValidator validator = new GetMovieDetailQueryValidator();
        validator.ValidateAndThrow(query);

        result = query.Handle();

        return Ok(result);
    }
}
