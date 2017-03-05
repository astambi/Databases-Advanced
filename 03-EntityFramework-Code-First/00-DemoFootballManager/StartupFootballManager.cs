using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoFootballManager.Models;

namespace DemoFootballManager
{
    class StartupFootballManager
    {
        static void Main(string[] args)
        {
            FootballManagerContext context = new FootballManagerContext();

            context.Database.Initialize(true);

            context.Players.Add(new Player()
            {
                Name = "Messi",
                Team = new Team() { Name = "Barcelona" }
            });
            context.Leagues.Add(new League()
            {
                Name = "League A"
            });
            context.SaveChanges();

            //var trans = context.Database.Connection.BeginTransaction();
            //trans.Rollback();
            //trans.Commit();
        }
    }
}