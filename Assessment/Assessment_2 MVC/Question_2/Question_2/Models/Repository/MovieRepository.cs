using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Question_2.Models;

namespace Question_2.Models.Repository
{
    public class MovieRepository<T> : IMovieRepository<T> where T : class
    {
        MovieContext db;
        DbSet<T> dbSet;

        public MovieRepository()
        {
            db = new MovieContext();
            dbSet = db.Set<T>();
        }

        public void Delete(int id)
        {
            T entity = dbSet.Find(id);
            if (entity != null)
                dbSet.Remove(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public T GetById(int id)
        {
            return dbSet.Find(id);
        }

        public void Insert(T entity)
        {
            dbSet.Add(entity);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(T entity)
        {
            db.Entry(entity).State = EntityState.Modified;
        }

        
    }

}
