using Microsoft.EntityFrameworkCore;
using MovieStore_Chili.Models.Database;

namespace MovieStore_Chili.Data
{
    public class MovieDbContext:DbContext
    {
         public MovieDbContext(DbContextOptions<MovieDbContext>dbContextoptions) : base(dbContextoptions) 
        {

        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<OrderRow> OrderRows { get; set; }

    }
}
