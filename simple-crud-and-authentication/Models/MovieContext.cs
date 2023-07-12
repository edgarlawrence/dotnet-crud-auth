using Microsoft.EntityFrameworkCore;

namespace simple_crud_and_authentication.Models
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options) : base(options)
        {

        }

        public DbSet<Movie> Movies { get; set; } = null!;

    }
}
