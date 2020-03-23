using AsyncApp.AwaitablePattern;
using AsyncApp.Helpers;
using AsyncApp.IteratorPattern;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AsyncApp.Program
{
    public class IteratorProgram
    {
        public void Run()
        {
            // Enumerator and Enumerable
            var enumerable = new CustomEnumerable();
            foreach (var element in enumerable)
            {
                Console.WriteLine(element);
            }

            var enumerator = enumerable.GetEnumerator();
            for (var i = 0; i < 10; i++)
            {
                if (!enumerator.MoveNext()) break;
                //Console.WriteLine(enumerator.Current);
            }
        }
    }
}
