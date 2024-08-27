using MovieStore_Chili.Models.Database;

namespace MovieStore_Chili.Services
{
    public interface IMovieService
    {
        List<Movie> GetMovies();
        List<Movie> GetPopularMovies();

        Movie GetMovieById(int id);
        
        void AddMovie(Movie movie);

        void RemoveMovie(int íd);

        void UpdateMovie(Movie movie);
       

    }
}
