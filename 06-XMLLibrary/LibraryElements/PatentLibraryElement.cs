using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06_XMLLibrary.LibraryElements
{
    public class PatentLibraryElement : BaseLibraryElement
    {
        public string Creator { get; set; }
        public string Country { get; set; }
        public int RegistrationNumber { get; set; }
        public DateTime ApplicationDate { get; set; }
        public DateTime PublicationDate { get; set; }
    }
}
