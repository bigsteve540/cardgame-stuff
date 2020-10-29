using System.Linq;

namespace GameEngine.Core
{
    public enum Turn_Type { Alternate_Teams, Full_Team_Before_Pass, Random }
    public enum Turn_Phase { Standby = 0, Income, Movement, Main1, ComCalc, Main2, End }

    public static class TurnHandler
    {
        public static int TurnNumber { get; private set; }
        public static int TargetIDofTurn { get; private set; } = -1;
        public static Turn_Phase CurrentPhase
        {
            get
            {
                return currentPhase;
            }
            private set
            {
                currentPhase = value;
                GameEventSystem.OnChangeTurnPhase?.Invoke(TargetIDofTurn, CurrentPhase);
            }
        }
        private static Turn_Phase currentPhase;
        private static int[] turnIDOrder;

        public static void InitTurnOrder(Team[] _teams, GameSettings _settings)
        {
            TurnNumber = 0;
            switch (Game.Settings.TurnType)
            {
                case Turn_Type.Alternate_Teams:
                    turnIDOrder = new int[_teams.Aggregate((i1, i2) => i1.Players.Length > i2.Players.Length ? i1 : i2).Players.Length * _teams.Length];

                    int j = 0;
                    int k = 0;
                    for (int i = 0; i < _teams.Length; i++)
                    {
                        j = 0;
                        k = 0;
                        while (j <= turnIDOrder.Length - 1)
                        {
                            if (k > _teams[i].Players.Length - 1)
                                k = 0;
                            turnIDOrder[j + i] = _teams[i].Players[k].ID;
                            j += _teams.Length;
                            k++;
                        }
                    }
                    break;
                case Turn_Type.Full_Team_Before_Pass:
                    break;
                case Turn_Type.Random:
                    break;
            }

            for (int i = 0; i < Network.Server.Clients.Count; i++)
            {
                Game.GetPlayer(i).Deck.Draw(c => c.CardType == Card_Type.Gold_Generator, 2);
                Game.GetPlayer(i).Deck.Draw(Hand.HandSizeLimit - Game.GetPlayer(i).Hand.Cards.Count);
            }
        }

        public static void StartTurn(int _targetID)
        {
            TargetIDofTurn = _targetID;
            CurrentPhase = Turn_Phase.Standby;
            Console.Logger.Print($"Turn: {TurnNumber}, Player: {TargetIDofTurn}, Phase {CurrentPhase.ToString()}");
            GameEventSystem.OnTurnChange?.Invoke(TargetIDofTurn);
            Game.GetPlayer(_targetID).Deck.Draw(1);
        }

        public static void NextPhase()
        {
            int turnID = (int)CurrentPhase;
            if (++turnID > 6)
                PassToNextPlayer();
            else
                CurrentPhase = (Turn_Phase)turnID;
            Console.Logger.Print($"Player {TargetIDofTurn}: Phase {CurrentPhase.ToString()}");
        }

        private static void PassToNextPlayer()
        {
            int targetID = TargetIDofTurn;

            TurnNumber++;
            switch (Game.Settings.TurnType)
            {
                case Turn_Type.Alternate_Teams:
                    TargetIDofTurn = TurnNumber - ((TurnNumber / turnIDOrder.Length) * turnIDOrder.Length);
                    break;
                case Turn_Type.Full_Team_Before_Pass:
                    TargetIDofTurn = (++targetID >= Game.Settings.MaxPlayers ? 0 : targetID);
                    break;
                case Turn_Type.Random:
                    break;
            }
            StartTurn(TargetIDofTurn);
        }
    }
}
