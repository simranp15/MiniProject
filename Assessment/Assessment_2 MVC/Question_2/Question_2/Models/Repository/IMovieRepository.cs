using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Question_2.Models.Repository
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetAllMovies();
        Movie GetMovieById(int id);
        void AddMovie(Movie movie);
        void EditMovie(Movie movie);
        void DeleteMovie(int id);
        IEnumerable<Movie> GetMoviesReleasedInYear(int year);
    }
    
}

