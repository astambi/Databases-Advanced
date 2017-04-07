namespace MassDefect.Client
{
    using Data;

    class MassDefectStartup
    {
        static void Main(string[] args)
        {
            /* Add Reference in Client to EntityFramework 
             * OR
             * Copy EntityFramework.SqlServer.dll 
             * from Data/bin/Debug to Client/bin/Debug
             */

            Utility.InitializeDB();
        }
    }
}
