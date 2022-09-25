using WebApi.DbOperations;
using WebApi.Entities;


namespace WebApi.UnitTests.TestSetup
{
    public static class Genres
    {
        public static void GetGenres(this BookStoreDbContext context)
        {
            context.Genres.AddRange(
                new Genre 
                    { 
                        Name = "Personel Growth" 
                    },
                    new Genre
                    {
                        Name = "Science Fiction"
                    },
                    new Genre
                    {
                        Name = "Romance"
                    }
            );
            context.SaveChanges();
        }
    }
}