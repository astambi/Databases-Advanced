namespace PlanetHunters.Import
{
    using Data.Store;
    using Models.DTOs;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public static class XmlImport
    {
        public static void ImportStars()
        {
            XDocument xmlDoc = LoadXmlFile("stars");
            var stars = xmlDoc.Root.Elements();

            List<StarDto> starsDto = new List<StarDto>();
            foreach (XElement star in stars)
            {
                var starDto = new StarDto()
                {
                    Name = star.Element("Name")?.Value,
                    Temperature = star.Element("Temperature")?.Value,
                    StarSystem = star.Element("StarSystem")?.Value
                };
                starsDto.Add(starDto);
            }

            StarStore.AddStars(starsDto);
        }

        public static void ImportDiscoveries()
        {
            XDocument xmlDoc = LoadXmlFile("discoveries");
            var discoveries = xmlDoc.Root.Elements();

            List<DiscoveryDto> discoveriesDto = new List<DiscoveryDto>();
            foreach (XElement discovery in discoveries)
            {
                var discoveryDto = new DiscoveryDto()
                {
                    DateMade = discovery.Attribute("DateMade")?.Value,
                    Telescope = discovery.Attribute("Telescope")?.Value,
                    Stars = discovery.Element("Stars")
                                    .Elements()
                                    .Select(e => e?.Value)
                                    .ToList(),
                    Planets = discovery.Element("Planets")
                                    .Elements()
                                    .Select(e => e?.Value)
                                    .ToList(),
                    Pioneers = discovery.Element("Pioneers")
                                    .Elements()
                                    .Select(e => e?.Value)
                                    .ToList(),
                    Observers = discovery.Element("Observers")
                                    .Elements()
                                    .Select(e => e?.Value)
                                    .ToList()
                };
                discoveriesDto.Add(discoveryDto);
            }

            DiscoveryStore.AddDiscoveries(discoveriesDto);
        }

        private static XDocument LoadXmlFile(string fileName)
        {
            return XDocument.Load($"../../../Import/{fileName}.xml");
        }
    }
}
