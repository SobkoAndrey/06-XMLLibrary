using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06_XMLLibrary.LibraryElements
{
    public class NewspaperLibraryElement : ExtendedLibraryElement
    {
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public string ISSN { get; set; }
    }
}
