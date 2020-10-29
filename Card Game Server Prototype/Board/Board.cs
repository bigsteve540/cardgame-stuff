/////////////////////////////////////////
/// Script is property of Ava Taylor. ///
/// All rights reserved. ////////////////
/////////////////////////////////////////

namespace GameEngine.Core
{
    public class Board
    {
        private Slot[,] boardArray2D;

        public Board(ref int _boardOwner)
        {
            #region Board Slot Setup
            Slot powerGrave = new Slot(new Card_Type[2] { Card_Type.Cantrip, Card_Type.Ritual }, 1, 1, ref _boardOwner);
            Slot exiledSlot = new Slot(new Card_Type[3] { Card_Type.Minion, Card_Type.Structure, Card_Type.Token }, 1, 2, ref _boardOwner);

            Slot envSlot1 = new Slot(Card_Type.Environment, 2, 1, ref _boardOwner);
            Slot envSlot2 = new Slot(Card_Type.Environment, 2, 2, ref _boardOwner);
            Slot envSlot3 = new Slot(Card_Type.Environment, 2, 3, ref _boardOwner);

            Slot structureSlot1 = new Slot(Card_Type.Structure, 3, 1, ref _boardOwner);
            Slot structureSlot2 = new Slot(Card_Type.Structure, 4, 1, ref _boardOwner);
            Slot structureSlot3 = new Slot(Card_Type.Structure, 5, 1, ref _boardOwner);
            Slot structureSlot4 = new Slot(Card_Type.Structure, 6, 1, ref _boardOwner);
            Slot structureSlot5 = new Slot(Card_Type.Structure, 7, 1, ref _boardOwner);

            Slot minionSlot1 = new Slot(new Card_Type[2] { Card_Type.Minion, Card_Type.Token }, 3, 2, ref _boardOwner);
            Slot minionSlot2 = new Slot(new Card_Type[2] { Card_Type.Minion, Card_Type.Token }, 4, 2, ref _boardOwner);
            Slot minionSlot3 = new Slot(new Card_Type[2] { Card_Type.Minion, Card_Type.Token }, 5, 2, ref _boardOwner);
            Slot minionSlot4 = new Slot(new Card_Type[2] { Card_Type.Minion, Card_Type.Token }, 6, 2, ref _boardOwner);
            Slot minionSlot5 = new Slot(new Card_Type[2] { Card_Type.Minion, Card_Type.Token }, 7, 2, ref _boardOwner);

            Slot goldGenSlot1 = new Slot(Card_Type.Gold_Generator, 4, 2, ref _boardOwner);
            Slot goldGenSlot2 = new Slot(Card_Type.Gold_Generator, 6, 2, ref _boardOwner);

            Slot ritualSlot1 = new Slot(Card_Type.Ritual, 3, 4, ref _boardOwner);
            Slot ritualSlot2 = new Slot(Card_Type.Ritual, 4, 4, ref _boardOwner);
            Slot ritualSlot3 = new Slot(Card_Type.Ritual, 5, 4, ref _boardOwner);
            Slot ritualSlot4 = new Slot(Card_Type.Ritual, 6, 4, ref _boardOwner);
            Slot ritualSlot5 = new Slot(Card_Type.Ritual, 7, 4, ref _boardOwner);

            Slot overlordSlot = new Slot(Card_Type.Overlord, 5, 5, ref _boardOwner);

            boardArray2D = new Slot[5, 7]
            {
            { powerGrave, envSlot1  , structureSlot1, structureSlot2, structureSlot3, structureSlot4, structureSlot5 },
            { exiledSlot, envSlot2  , minionSlot1   , minionSlot2   , minionSlot3   , minionSlot4   , minionSlot5    },
            { null      , envSlot3  , null          , goldGenSlot1  , null          , goldGenSlot2  , null           },
            { null      , null      , ritualSlot1   , ritualSlot2   , ritualSlot3   , ritualSlot4   , ritualSlot5    },
            { null      , null      , null          , null          , overlordSlot  , null/*token deck here*/, null/*deck goes here*/ }
            };
            #endregion
        }

        public Slot FetchSlot(int _x, int _y)
        {
            if (boardArray2D[_y - 1, _x - 1] == null)
                throw new System.Exception($"Slot {_x},{_y} is null and should not be attempted to be fetched.");

            return boardArray2D[_y - 1, _x - 1];
        }

        public override string ToString()
        {
            string output = string.Empty;
            for (int y = 0; y < boardArray2D.GetLength(0); y++)
            {
                for (int x = 0; x < boardArray2D.GetLength(1); x++)
                {
                    string tmp = string.Empty;
                    if(boardArray2D[y,x] != null)
                    {
                        if (boardArray2D[y, x].Occupied)
                        {
                            
                            output += "[x]";
                            continue;
                        }
                        //Logger.Print($"Slot {x},{y} is not occupied");
                        output += "[ ]";
                        continue;
                    }
                    output += "   ";
                }
                output += "\n";
            }
            return output;
        }
    }
}

