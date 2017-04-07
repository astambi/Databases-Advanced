namespace MassDefect.Import
{
    using Data.Store;
    using Models.DTO;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public static class XmlImport
    {
        public static void ImportAnomalyVictims()
        {
            XDocument xml = XDocument.Load("../../../datasets/new-anomalies.xml");
            IEnumerable<XElement> anomalies = xml.Root.Elements();

            var anomaliesWithVictims = new List<AnomalyWithVictimDto>();
            foreach (XElement anomaly in anomalies)
            {                
                var anomalyWithVictims = new AnomalyWithVictimDto()
                {
                    OriginPlanet = anomaly.Attribute("origin-planet")?.Value,
                    TeleportPlanet = anomaly.Attribute("teleport-planet")?.Value,
                    Victims = anomaly.Element("victims")
                            .Elements()     // victim
                            .Select(e => e.Attribute("name").Value)
                            .ToList()
                };
                anomaliesWithVictims.Add(anomalyWithVictims);
            }

            AnomalyStore.AddNewAnomaliesWithVictims(anomaliesWithVictims);
        }
    }
}
