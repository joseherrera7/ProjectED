using ProjectED1.DBContext;
using ProjectED1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDA;

namespace ProjectED1.Controllers
{
    public class MovieController : Controller
    {
        DefaultConnection<Movie, string> db = DefaultConnection<Movie, string>.getInstance;
        // GET: Movie
        public ActionResult Index()
        {
            return View();
        }

        // GET: Movie/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Movie/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movie/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Movie/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Movie/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Movie/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Movie/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        #region Funciones y Metodos
        public void asignComparator(elemento<Movie, string> actual)
        {
            actual.comparador = moviesComparator;
        }
        public static int moviesComparator(string actual, string Other)
        {
            return Other.CompareTo(actual);
        }
        public void toList(elemento<Movie, string> actual)
        {
            db.userLogged.WatchList_lista.Add(actual.valor);
        }
        public void To_List(elemento<Movie, string> actual)
        {
            db.moviesList.Add(actual.valor);
        }

        public void To_ListGenre(elemento<Movie, Movie> actual)
        {
            db.moviesList.Add(actual.valor);
        }
        public void To_ListInt(elemento<Movie, Movie> actual)
        {
            db.moviesList.Add(actual.valor);
        }

        public void AsignNameComparator(elemento<Movie, string> actual)
        {
            actual.comparador = nameComparator;
        }
        public void AsignGenreComparator(elemento<Movie, Movie> actual)
        {
            actual.comparador = genreComparator;
        }
        public void AsignYearComparator(elemento<Movie, Movie> actual)
        {
            actual.comparador = yearComparator;
        }
        public int nameComparator(string actual, string Other)
        {
            return Other.CompareTo(actual);
        }
        public int genreComparator(Movie actual, Movie Other)
        {

            if (Other.genre.CompareTo(actual.genre) == 0)
            {

                return Other.name.CompareTo(actual.name);
            }
            else
            {

                return Other.genre.CompareTo(actual.genre);
            }

        }
        public int yearComparator(Movie actual, Movie Other)
        {
            if (Other.year.CompareTo(actual.year) == 0)
            {

                return Other.name.CompareTo(actual.name);
            }
            else
            {

                return Other.year.CompareTo(actual.year);
            }
        }
        #endregion
    }
}

  
