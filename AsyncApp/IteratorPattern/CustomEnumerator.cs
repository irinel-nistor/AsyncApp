using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AsyncApp.IteratorPattern
{
    public class CustomEnumerator /*: IEnumerator*/
    {
        private string[] array;
        private int currentIndex;

        public CustomEnumerator()
        {
            array = new string[5] { "exemple 1", "example 2", "example 3", "example 4", "example 5" };
            currentIndex = -1;
        }

        public object Current => array[currentIndex];

        public bool MoveNext()
        {
            if (currentIndex == array.Length - 1)
            {
                return false;
            }
            else
            {
                currentIndex++;
                return true;
            }
        }

        public void Reset()
        {
            currentIndex = -1;
        }
    }
}
