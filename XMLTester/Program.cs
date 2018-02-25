using _06_XMLLibrary;
using _06_XMLLibrary.LibraryElements;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLTester
{
    class Program
    {

        static RootLibraryElement Read(string filePath)
        {
            var root = new RootLibraryElement();
            var worker = new XMLWorker();

            foreach (var el in worker.ReadXMLFromFile(filePath))
            {
                root.Elements.Add(TryParse(el));
            }

            return root;
        }

        static void Write()
        {
            var root = new RootLibraryElement();

            var book = new BookLibraryElement();
            book.Name = "TestBook";
            book.PageCount = 34;
            book.PublicationPlace = "Karaganda";
            book.PublisherName = "Fgfgf";
            book.PublishYear = 1943;
            book.ISBN = "5786v45677v76487";
            book.Comment = "fgkllrlljjklghhgfdhfhgfhgf";
            book.Authors = "Villain";

            root.Elements.Add(book);

            var paper = new NewspaperLibraryElement();
            paper.Name = "TestPaper";
            paper.PageCount = 25;
            paper.PublicationPlace = "Karaganda";
            paper.PublisherName = "Namenaem";
            paper.PublishYear = 2001;
            paper.ISSN = "456754547687";
            paper.Comment = "fgkоднакоhgf";
            paper.Date = DateTime.Now;
            paper.Number = 856;

            root.Elements.Add(paper);

            var patent = new PatentLibraryElement();
            patent.Name = "TestPatent";
            patent.PageCount = 125;
            patent.ApplicationDate = DateTime.Now.Date.AddDays(-2);
            patent.PublicationDate = DateTime.Now.Date.AddDays(-1);
            patent.Creator = "Леша изобрел что-то там";
            patent.Country = "Гваделупа";
            patent.Comment = "вот какая она";
            patent.RegistrationNumber = 766456;

            root.Elements.Add(patent);

            var worker = new XMLWorker();
            worker.WriteToXMLFile(root, @"D:\XMLFileWriteTest.xml");

            Console.ReadKey();
        }

        static void Write(RootLibraryElement from, string toFile)
        {
            var worker = new XMLWorker();
            worker.WriteToXMLFile(from, toFile);
        }

        private static BaseLibraryElement TryParse(object element)
        {
            if (element is BookLibraryElement)
            {
                return element as BookLibraryElement;
            }
            else if (element is NewspaperLibraryElement)
            {
                return element as NewspaperLibraryElement;
            }
            else if (element is PatentLibraryElement)
            {
                return element as PatentLibraryElement;
            }
            else
            {
                throw new Exception("Invalid format");
            }
        }

        static void Main(string[] args)
        {
            var root = Read(@"D:\XMLFileTest.xml");
            Write(root, @"D:\XMLFileWrite.xml");
        }
    }
}
