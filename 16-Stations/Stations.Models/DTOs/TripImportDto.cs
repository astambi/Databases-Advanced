namespace Stations.Models.DTOs
{
    public class TripImportDto
    {
        public string Train { get; set; }
        public string OriginStation { get; set; }
        public string DestinationStation { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
        public StatusType Status { get; set; }
        public string TimeDifference { get; set; }
    }
}
