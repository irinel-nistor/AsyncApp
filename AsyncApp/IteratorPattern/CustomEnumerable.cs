using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AsyncApp.IteratorPattern
{
    public class CustomEnumerable /*: IEnumerable*/
    {
        public CustomEnumerator GetEnumerator()
        {
            return new CustomEnumerator();
        }
    }
}
