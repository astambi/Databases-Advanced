namespace MassDefect.Data
{
    public static class Utility
    {
        public static void InitializeDB()
        {
            using (MassDefectContext context = new MassDefectContext())
            {
                context.Database.Initialize(true);
            }
        }
    }
}
