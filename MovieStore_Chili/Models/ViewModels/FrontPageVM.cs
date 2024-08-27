using MovieStore_Chili.Models.Database;

namespace MovieStore_Chili.Models.ViewModels
{
    public class FrontPageVM
    {
        public List<Movie> PopulerMovies { get; set; }
        public List<Movie> NewestMovies { get; set; }

        public List<Movie> OldestMovies { get; set; }

        public List<Movie> CheapestMovies { get; set; }

        
        public Customer MostExpensiveOrderCustomerrName { get; set; } 
        
    }
}
