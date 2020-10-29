namespace GameEngine.Console.Commands
{
    public abstract class ConsoleCommand
    {
        public abstract string Name { get; }
        public abstract string Usage { get; }
        public abstract string Description { get; }
        public bool RequiresArgs { get { if (Usage == string.Empty) return false; return true; } }

        public abstract void Execute(string[] args);
    }
}
