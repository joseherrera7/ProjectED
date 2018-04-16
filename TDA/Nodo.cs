using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDA.Interfaces;
namespace TDA
{
    public class Nodo<T,K>
    {
        public SortedList<K,elemento<T, K>> elementos { get; set; }
        public SortedList<K, Nodo<T, K>> hijos { get; set; }
        public Nodo<T, K> padre { get; set; }
        public K llave { get; set; }

        public Nodo( K llave)
        {
          
           this.llave=llave;
            hijos = new SortedList<K, Nodo<T, K>>();
            elementos = new SortedList<K, elemento<T, K>>();


        }

        

    }

    public class elemento<T,K> : IComparable<K>
    {

        public ComparadorNodosDelegate<K> comparador;
        public T valor { get; set; }
      public  K llave { get; set; }
        public elemento(T val,K llave,ComparadorNodosDelegate<K> comparador) {
            valor = val;
           this.llave=llave;
            this.comparador = comparador;
        }

        public int CompareTo(K _other)
        {
            return comparador(this.llave, _other);
        }
    }
}
