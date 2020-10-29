using System;
using GameEngine.Core;

namespace GameEngine.CardEffects
{
    public class FX_G_SlotMachine : CardEffect, IEffectOnPhaseChange
    {
        public override int EffectID => 5;
        public override string Description => "On your income phase, roll a dice. On a 6, gain 2 gold. On 1-4, gain 1.";

        private int playerID;
        private readonly int baseOdds = 10;
        private int currentOdds;
        private int previousOdds;
        private Random rand;

        public override void Init(ref int _player, ref Vector2 _coords)
        {
            playerID = _player;

            currentOdds = baseOdds;
            previousOdds = 0;
            rand = new Random();

            GameEventSystem.OnChangeTurnPhase += Trigger;
        }

        public void Trigger(int _playerID, Turn_Phase _currentPhase)
        {
            if (playerID != _playerID) 
                return;
            if (_currentPhase != Turn_Phase.Income)
                return;

            int oddscmp = rand.Next(0, baseOdds * 6);

            if (oddscmp > currentOdds)
            {
                currentOdds++;
                Game.GetPlayer(_playerID).Gold++;
            }
            else
            {
                currentOdds--;
                Game.GetPlayer(_playerID).Gold += 2;
            }

            if (CompareToOdds(previousOdds, oddscmp))
                currentOdds = baseOdds;

            previousOdds = oddscmp; 
        }

        bool CompareToOdds(int _a, int _b)
        {
            if (_a > currentOdds && !(_b > currentOdds) || _a < currentOdds && !(_b < currentOdds))
            {
                return true;
            }
            return false;
        }
    }
}
