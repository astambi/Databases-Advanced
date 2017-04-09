using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetHunters.Models.DTOs
{
    public class StarExportDto
    {
        public DateTime DiscoveryDate { get; set; }
        public string Telescope { get; set; }
        public ICollection<AstronomerExportDto> Astronomers { get; set; }
        public List<StarDto> Stars { get; set; }


    }
}
