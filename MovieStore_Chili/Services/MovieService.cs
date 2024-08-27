using MovieStore_Chili.Data;
using MovieStore_Chili.Models.Database;
using MovieStore_Chili.Models.ViewModels;

namespace MovieStore_Chili.Services
{
    public class MovieService: IMovieService
    {
        private readonly MovieDbContext _db;
        private readonly IConfiguration _configuration;

        public MovieService(MovieDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }
        public List<Movie> GetMovies()
        {
            var movies = _db.Movies.ToList();
            return movies;
        }
        public Movie GetMovieById(int id)
        {
            var movie = _db.Movies.FirstOrDefault(m => m.Id == id);
            return movie;
        }

        public void AddMovie(Movie movie)
        {
            _db.Movies.Add(movie);
            _db.SaveChanges();
        }

        public void RemoveMovie(Movie movie)
        {
            _db.Movies.Remove(movie);
            _db.SaveChanges();
        }

        public void RemoveMovie(int id)
        {
            var movie = _db.Movies.First(m => m.Id == id);
            _db.Movies.Remove(movie);
            _db.SaveChanges();
        }

        public void UpdateMovie(Movie movie)
        {
            _db.Movies.Update(movie);
            _db.SaveChanges();
        }
        public List<Movie> GetPopularMovies()
        {
            
           var PopularMovies=_db.Orders
                                 .SelectMany(o =>o.OrderRows)
                                 .GroupBy(or =>or.MovieId)
                                 .OrderByDescending(g =>g.Count())
                                 .Select(g =>g.First().Movie)
                                 .Take(5).ToList();
            return PopularMovies;




        }
       

    }
}
