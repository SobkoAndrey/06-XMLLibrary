using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _06_XMLLibrary;
using _06_XMLLibrary.LibraryElements;
using System.IO;
using System.Linq;

namespace XMLLibraryTests
{
    [TestClass]
    public class XMLWorkerTests
    {
        RootLibraryElement root;
        public XMLWorkerTests()
        {
            root = new RootLibraryElement();

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
        }

        [TestMethod]
        public void WriteToXMLFileCreateFile()
        {
            var worker = new XMLWorker();

            if (File.Exists(@"C:\XMLFileWrite.xml"))
                File.Delete(@"C:\XMLFileWrite.xml");

            worker.WriteToXMLFile(root, @"C:\XMLFileWrite.xml");

            Assert.IsTrue(File.Exists(@"C:\XMLFileWrite.xml"));
        }

        [TestMethod]
        public void ReadXMLFromFileWorksCorrectly()
        {
            var worker = new XMLWorker();
            var root2 = new RootLibraryElement();

            foreach (var el in worker.ReadXMLFromFile(@"C:\XMLFileWrite.xml"))
            {
                root2.Elements.Add(TryParse(el));
            }

            Assert.IsTrue(root2.Elements.Count == 3);
            Assert.IsInstanceOfType(root2.Elements.First(), typeof(BookLibraryElement));
            Assert.IsInstanceOfType(root2.Elements.Skip(1).First(), typeof(NewspaperLibraryElement));
            Assert.IsInstanceOfType(root2.Elements.Skip(2).First(), typeof(PatentLibraryElement));
            Assert.AreEqual("TestBook", (root2.Elements.First() as BookLibraryElement).Name);
            Assert.AreEqual("TestPaper", (root2.Elements.Skip(1).First() as NewspaperLibraryElement).Name);
            Assert.AreEqual("TestPatent", (root2.Elements.Skip(2).First() as PatentLibraryElement).Name);
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
    }
}
