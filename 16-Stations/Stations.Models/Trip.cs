namespace Stations.Models
{
    using System;
    using System.Collections.Generic;

    public enum StatusType { OnTime, Delayed, Early }

    public class Trip
    {
        public Trip()
        {
            this.Status = StatusType.OnTime;
            this.Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }

        public int OriginStationId { get; set; }
        public virtual Station OriginStation { get; set; }

        public int DestinationStationId { get; set; }
        public virtual Station DestinationStation { get; set; }

        public DateTime DepartureTime { get; set; }

        public DateTime ArrivalTime { get; set; }

        public int TrainId { get; set; }
        public virtual Train Train { get; set; }
                
        public StatusType Status { get; set; }  // Default value = OnTime

        public TimeSpan TimeDifference { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
