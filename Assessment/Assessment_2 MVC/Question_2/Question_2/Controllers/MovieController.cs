using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Question_2.Models;
using Question_2.Models.Repository;



namespace Question_2.Models.Repository
{
    public class MoviesController : Controller
    {
        IMovieRepository<Movie> _movieRepo = null;

        public MoviesController()
        {
            _movieRepo = new MovieRepository<Movie>();
        }

        // GET: Movies
        public ActionResult Index()
        {
            var movies = _movieRepo.GetAll();
            return View(movies);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                _movieRepo.Insert(movie);
                _movieRepo.Save();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        public ActionResult Edit(int id)
        {
            var movie = _movieRepo.GetById(id);
            return View(movie);
        }

        [HttpPost]
        public ActionResult Edit(Movie movie)
        {
            if (ModelState.IsValid)
            {
                _movieRepo.Update(movie);
                _movieRepo.Save();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        public ActionResult Details(int id)
        {
            var movie = _movieRepo.GetById(id);
            return View(movie);
        }

        public ActionResult Delete(int id)
        {
            var movie = _movieRepo.GetById(id);
            return View(movie);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _movieRepo.Delete(id);
            _movieRepo.Save();
            return RedirectToAction("Index");
        }
    }
}

