namespace PlanetHunters.Export
{
    using Data.Store;
    using Models.DTOs;
    using System.Collections.Generic;
    using System.Xml.Linq;

    public static class XmlExport
    {
        public static void ExportStars()
        {
            List<StarExportDto> starsDto = StarStore.GetStars();

            XDocument xmlDoc = new XDocument();
            XElement starsXml = new XElement("Stars");

            foreach (var starDto in starsDto)
            {
                foreach (var star in starDto.Stars)
                {
                    XElement starXml = new XElement("Star",
                        new XElement("Name", star.Name),
                        new XElement("Temperature", star.Temperature),
                        new XElement("StarSystem", star.StarSystem));

                    XElement discoveryInfo = new XElement("DiscoveryInfo",
                        new XAttribute("DiscoveryDate", $"{starDto.DiscoveryDate:yyyy-MM-dd}"),
                        new XAttribute("TelescopeName", starDto.Telescope));

                    foreach (var astronomer in starDto.Astronomers)
                    {
                        discoveryInfo.Add(new XElement("Astronomer",
                            astronomer.FirstName + " " + astronomer.LastName,
                            new XAttribute("Pioneer", astronomer.Role)));
                    }

                    starXml.Add(discoveryInfo);
                    starsXml.Add(starXml);
                }
            }
            xmlDoc.Add(starsXml);

            xmlDoc.Save("../../Export/stars.xml");
        }
    }
}
