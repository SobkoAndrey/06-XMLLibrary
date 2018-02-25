using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06_XMLLibrary.LibraryElements
{
    public class BookLibraryElement : ExtendedLibraryElement
    {
        public string Authors { get; set; }
        public string ISBN { get; set; }
    }
}
