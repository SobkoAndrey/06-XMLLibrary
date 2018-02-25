using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06_XMLLibrary.LibraryElements
{
    public class RootLibraryElement
    {
        public List<object> Elements { get; set; }

        public RootLibraryElement()
        {
            Elements = new List<object>();
        }
    }
}
