using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDA.Interfaces;
namespace TDA
{
    public class duplicate_comparer<K> : IComparer<K>
    {
        public ComparadorNodosDelegate<K> comparador;
      
      
        public int Compare(K x, K y)
        {
            
                return comparador(y, x);
           
           
           
        }
    }
}
