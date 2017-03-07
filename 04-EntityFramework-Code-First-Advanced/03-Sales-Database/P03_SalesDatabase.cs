using _03_Sales_Database.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_Sales_Database
{
    class P03_SalesDatabase
    {
        static void Main(string[] args)
        {
            SalesContext context = new SalesContext();

            Console.WriteLine("Solutions to Problems:\n" +
                "3. Sales Database\n" +
                "4. Products Migration\n" +
                "5. Sales Migration\n" +
                "6. Customers Migration (automatic migration)\n" +
                "7. Add Default Age\n" +
                "8. *Script Migration\n\n" +
                "Rollback migrations to see previous versions of the database.\n" +
                "Please comment/ uncommnet added/ removed props in Models accordingly\n\n" + 
                "To rollback migrations in PackageManagerConsole type \n" +
                "Update-Database -TargetMigration [MigrationName]\n");

            context.Products.Add(new Product() { Name = "test"});
        }
    }
}