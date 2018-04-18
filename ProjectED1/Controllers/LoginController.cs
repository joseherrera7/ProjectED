using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectED1;
using ProjectED1.Models;
using ProjectED1.DBContext;

namespace ProyectoED1.Controllers
{
    public class LoginController : Controller
    {
        DefaultConnection<Movie, string> db = DefaultConnection<Movie, string>.getInstance;
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Verifies the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="pass">The pass.</param>
        /// <returns></returns>
        public ActionResult Login(string user, string pass)
        {
            if(user=="admin"&& user == "admin")
            {
                return RedirectToAction("Index", "Filme");

            } else {
                User newUser = null;
                User searchUser = db.Users.buscar(user);
                if (searchUser != null)
                {
                    if (searchUser.username == user && searchUser.password == pass)
                    {
                        newUser = searchUser;
                    }
                }

                if (newUser != null)
                {
                    return RedirectToAction("Details", "User", new { id = newUser.username });
                }
                else
                {
                    return View("Index");
                }
            }
           
        }


    }
}
