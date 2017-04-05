namespace TeamBuilder.App
{
    using Core;
    using Data;

    class Application
    {
        static void Main(string[] args)
        {
            //using (TeamBuilderContext context = new TeamBuilderContext())
            //{
            //    context.Database.Initialize(true);
            //}

            Engine engine = new Engine(new CommandDispatcher());
            engine.Run();
        }
    }
}
