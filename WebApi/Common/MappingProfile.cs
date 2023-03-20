using AutoMapper;
using WebApi.Application.FilmOperations.Queries.GetMovies;
using WebApi.Application.FilmOperations.Queries.GetMovieDetails;
using WebApi.Entities;
using WebApi.Application.ActorOperations.Queries.GetActors;
using WebApi.Application.ActorOperations.Queries.GetActorDetails;
using WebApi.Application.ActorOperations.Commands.CreateActor;
using WebApi.Application.DirectorOperations.Queries.GetDirectors;
using WebApi.Application.DirectorOperations.Queries.GetDirectorDetails;
using WebApi.Application.DirectorOperations.Commands.CreateDirector;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Application.GenreOperations.Queries.GetGenreDetails;
using WebApi.Application.GenreOperations.Commands.CreateGenre;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Film Operations Queries Get
            CreateMap<Film, FilmViewModel>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(dest => dest.Director, opt => opt.MapFrom(m => m.DirectorFilm.Select(s => s.Director.NameSurname)))
                .ForMember(dest => dest.Actors, opt => opt.MapFrom(m => m.ActorFilm.Select(s => s.Actor.NameSurname)));

            //Film Operations Queries Get Details
            CreateMap<Film, FilmsViewIdModel>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(dest => dest.Director, opt => opt.MapFrom(m => m.DirectorFilm.Select(s => s.Director.NameSurname)))
                .ForMember(dest => dest.Actors, opt => opt.MapFrom(m => m.ActorFilm.Select(s => s.Actor.NameSurname)));

            //Actor Operations Queries Get
            CreateMap<Actor, ActorViewModel>()
                .ForMember(dest => dest.Films, opt => opt.MapFrom(m => m.ActorFilm.Select(s => s.Film.Title)));

            //Actor Operations Queries Get Details
            CreateMap<Actor, ActorsViewIdModel>()
                .ForMember(dest => dest.Films, opt => opt.MapFrom(m => m.ActorFilm.Select(s => s.Film.Title)));

            //Actor Operations Commands Create Actor
            CreateMap<CreateActorViewModel, Actor>();

            //Director Operations Queries Get
            CreateMap<Director, DirectorViewModel>()
                .ForMember(dest => dest.Films, opt => opt.MapFrom(m => m.DirectorFilm.Select(s => s.Film.Title)));

            //Director Operations Queries Get Details
            CreateMap<Director, DirectorViewIdModel>()
                .ForMember(dest => dest.Films, opt => opt.MapFrom(m => m.DirectorFilm.Select(s => s.Film.Title)));

            //Director Operations Commands Create Director
            CreateMap<CreateDirectorViewModel, Director>();

            //Genre Operations Queries Get
            CreateMap<Genre, GenresViewModel>();

            //Genre Operations Queries Get Details
            CreateMap<Genre, GenresViewIdModel>();

            //Actor Operations Commands Create Actor
            CreateMap<CreateGenreViewModel, Genre>();
        }
    }
}