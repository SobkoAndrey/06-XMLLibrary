using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06_XMLLibrary.LibraryElements
{
    public class ExtendedLibraryElement : BaseLibraryElement
    {
        public string PublicationPlace { get; set; }
        public string PublisherName { get; set; }
        public int PublishYear { get; set; }
    }
}
