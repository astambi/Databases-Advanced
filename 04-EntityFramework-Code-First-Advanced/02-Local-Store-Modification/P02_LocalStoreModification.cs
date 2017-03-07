using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_Local_Store_Modification
{
    class P02_LocalStoreModification
    {
        static void Main(string[] args)
        {
            // script to restore the original db schema & data => sql/DbRestore.sql

            SqlConnection connection = new SqlConnection("Server =.;Database=Products.CodeFirst;Integrated Security = true");
            connection.Open();
            Console.WriteLine("Adding columns [Quantity] and [Weight] to Database [Products.CodeFirst] and setting default values");

            using (connection)
            {
                // add new nullable columns
                string sqlAddColumns = @"ALTER TABLE Products ADD Quantity FLOAT NULL, Weight FLOAT NULL";
                SqlCommand cmdAddColumns = new SqlCommand(sqlAddColumns, connection);
                cmdAddColumns.ExecuteNonQuery();

                // update all null values in the new columns to default values = 0, then make cols not nullable
                string sqlUpdateCols = File.ReadAllText(@"../../sql/ModifyNullCols.sql");
                SqlCommand cmdUpdateCols = new SqlCommand(sqlUpdateCols, connection);
                cmdUpdateCols.ExecuteNonQuery();
            }
        }
    }
}
