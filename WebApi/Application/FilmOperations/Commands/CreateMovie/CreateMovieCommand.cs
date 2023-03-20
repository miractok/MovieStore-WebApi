namespace WebApi.Application.FilmOperations.Commands.CreateMovie
{
    public class CreateMovieCommand
    {
        public CreateBookModel? Model { get; set; }

    }

    public class CreateBookModel
    {
        public string? Title { get; set; }
        public DateTime PublishDate { get; set; }
        public string? Genre { get; set; }
        public IReadOnlyCollection<string>? Director { get; set; }
        public IReadOnlyCollection<string>? Actors { get; set; }
        public double Price { get; set; }
    }
}