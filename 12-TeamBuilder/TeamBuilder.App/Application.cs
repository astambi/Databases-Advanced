namespace TeamBuilder.App
{
    using Core;

    class Application
    {
        static void Main(string[] args)
        {
            Engine engine = new Engine(new CommandDispatcher());
            engine.Run();
        }
    }
}
