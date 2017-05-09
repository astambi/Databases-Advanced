namespace Stations.Models.DTOs
{
    using System.Collections.Generic;

    public class TrainImportDto
    {
        public TrainImportDto()
        {
            this.Seats = new HashSet<SeatImportDto>();
        }

        public string TrainNumber { get; set; }
        public TrainType? Type { get; set; }
        public ICollection<SeatImportDto> Seats { get; set; }
    }
}
