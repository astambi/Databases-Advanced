namespace MassDefect.Export
{
    using Data.Store;
    using Models.DTO;
    using System.Collections.Generic;
    using System.Xml.Linq;

    public class XmlExport
    {
        public static void ExportAnomaliesAndVictims()
        {
            List<AnomaliesWithVictimsExportDto> anomalies = AnomalyStore.GetAnomaliesWithVictims();

            XDocument xmlDoc = new XDocument();
            XElement anomaliesXml = new XElement("anomalies");

            foreach (var anomaly in anomalies)
            {
                // Anomaly XML
                XElement anomalyXml = new XElement("anomaly");
                anomalyXml.SetAttributeValue("id", anomaly.Id);
                anomalyXml.SetAttributeValue("origin-planet", anomaly.OriginPlanet);
                anomalyXml.SetAttributeValue("teleport-planet", anomaly.TeleportPlanet);
                
                // Victims XML
                XElement victims = new XElement("victims");
                foreach (var victim in anomaly.Victims)
                {
                    XElement victimXml = new XElement("victim");
                    victimXml.SetAttributeValue("name", victim);
                    victims.Add(victimXml);
                }

                anomalyXml.Add(victims);
                anomaliesXml.Add(anomalyXml);
            }
            xmlDoc.Add(anomaliesXml);

            xmlDoc.Save("../../Export/anomalies.xml");
        }
    }
}
