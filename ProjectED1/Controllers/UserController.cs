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
    public class UserController : Controller
    {
        DefaultConnection<Movie, string> db = DefaultConnection<Movie, string>.getInstance;
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Logouts this instance.
        /// </summary>
        /// <returns>Returns to the Login View</returns>
        public ActionResult logout()
        {
            db.Users.buscar(db.userLogged.username).WatchList = db.userLogged.WatchList;
            db.Users.buscar(db.userLogged.username).WatchList_lista = db.userLogged.WatchList_lista;
            db.userLogged = null;
            return RedirectToAction("Index", "Login");
        }
        /// <summary>
        /// Adds the movie to the watchlist.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>if movie already on the watchlist, warning. Else, adds the movie to the watchlist</returns>
        public ActionResult AddMovieWatchlist(string id)
        {
            if (db.userLogged.WatchList.existe(id))
            {
                Response.Write("<script>alert('Pelicula ya fue agregada en su watchlist');</script>");
                return RedirectToAction("Catalogo_user", "Filme");

            }
            else
            {
                db.userLogged.WatchList_lista.Clear();
                db.userLogged.WatchList.recorrer(asignComparator);
                db.MoviesByName.recorrer(asignComparator);
                db.userLogged.WatchList.insertar(db.MoviesByName.buscar(id), db.MoviesByName.buscar(id).name);
                db.userLogged.WatchList.recorrer(toList);
                return RedirectToAction("Details", new { id = db.userLogged.username });
            }

        }
        // GET: User/Details/5
        public ActionResult Details(string id)
        {
            User userSearch = db.Users.buscar(id);
            db.userLogged = userSearch;
            return View(userSearch);
        }
        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
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

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
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
        //FUNCIONES y METODOS
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
    }
}
