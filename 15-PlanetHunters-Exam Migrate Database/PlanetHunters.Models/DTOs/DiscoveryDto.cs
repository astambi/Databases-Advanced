using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetHunters.Models.DTOs
{
    public class DiscoveryDto
    {
        public string DateMade { get; set; }
        public string Telescope { get; set; }
        public ICollection<string> Stars { get; set; }
        public ICollection<string> Planets { get; set; }
        public ICollection<string> Pioneers { get; set; }
        public ICollection<string> Observers { get; set; }

    }
}
