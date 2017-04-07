namespace MassDefect.Data
{
    using Models;
    using System.Linq;

    public static class CommandHelpers
    {
        public static SolarSystem GetSolarSystemByName(MassDefectContext context, string name)
        {
            return context.SolarSystems.FirstOrDefault(s => s.Name == name);
        }

        public static Star GetSunByName(MassDefectContext context, string name)
        {
            return context.Stars.FirstOrDefault(s => s.Name == name);
        }

        public static Planet GetPlanetByName(MassDefectContext context, string name)
        {
            return context.Planets.FirstOrDefault(p => p.Name == name);
        }

        public static Person GetPersonByName(MassDefectContext context, string name)
        {
            return context.Persons.FirstOrDefault(p => p.Name == name);
        }

        public static Anomaly GetAnomalyById(MassDefectContext context, int id)
        {
            return context.Anomalies.FirstOrDefault(a => a.Id == id);
        }

        public static Anomaly GetAnomalyByPlanetsId(MassDefectContext context, int originPlanetId, int teleportPlanetId)
        {
            return context.Anomalies
                .FirstOrDefault(a => a.OriginPlanetId == originPlanetId &&
                                     a.TeleportPlanetId == teleportPlanetId);
        }
    }
}
