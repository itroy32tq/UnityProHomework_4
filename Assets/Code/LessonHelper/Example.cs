using System.Collections.Generic;
using UnityEngine;

namespace Code
{
    public sealed class Example
    {
        private void Main()
        {
            List<A> t = new List<A>();
            IEnumerable<A> y = new List<B>();
            //IComparer<B> u =
            NameMethod();
        }

        private void NameMethod()
        {
            A a = new A();

            Debug.LogError(55);
        }

        public class A
        {
            
        }
        
        public class B : A
        {
            
        }
    }
}
