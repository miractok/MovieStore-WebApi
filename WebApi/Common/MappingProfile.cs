using AutoMapper;
using WebApi.Application.FilmOperations.Queries.GetMovies;
using WebApi.Application.FilmOperations.Queries.GetMovieDetails;
using WebApi.Entities;
using WebApi.Application.ActorOperations.Queries.GetActor;
using WebApi.Application.ActorOperations.Queries.GetActorDetails;
using WebApi.Application.ActorOperations.Commands.CreateActor;

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
        }
    }
}