using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _06_XMLLibrary.LibraryElements;
using System.Xml;
using System.Collections;
using System.Xml.Linq;
using System.Globalization;
using System.Threading;

namespace _06_XMLLibrary
{
    public class XMLWorker
    {
        public IEnumerable ReadXMLFromFile(string path)
        {
            var stream = new FileStream(path, FileMode.Open);

            return ReadXMLFromStream(stream);
        }

        public IEnumerable ReadXMLFromStream(Stream stream)
        {
            var reader = XmlReader.Create(stream);

            while (reader.ReadToFollowing("libraryElement"))
            {
                var element = XElement.ReadFrom(reader) as XElement;
                var type = element.FirstAttribute.Value;

                switch (type)
                {
                    case "book":

                        if (!BookXmlValidate(element))
                            break;

                        var book = new BookLibraryElement();
                        book.Name = element.Element("name").Value;
                        book.Authors = element.Element("authors").Value;
                        book.PublicationPlace = element.Element("city").Value;
                        book.PublisherName = element.Element("publisher").Value;
                        book.PublishYear = XmlConvert.ToInt32(element.Element("year").Value);
                        book.PageCount = XmlConvert.ToInt32(element.Element("pageCount").Value);
                        book.Comment = element.Element("comment").Value;
                        book.ISBN = element.Element("ISBN").Value;

                        yield return book;
                        break;
                    case "newspaper":

                        if (!NewspaperXmlValidate(element))
                            break;

                        var newspaper = new NewspaperLibraryElement();
                        newspaper.Name = element.Element("name").Value;
                        newspaper.Number = XmlConvert.ToInt32(element.Element("number").Value);
                        newspaper.PublicationPlace = element.Element("city").Value;
                        newspaper.PublisherName = element.Element("publisher").Value;
                        newspaper.PublishYear = XmlConvert.ToInt32(element.Element("year").Value);
                        newspaper.PageCount = XmlConvert.ToInt32(element.Element("pageCount").Value);
                        newspaper.Comment = element.Element("comment").Value;
                        newspaper.ISSN = element.Element("ISSN").Value;
                        newspaper.Date = XmlConvert.ToDateTime(element.Element("date").Value, "yyyy-mm-dd");

                        yield return newspaper;
                        break;
                    case "patent":

                        if (!PatentXmlValidate(element))
                            break;

                        var patent = new PatentLibraryElement();
                        patent.Name = element.Element("name").Value;
                        patent.Creator = element.Element("creator").Value;
                        patent.Country = element.Element("country").Value;
                        patent.RegistrationNumber = XmlConvert.ToInt32(element.Element("registrationNumber").Value);
                        patent.PageCount = XmlConvert.ToInt32(element.Element("pageCount").Value);
                        patent.Comment = element.Element("comment").Value;
                        patent.ApplicationDate = XmlConvert.ToDateTime(element.Element("applicationDate").Value, "yyyy-mm-dd");
                        patent.PublicationDate = XmlConvert.ToDateTime(element.Element("publicationDate").Value, "yyyy-mm-dd");

                        yield return patent;
                        break;
                }

            }

        }

        public void WriteToXMLFile(RootLibraryElement library, string filePath)
        {
            var lib = new XElement("root");

            foreach (var el in library.Elements)
            {
                if (el is BookLibraryElement)
                {
                    var book = el as BookLibraryElement;
                    if (!BookValidate(book))
                        break;

                    var bookElement = new XElement("libraryElement", new XAttribute("type", "book"));
                    bookElement.Add(new XElement("name", book.Name));
                    bookElement.Add(new XElement("authors", book.Authors));
                    bookElement.Add(new XElement("city", book.PublicationPlace));
                    bookElement.Add(new XElement("publisher", book.PublisherName));
                    bookElement.Add(new XElement("year", book.PublishYear));
                    bookElement.Add(new XElement("pageCount", book.PageCount));
                    bookElement.Add(new XElement("comment", book.Comment));
                    bookElement.Add(new XElement("ISBN", book.ISBN));

                    lib.Add(bookElement);
                }
                else if (el is NewspaperLibraryElement)
                {
                    var paper = el as NewspaperLibraryElement;
                    if (!NewspaperValidate(paper))
                        break;

                    var paperElement = new XElement("libraryElement", new XAttribute("type", "newspaper"));
                    paperElement.Add(new XElement("name", paper.Name));
                    paperElement.Add(new XElement("city", paper.PublicationPlace));
                    paperElement.Add(new XElement("publisher", paper.PublisherName));
                    paperElement.Add(new XElement("year", paper.PublishYear));
                    paperElement.Add(new XElement("pageCount", paper.PageCount));
                    paperElement.Add(new XElement("comment", paper.Comment));
                    paperElement.Add(new XElement("number", paper.Number));
                    paperElement.Add(new XElement("date", paper.Date.ToString("yyyy-mm-dd", new CultureInfo("us-US").DateTimeFormat)));
                    paperElement.Add(new XElement("ISSN", paper.ISSN));

                    lib.Add(paperElement);
                }
                else if (el is PatentLibraryElement)
                {
                    var patent = el as PatentLibraryElement;
                    if (!PatentValidate(patent))
                        break;

                    var patentElement = new XElement("libraryElement", new XAttribute("type", "patent"));
                    patentElement.Add(new XElement("name", patent.Name));
                    patentElement.Add(new XElement("creator", patent.Creator));
                    patentElement.Add(new XElement("country", patent.Country));
                    patentElement.Add(new XElement("registrationNumber", patent.RegistrationNumber));
                    patentElement.Add(new XElement("applicationDate", patent.ApplicationDate.ToString("yyyy-mm-dd", new CultureInfo("us-US").DateTimeFormat)));
                    patentElement.Add(new XElement("publicationDate", patent.PublicationDate.ToString("yyyy-mm-dd", new CultureInfo("us-US").DateTimeFormat)));
                    patentElement.Add(new XElement("pageCount", patent.PageCount));
                    patentElement.Add(new XElement("comment", patent.Comment));

                    lib.Add(patentElement);
                }
            }

            lib.Save(filePath);
        }

        #region Xml Validation
        private bool BaseXmlValidate(XElement element)
        {
            if (string.IsNullOrEmpty(element.Element("name").Value))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool ExtendedXmlValidate(XElement element)
        {
            var baseValidate = BaseXmlValidate(element);

            if (string.IsNullOrEmpty(element.Element("publisher").Value))
            {
                return false;
            }
            else
            {
                return baseValidate;
            }
        }

        private bool BookXmlValidate(XElement element)
        {
            var extendedValidate = ExtendedXmlValidate(element);

            if (string.IsNullOrEmpty(element.Element("authors").Value) || string.IsNullOrEmpty(element.Element("ISBN").Value))
            {
                return false;
            }
            else
            {
                return extendedValidate;
            }
        }

        private bool NewspaperXmlValidate(XElement element)
        {
            var extendedValidate = ExtendedXmlValidate(element);

            try
            {
                var date = XmlConvert.ToDateTime(element.Element("date").Value, "yyyy-mm-dd");
            }
            catch (Exception)
            {
                return false;
            }

            if (string.IsNullOrEmpty(element.Element("ISSN").Value))
            {
                return false;
            }
            else
            {
                return extendedValidate;
            }
        }

        private bool PatentXmlValidate(XElement element)
        {
            var baseValidate = BaseXmlValidate(element);

            try
            {
                var number = XmlConvert.ToInt64(element.Element("registrationNumber").Value);
            }
            catch (Exception)
            {
                return false;
            }


            if (string.IsNullOrEmpty(element.Element("creator").Value))
            {
                return false;
            }
            else
            {
                return baseValidate;
            }
        }

        #endregion

        #region Element Validation

        private bool BaseValidate(BaseLibraryElement element)
        {
            if (string.IsNullOrEmpty(element.Name))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool ExtendedValidate(ExtendedLibraryElement element)
        {
            var baseValidate = BaseValidate(element);

            if (string.IsNullOrEmpty(element.PublisherName))
            {
                return false;
            }
            else
            {
                return baseValidate;
            }
        }

        private bool BookValidate(BookLibraryElement element)
        {
            var extendedValidate = ExtendedValidate(element);

            if (string.IsNullOrEmpty(element.Authors) || string.IsNullOrEmpty(element.ISBN))
            {
                return false;
            }
            else
            {
                return extendedValidate;
            }
        }

        private bool NewspaperValidate(NewspaperLibraryElement element)
        {
            var extendedValidate = ExtendedValidate(element);

            if (string.IsNullOrEmpty(element.ISSN))
            {
                return false;
            }
            else
            {
                return extendedValidate;
            }
        }

        private bool PatentValidate(PatentLibraryElement element)
        {
            var baseValidate = BaseValidate(element);

            if (string.IsNullOrEmpty(element.Creator))
            {
                return false;
            }
            else
            {
                return baseValidate;
            }
        }

        #endregion
    }
}
