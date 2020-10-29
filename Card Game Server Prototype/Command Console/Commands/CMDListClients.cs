using GameEngine.Network;

namespace GameEngine.Console.Commands
{
    public class CMDListClients : ConsoleCommand
    {
        public override string Name => "listusers";
        public override string Usage => string.Empty;
        public override string Description => "lists all users connected to the server";

        public override void Execute(string[] args)
        {
            string final = string.Empty;
            for (int i = 0; i < Server.Clients.Count; i++)
            {
                if(Server.Clients[i].Claimed)
                    final += "User:"+ Server.Clients[i].ID + " ";
            }

            if (final == string.Empty)
                Logger.Print("No Users Found.");
            else
                Logger.Print(final);
        }
    }
}
