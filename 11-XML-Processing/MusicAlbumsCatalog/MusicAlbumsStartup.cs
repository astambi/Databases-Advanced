namespace MusicAlbumsCatalogInXML
{
    using System;
    using System.Xml.Linq;

    class MusicAlbumsStartup
    {
        static void Main(string[] args)
        {
            XDocument xmlDoc = CreateXMLDocument();                     // version 1
            ExportAndPrint(xmlDoc, "catalog1");

            XDocument catalogXmlDoc = CreateXmlDocumentFromElements();  // version 2
            ExportAndPrint(catalogXmlDoc, "catalog2");
        }

        private static void ExportAndPrint(XDocument xmlDoc, string fileName) // filename without extentsion
        {
            Console.WriteLine(xmlDoc);
            xmlDoc.Save($"../../Export/{fileName}.xml");
            xmlDoc.Save($"../../Export/{fileName}NoFormatting.xml", SaveOptions.DisableFormatting);
            Console.WriteLine($"\nXML Document exported as: \nExport/{fileName}.xml & Export/{fileName}NoFormatting.xml\n");
        }

        private static XDocument CreateXmlDocumentFromElements()
        {
            // Songs
            XElement songsAlbum1 = CreateCollection("songs");
            songsAlbum1.Add(
                CreateSong("Airbag", "4:44"),
                CreateSong("Paranoid Android", "6:23"),
                CreateSong("Subterranean Homesick Alien", "4:27"),
                CreateSong("Exit Music (For a Film)", "4:24"),
                CreateSong("Let Down", "4:59"));

            XElement songsAlbum2 = CreateCollection("songs");
            songsAlbum2.Add(
                CreateSong("Bloom", "5:15"),
                CreateSong("Morning Mr Magpie", "4:41"),
                CreateSong("Little by Little", "4:27"),
                CreateSong("Feral", "3:13"),
                CreateSong("Lotus Flower", "5:01"));

            // Albums
            XElement albums = CreateCollection("albums");
            albums.Add(
                CreateAlbum("OK Computer", "Radiohead", 1997, "EMI", "15.00", songsAlbum1),
                CreateAlbum("The King of Limbs", "Radiohead", 2011, "Radiohead", "5.00", songsAlbum2));

            // XML Document
            XDocument catalogXmlDoc = new XDocument();
            catalogXmlDoc.Add(albums);

            return catalogXmlDoc;
        }

        private static XElement CreateSong(string title, string duration)
        {
            return new XElement("song", new XElement("title", title), new XElement("duration", duration));
        }

        private static XElement CreateAlbum(string name, string artist, int year, string producer, string price, XElement songs)
        {
            return new XElement("album",
                        new XElement("name", name),
                        new XElement("artist", artist),
                        new XElement("year", year),
                        new XElement("producer", producer),
                        new XElement("price", price),
                        songs);
        }

        private static XElement CreateCollection(string collection)
        {
            return new XElement($"{collection}");
        }

        private static XDocument CreateXMLDocument()
        {
            XDocument xmlDoc = new XDocument();
            xmlDoc.Add(
                new XElement("albums",
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
