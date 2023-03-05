using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MovieStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<MovieStoreDbContext>>()))
            {
                if(context.Films.Any())
                {
                    return;
                }

                context.Films.AddRange(
                    new Film
                    {
                        Title = "Lord Of The Rings: The Fellowship of the ring",
                        PublishDate = new DateTime(2001), 
                        GenreId = 1,
                        DirectorId = 1,
                        ActorId = 1,
                        Price = 12,
                    }
                );

                context.SaveChanges();
            }
        }
    }
}