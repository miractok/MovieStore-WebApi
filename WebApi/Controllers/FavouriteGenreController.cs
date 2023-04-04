using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.FavouriteGenreOperations.Commands.CreateFavouriteGenre;
using WebApi.Application.FavouriteGenreOperations.Commands.DeleteFavouriteGenre;
using WebApi.Application.FavouriteGenreOperations.Commands.UpdateFavouriteGenre;
using WebApi.Application.FavouriteGenreOperations.Queries.GetFavouriteGenre;
using WebApi.Application.FavouriteGenreOperations.Queries.GetFavouriteGenreDetails;
using WebApi.DBOperations;

namespace WebApi.Controllers;

[ApiController]
[Route("[Controller]s")]
public class FavouriteGenreController : ControllerBase
{
    private readonly IMovieStoreDbContext _context;
    private readonly IMapper _mapper;

    public FavouriteGenreController(IMapper mapper, IMovieStoreDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    [HttpGet]
    public IActionResult GetFavouriteGenres()
    {
        GetFavouriteGenreQuery query = new GetFavouriteGenreQuery(_mapper,_context);

        var result = query.Handle();

        return Ok(result);
    }

    [HttpGet("{id}")]
    [return: System.Diagnostics.CodeAnalysis.MaybeNull]
    public IActionResult GetFavouriteGenreById(int id)
    {
        FavouriteGenreViewIdModel result;

        GetFavouriteGenreDetailQuery query = new GetFavouriteGenreDetailQuery(_context, _mapper);
        query.favouriteGenreId = id;

        GetFavouriteGenreDetailQueryValidator validator = new GetFavouriteGenreDetailQueryValidator();
        validator.ValidateAndThrow(query);

        result = query.Handle();

        return Ok(result);
    }

    [HttpPost]
    public IActionResult AddFavouriteGenre([FromBody] CreateFavouriteGenreViewModel newFavouriteGenre)
    {
        CreateFavouriteGenreCommand command = new CreateFavouriteGenreCommand(_mapper,_context);
        command.Model = newFavouriteGenre;

        CreateFavouriteGenreCommandValidator validator = new CreateFavouriteGenreCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpDelete("id")]
    public IActionResult DeleteFavouriteGenre(int id)
    {
        DeleteFavouriteGenreCommand command = new DeleteFavouriteGenreCommand(_context);
        command.DataId = id;

        DeleteFavouriteGenreCommandValidator validator = new DeleteFavouriteGenreCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpPut("id")]
    public IActionResult UpdateFavouriteGenre(int id,[FromBody] UpdateFavouriteGenreModel updateFavouriteGenre)
    {
        UpdateFavouriteGenreCommand command = new UpdateFavouriteGenreCommand(_context);
        command.DataId = id;
        command.Model = updateFavouriteGenre;
        
        UpdateFavouriteGenreCommandValidator validator = new UpdateFavouriteGenreCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();
        
        return Ok();
    }
}