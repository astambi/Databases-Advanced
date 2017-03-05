using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11_Users_By_Email_Provider
{
    class P11_UsersByEmailProvider
    {
        static void Main(string[] args)
        {
            UsersContext context = new UsersContext();

            // create database Users => from Problem 08.Create User
            context.Database.Initialize(true);

            // run SQL script to populated db Users 

            Console.Write("Enter email provider: "); ;
            string emailProvider = Console.ReadLine();
            context.Users
                .Where(u => u.Email.EndsWith(emailProvider)).ToList()
                .ForEach(u => Console.WriteLine($"{u.Username} {u.Email}"));
        }
    }
}
