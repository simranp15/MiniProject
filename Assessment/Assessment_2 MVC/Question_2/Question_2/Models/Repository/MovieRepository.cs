using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Question_2.Models;
using System.Data.Entity;
using Question_2.Controllers;

namespace Question_2.Models.Repository
{
    public class MovieRepository<Movie> : IMovieRepository<Movie>where Movie : class
    {
        MovieContext db;
        DbSet<Movie>dbSet;

        public MovieRepository()
        {
            db = new MovieContext();
            dbSet = db.Set<Movie>();
        }

        public void Delete(int id)
        {
               Movie movie = dbSet.Find(id);
            if (movie != null)
                dbSet.Remove(movie);
        }

        public IEnumerable<Movie> GetAll()
        {
            return dbSet.ToList();
        }

        public Movie GetById(int id)
        {
            return dbSet.Find(id);
        }

        public void Insert(Movie movie)
        {
            dbSet.Add(movie);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Movie movie)
        {
            db.Entry(movie).State = EntityState.Modified;
        }

        public IEnumerable<Movie>GetMoviesReleasedInYear(int year)
        {
            return dbSet.Where(m => m.DateofRelease.Year == year).ToList();
        }

        public Movie GetById(object Id)
        {
            throw new NotImplementedException();
        }

        public void Delete(object Id)
        {
            throw new NotImplementedException();
        }
    }
}
       