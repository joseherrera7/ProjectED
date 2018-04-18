using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TDA;

namespace ProjectED1.Models
{
    public class User
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string password { get; set; }
        public string username { get; set; }
        public int Age { get; set; }
        public ArbolB<Movie, string> WatchList = new ArbolB<Movie, string>(3, "", moviesComparator);
        public List<Movie> WatchList_lista = new List<Movie>();
        
    }
}
