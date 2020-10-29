using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GameEngine.Console.Commands;

namespace GameEngine.Console
{
    public static class ConsoleCommandHandler
    {
        public static Dictionary<string, ConsoleCommand> Commands = new Dictionary<string, ConsoleCommand>();
        private static bool loaded = false;

        public static void Init()
        {
            if (!loaded)
            {
                IEnumerable<Type> types = Assembly.GetAssembly(typeof(ConsoleCommand)).GetTypes()
                .Where(type => !type.IsAbstract && type.IsClass && type.IsSubclassOf(typeof(ConsoleCommand)));

                foreach (Type command in types)
                {
                    ConsoleCommand instance = Activator.CreateInstance(command) as ConsoleCommand;
                    Commands.Add(instance.Name.Trim(), instance);
                }

                Logger.AllowInput();
                loaded = true;
            }
        }

        public static void ParseInput(string msg)
        {
            string[] input = msg.Split();

            if (input.Length == 0
                || input == null
                || !Commands.ContainsKey(input[0]))
            {
                Logger.Print("Command not Recognized.");
                Logger.AllowInput();
                return;
            }

            if (Commands[input[0]].RequiresArgs)
            {
                if (input.Length - 1 == 0)
                {
                    Logger.Print($"Command {input[0]} requires arguments: {Commands[input[0]].Usage}");
                    Logger.AllowInput();
                    return;
                }
            }

            try
            {
                Commands[input[0]].Execute(input.Skip(1).ToArray());
            }
            catch (Exception e)
            {
                Logger.Print(e.ToString());
            }
            Logger.AllowInput();
        }
    }
}
