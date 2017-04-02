namespace MusicAlbumsCatalogInXML
{
    using System;
    using System.Xml.Linq;

    class MusicAlbumsCatalog
    {
        static void Main(string[] args)
        {
            XDocument xmlDoc = CreateXMLDocument();
            PrintAndExportToFile(xmlDoc);
        }

        private static void PrintAndExportToFile(XDocument xmlDoc)
        {
            Console.WriteLine(xmlDoc);

            xmlDoc.Save("../../ExportsXml/catalog.xml");
            xmlDoc.Save("../../ExportsXml/catalogNoFormatting.xml", SaveOptions.DisableFormatting);
            Console.WriteLine("\nCatalog exported as:\nExportsXml/catalog.xml & ExportsXml/catalogNoFormatting.xml");
        }

        private static XDocument CreateXMLDocument()
        {
            XDocument xmlDoc = new XDocument();
            xmlDoc.Add(
                new XElement("catalog",
                    new XElement("album",
                        new XElement("name", "OK Computer"),
                        new XElement("artist", "Radiohead"),
                        new XElement("year", "1997"),
                        new XElement("producer", "EMI"),
                        new XElement("price", "15.00"),
                        new XElement("songs",
                            new XElement("song",
                                new XElement("title", "Airbag"),
                                new XElement("duration", "4:44")),
                            new XElement("song",
                                new XElement("title", "Paranoid Android"),
                                new XElement("duration", "6:23")),
                            new XElement("song",
                                new XElement("title", "Subterranean Homesick Alien"),
                                new XElement("duration", "4:27")),
                            new XElement("song",
                                new XElement("title", "Exit Music (For a Film)"),
                                new XElement("duration", "4:24")),
                            new XElement("song",
                                new XElement("title", "Let Down"),
                                new XElement("duration", "4:59"))
                        )
                    ),
                    new XElement("album",
                        new XElement("name", "The King of Limbs"),
                        new XElement("artist", "Radiohead"),
                        new XElement("year", "2011"),
                        new XElement("producer", "Radiohead"),
                        new XElement("price", "5.00"),
                        new XElement("songs",
                            new XElement("song",
                                new XElement("title", "Bloom"),
                                new XElement("duration", "5:15")),
                            new XElement("song",
                                new XElement("title", "Morning Mr Magpie"),
                                new XElement("duration", "4:41")),
                            new XElement("song",
                                new XElement("title", "Little by Little"),
                                new XElement("duration", "4:27")),
                            new XElement("song",
                                new XElement("title", "Feral"),
                                new XElement("duration", "3:13")),
                            new XElement("song",
                                new XElement("title", "Lotus Flower"),
                                new XElement("duration", "5:01"))
                        )
                    )
                ));
            return xmlDoc;
        }
    }
}
