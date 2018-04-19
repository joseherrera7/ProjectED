using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProjectED1.DBContext;
using ProjectED1.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
        /// Creates the by json.
        /// </summary>
        /// <returns></returns>

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create        
        /// <summary>
        /// Register the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>returns to the login biew</returns>
        [HttpPost]
        public ActionResult Create([Bind(Include = "firstName,lastName,Age,username,password")]User user)
        {
            try
            {
                if (db.Users.existe(user.username))
                {
                    Response.Write("<script>alert('Ya existe este usuario.');</script>");
                    return View();
                }
                else
                {
                    Response.Write("<script>alert('Usuario creado exitosamente.');</script>");
                    db.Users.insertar(user, user.username);
                    
                    return RedirectToAction("Index", "Login");

                }
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase postedFile)
        {

           



            if (postedFile != null)
            {


                string filepath = string.Empty;

                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                filepath = path + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(filepath);

                string csvData = System.IO.File.ReadAllText(filepath);



                try
                {
                    JObject json = JObject.Parse(csvData);

                    foreach (JProperty property in json.Properties())
                    {

                        string x = property.Value.ToString();
                        Movie y = JsonConvert.DeserializeObject<Movie>(x);
                        if (!db.MoviesByName.existe(y.name))
                        {
                            db.moviesList.Clear();
                            db.MoviesByName.recorrer(AsignNameComparator);
                            db.MoviesByGenre.recorrer(AsignGenreComparator);
                            db.MoviesByYear.recorrer(AsignYearComparator);
                            elemento<Movie, string> newMovieByName = new elemento<Movie, string>(y, y.name, nameComparator);
                            elemento<Movie, Movie> newMovieByGenre = new elemento<Movie, Movie>(y, y, genreComparator);
                            elemento<Movie, Movie> newMovieByYear = new elemento<Movie, Movie>(y, y, yearComparator);

                            db.MoviesByName.insertar(newMovieByName.valor, newMovieByName.valor.name);
                            db.MoviesByGenre.insertar(newMovieByGenre.valor, newMovieByGenre.valor);
                            db.MoviesByYear.insertar(newMovieByYear.valor, newMovieByYear.valor);

                            if (OrderSelection == "genero")
                            {
                                db.MoviesByGenre.recorrer(To_ListGenre);

                            }
                            else if (OrderSelection == "nombre")
                            {
                                db.MoviesByName.recorrer(To_List);

                            }
                            else if (OrderSelection == "anio")
                            {
                                db.MoviesByYear.recorrer(To_ListInt);

                            }

                            return RedirectToAction("Index");
                        }
                        else
                        {

                            return RedirectToAction("Index");
                        }
                    }
                    ViewBag.Message = "Cargado Exitosamente";
                }
                catch (Exception)
                {
                    ViewBag.Message = "Dato erroneo.";
                }

            }

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
        // GET: User/Delete/5
        public ActionResult DeleteMovies(string id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult DeleteMovies(string id, FormCollection collection)
        {
            try
            {

                // TODO: Add delete logic here
                Movie searchMovie = db.MoviesByName.buscar(id);
                db.MoviesByGenre.eliminar(searchMovie);
                db.MoviesByName.eliminar(id);
                db.MoviesByYear.eliminar(searchMovie);
                return RedirectToAction("Index", "User");
            }
            catch
            {
                ViewBag.Message("No se pudo eliminar");
                return RedirectToAction("Index", "User");
            }
        }
        // GET: User/Delete/5
        public ActionResult DeleteUser(string id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult DeleteUser(string id, FormCollection collection)
        {
            try
            {
                
                // TODO: Add delete logic here
                db.Users.eliminar(id);
                return RedirectToAction("Index", "User");
            }
            catch
            {
                ViewBag.Message("No se pudo eliminar");
                return RedirectToAction("Index", "User");
            }
        }
        // GET: User/Delete/5
        public ActionResult DeleteWatchList(string id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult DeleteWatchList(string id, FormCollection collection)
        {
            try
            {
                Movie searchMovie = db.MoviesByName.buscar(id);
                // TODO: Add delete logic here
                db.userLogged.WatchList.eliminar(searchMovie.name);
                db.moviesList.Clear();
                db.userLogged.WatchList.recorrer(asignComparator);
                db.userLogged.WatchList.recorrer(toList);
                return RedirectToAction("Details", new { id = db.userLogged.username });
            }
            catch
            {
                return RedirectToAction("Details", new { id = db.userLogged.username });
            }
        }
        string OrderSelection = "nombre";
        //FUNCIONES y METODOS
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

    }
}
#endregion
