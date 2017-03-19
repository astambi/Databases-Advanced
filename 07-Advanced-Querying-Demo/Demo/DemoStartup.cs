using Demo.Data;
using Demo.Models;
using EntityFramework.Extensions;
using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace Demo
{
    class DemoStartup
    {
        static void Main(string[] args)
        {
            QueryContext context = new QueryContext();
            Console.WriteLine("Initializing database");
            context.Database.Initialize(true);

            //SqlQueryWithoutParam(context);
            //SqlQueryWithParam(context);
            //SqlQueryWithPlaceholders(context);
            //AttachingDetachingObjects(context);
            //WorkingWithDetachedEntities(context);
            //BulkUpdateDelete(context);
            //CreateProcedure(context);
            //ExecuteProcedureWithParam(context);
            //EagerLoading(context);
            //LazyLoading(context);
            //ExplicitLoading(context);
            //OptimisticConcurrency();

            /* Cascade Delete
             * Make Client in Order Optional
             * Disable Seed for orders based on ClientId
             * Add migration
             * or
             * change OnModelCreating in Context => modelBuilder ...WillCascadeOnDelete(false)
             */
        }

        private static void OptimisticConcurrency()
        {
            QueryContext contextFirst = new QueryContext();
            var lastProductFirstUser = contextFirst.Products
                .OrderByDescending(p => p.Id).First();
            lastProductFirstUser.Name = "Changed by the First User";

            QueryContext contextSecond = new QueryContext();
            var lastProductSecondUser = contextSecond.Products
                .OrderByDescending(p => p.Id).First();
            lastProductSecondUser.Name = "Changed by the Second User";

            // Optimistic concurrency by default
            // Conflicting changes: last wins
            contextFirst.SaveChanges();
            contextSecond.SaveChanges(); // second user wins

            // With a [ConcurrencyCheck] attribute on the property 
            // => first wins & exception for the second user

            // when using context ends => the lock is resolved
        }

        private static void ExplicitLoading(QueryContext context)
        {
            Order order = context.Orders.Find(4);

            context.Entry(order)
                    .Collection(o => o.Products)
                    .Load();
            var collection = context.Orders.Local; // 1 order with 2 products

            foreach (Product p in collection.First().Products)
            {
                Console.WriteLine(p.Name);
            }
        }

        private static void LazyLoading(QueryContext context)
        {
            Order order = context.Orders
                .Include(o => o.Client) // Exception if Order does NOT have a virtual Client! & NO Include!
                .FirstOrDefault();
            Console.WriteLine(order.Client.Name);
        }

        private static void EagerLoading(QueryContext context)
        {
            Order order = context.Orders
                .Include(o => o.Client) // obligatory for non-virtual properties
                .FirstOrDefault();
            Console.WriteLine(order.Client.Name);
        }

        private static void ExecuteProcedureWithParam(QueryContext context)
        {
            context.Database
                .ExecuteSqlCommand("EXEC dbo.usp_UpdateAge @age",
                                    new SqlParameter("@age", 10));
            context.Database
                .ExecuteSqlCommand("usp_UpdateAge @age",
                                    new SqlParameter("@age", 1));
        }

        private static void CreateProcedure(QueryContext context)
        {
            string sqlCreateProcUpdateAge = File.ReadAllText("../../SqlQueries/CreateProc_UpdateAge.sql");
            context.Database.ExecuteSqlCommand(sqlCreateProcUpdateAge);
        }

        private static void BulkUpdateDelete(QueryContext context)
        {
            //install Entity Framework Extended
            context.Clients
                .Where(c => c.Address.StartsWith("Plovdiv"))
                .Update(c => new Client() { Address = "Varna" }); // updates address only

            context.Clients
                .Where(c => c.Address == null)
                .Delete();

            context.Clients
                .Update(c => new Client() { Age = 18 }); // update age only
        }

        private static void WorkingWithDetachedEntities(QueryContext context)
        {
            Console.WriteLine("\nClient from non-existing context");
            var loadedClient = LoadAndDetach();

            Console.WriteLine($"{loadedClient.Name} - {context.Entry(loadedClient).State}"); // detached

            loadedClient.Address = "Modified Address " + DateTime.Now;
            context.Entry(loadedClient).State = System.Data.Entity.EntityState.Modified;  // or Unchanged
            Console.WriteLine($"{loadedClient.Name} - {context.Entry(loadedClient).State}"); // modified

            context.SaveChanges();
            Console.WriteLine($"{loadedClient.Name} - {context.Entry(loadedClient).State}"); // unchanged (but added)
        }

        static Client LoadAndDetach()
        {
            using (QueryContext context = new QueryContext())
            {
                var client = context.Clients.FirstOrDefault();
                return client;
            }
        }

        private static void AttachingDetachingObjects(QueryContext context)
        {
            // existing client
            Console.WriteLine("\nExisting client");
            Client client = context.Clients.FirstOrDefault();

            Console.WriteLine($"{client.Name} - {context.Entry(client).State}"); // unchanged

            context.Entry(client).State = System.Data.Entity.EntityState.Deleted;
            Console.WriteLine($"{client.Name} - {context.Entry(client).State}"); // deleted

            context.SaveChanges();
            Console.WriteLine($"{client.Name} - {context.Entry(client).State}"); ; // detached

            client.Name = "New Name";
            context.SaveChanges();
            Console.WriteLine($"{client.Name} - {context.Entry(client).State}"); // detached

            // new client
            Console.WriteLine("\nNew client");
            Client newClient = new Client() { Name = "New Client" };
            Console.WriteLine($"{newClient.Name} - {context.Entry(newClient).State}"); // detached

            newClient.Name = "New Client Modified Name";
            Console.WriteLine($"{newClient.Name} - {context.Entry(newClient).State}");  // detached

            context.Entry(newClient).State = System.Data.Entity.EntityState.Added;
            Console.WriteLine($"{newClient.Name} - {context.Entry(newClient).State}");  // added

            context.SaveChanges();
            Console.WriteLine($"{newClient.Name} - {context.Entry(newClient).State}");  // unchanged

            newClient.Name = "New Modified name";
            Console.WriteLine($"{newClient.Name} - {context.Entry(newClient).State}");  // modified
        }

        private static void SqlQueryWithPlaceholders(QueryContext context)
        {
            context.Database
                .SqlQuery<Client>(@"
                    SELECT * FROM Clients 
                    WHERE Name LIKE {0}",
                    "%petar%")
                .ToList()
                .ForEach(c => Console.WriteLine(c.Name));
        }

        private static void SqlQueryWithParam(QueryContext context)
        {
            SqlParameter nameParam = new SqlParameter("@nameParam", "%" + "petar" + "%");
            context.Database
                .SqlQuery<Client>(@"
                    SELECT * FROM Clients 
                    WHERE Name LIKE @nameParam",
                    nameParam)
                .ToList()
                .ForEach(c => Console.WriteLine($"{c.Name} - {c.Address}"));

            context.Database
                .SqlQuery<Client>(@"
                    SELECT * FROM Clients 
                    WHERE Name LIKE @nameParam",
                    new SqlParameter("@nameParam", "%petar%")) // avoid exception with itentical param names
                .ToList()
                .ForEach(c => Console.WriteLine(c.Name));
        }

        private static void SqlQueryWithoutParam(QueryContext context)
        {
            context.Clients.FirstOrDefault().Address = "Sofia, 15-17 Tintiava";
            context.SaveChanges();

            context.Database.SqlQuery<Client>("SELECT * FROM Clients")
                .ToList()
                .ForEach(c => Console.WriteLine($"{c.Name} - {c.Address}"));
        }
    }
}
