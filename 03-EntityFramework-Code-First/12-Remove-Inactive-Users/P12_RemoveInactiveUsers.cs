using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12_Remove_Inactive_Users
{
    class P12_RemoveInactiveUsers
    {
        static void Main(string[] args)
        {
            UsersContext context = new UsersContext();

            DateTime date = GetDate();
            List<User> inactiveUsers = context.Users
                .Where(u => u.LastTimeLoggedIn <= date && u.IsDeleted == false).ToList();
            inactiveUsers.ForEach(u => u.IsDeleted = true);
            context.SaveChanges();

            Console.WriteLine($"{inactiveUsers.Count} users have been deleted");
        }

        private static DateTime GetDate()
        {
            string[] months = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            int day = -1, month = -1, year = -1;
            while (day == -1 || month == -1 || year == -1)
            {
                Console.Write("Enter a date [dd mmm yyyy]: ");
                string[] dateTokens = Console.ReadLine().Split(' ').ToArray();
                day = int.Parse(dateTokens[0]);
                year = int.Parse(dateTokens[2]);
                for (int i = 0; i < months.Length; i++)
                {
                    if (months[i] == dateTokens[1])
                    {
                        month = i + 1; break;
                    }
                }
            }
            return new DateTime(year, month, day);
        }
    }
}