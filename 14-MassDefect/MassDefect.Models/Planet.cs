namespace MassDefect.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Planet
    {
        public Planet()
        {
            this.Persons = new HashSet<Person>();
            this.OriginatingAnomalies = new HashSet<Anomaly>();
            this.TargettingAnomalies = new HashSet<Anomaly>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int SunId { get; set; }
        public virtual Star Sun { get; set; }

        public int SolarSystemId { get; set; }
        public virtual SolarSystem SolarSystem { get; set; }

        public virtual ICollection<Person> Persons { get; set; }
        public virtual ICollection<Anomaly> OriginatingAnomalies { get; set; }
        public virtual ICollection<Anomaly> TargettingAnomalies { get; set; }
    }
}
