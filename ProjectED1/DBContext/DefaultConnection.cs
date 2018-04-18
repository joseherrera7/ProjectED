using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectED1.Models;
using TDA;
using TDA.Interfaces;

namespace ProjectED1.DBContext
{
    public class DefaultConnection<T, K>
    {
        private static volatile DefaultConnection<T, K> Instance;
        private static object syncRoot = new Object();

       
        public ArbolB<User, string> Users = new ArbolB<User, string>(3, "", stringComparator);
        public ArbolB<Movie, string> MoviesByName = new ArbolB<Movie, string>(3, "", stringComparator);
        public ArbolB<Movie, Movie> MoviesByGenre = new ArbolB<Movie, Movie>(3, null, genreComparator);
        public ArbolB<Movie, Movie> MoviesByYear = new ArbolB<Movie, Movie>(3, null, yearComparator);

        public List<string> Ids = new List<string>();
        public List<Movie> moviesList = new List<Movie>();
        public int IDActual { get; set; }
        public User userLogged;

        /// <summary>
        /// Genres the comparator.
        /// </summary>
        /// <param name="actual">The actual.</param>
        /// <param name="Other">The other.</param>
        /// <returns>-1 , 0, 1</returns>
        public static int genreComparator(Movie actual, Movie Other)
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
        /// <summary>
        /// Years the comparator.
        /// </summary>
        /// <param name="actual">The actual.</param>
        /// <param name="Other">The other.</param>
        /// <returns>-1 , 0, 1</returns>
        public static int yearComparator(Movie actual, Movie Other)
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
        /// <summary>
        /// Strings the comparator.
        /// </summary>
        /// <param name="actual">The actual.</param>
        /// <param name="other">The other.</param>
        /// <returns>-1 , 0, 1</returns>
        public static int stringComparator(string actual, string other)
        {
            return other.CompareTo(actual);
        }




        public DefaultConnection()
        {
            if (Users.contar() == 0)
            {
                IDActual = 0;
            }
            else
            {
                IDActual = Users.contar() - 1;
            }
        }

        public static DefaultConnection<T, K> getInstance
        {

            get
            {

                if (Instance == null)
                {
                    lock (syncRoot)
                    {

                        if (Instance == null)
                        {
                            Instance = new DefaultConnection<T, K>();
                        }
                    }
                }
                return Instance;
            }
        }


    }
}