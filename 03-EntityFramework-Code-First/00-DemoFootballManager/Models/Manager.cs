using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoFootballManager.Models
{
    public class Manager
    {
        public int ManagerId { get; set; }
        public string Name { get; set; }
        public int TeamId { get; set; }
        public virtual Team Team { get; set; }
    }
}
