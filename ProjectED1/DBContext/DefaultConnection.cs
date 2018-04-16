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

        private static ComparadorNodosDelegate<K> comparador;
        public ArbolB<User, string> usuarios = new ArbolB<User, string>(3, "", comparadorusuarios);
        public ArbolB<T, K> catalogo = new ArbolB<T, K>(3, default(K), comparador);

        public List<string> Ids = new List<string>();
        public List<Movie> filmes_lista = new List<Movie>();
        public int IDActual { get; set; }
        public User usuariologeado;
        public static int comparadorusuarios(string actual, string other)
        {
            return other.CompareTo(actual);
        }



        public DefaultConnection()
        {
            if (usuarios.contar() == 0)
            {
                IDActual = 0;
            }
            else
            {
                IDActual = usuarios.contar() - 1;
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