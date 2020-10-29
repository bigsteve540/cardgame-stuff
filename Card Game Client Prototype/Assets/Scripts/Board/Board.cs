
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/////////////////////////////////////////
/// Script is property of Ava Taylor. ///
/// All rights reserved. ////////////////
/////////////////////////////////////////

public class Board
{
    private Slot[,] boardArray2D;

    public Board()
    {
        #region Board Slot Setup
        Slot powerGrave = new Slot(new Card_Type[2] { Card_Type.Cantrip, Card_Type.Ritual }, 0, 0);
        Slot exiledSlot = new Slot(new Card_Type[3] { Card_Type.Minion, Card_Type.Structure, Card_Type.Token }, 0, 1);

        Slot envSlot1 = new Slot(Card_Type.Environment, 1, 0);
        Slot envSlot2 = new Slot(Card_Type.Environment, 1, 1);
        Slot envSlot3 = new Slot(Card_Type.Environment, 1, 2);

        Slot structureSlot1 = new Slot(Card_Type.Structure, 2, 0);
        Slot structureSlot2 = new Slot(Card_Type.Structure, 3, 0);
        Slot structureSlot3 = new Slot(Card_Type.Structure, 4, 0);
        Slot structureSlot4 = new Slot(Card_Type.Structure, 5, 0);
        Slot structureSlot5 = new Slot(Card_Type.Structure, 6, 0);

        Slot minionSlot1 = new Slot(new Card_Type[2] { Card_Type.Minion, Card_Type.Token }, 2, 1);
        Slot minionSlot2 = new Slot(new Card_Type[2] { Card_Type.Minion, Card_Type.Token }, 3, 1);
        Slot minionSlot3 = new Slot(new Card_Type[2] { Card_Type.Minion, Card_Type.Token }, 4, 1);
        Slot minionSlot4 = new Slot(new Card_Type[2] { Card_Type.Minion, Card_Type.Token }, 5, 1);
        Slot minionSlot5 = new Slot(new Card_Type[2] { Card_Type.Minion, Card_Type.Token }, 6, 1);

        Slot goldGenSlot1 = new Slot(Card_Type.Gold_Generator, 3, 2);
        Slot goldGenSlot2 = new Slot(Card_Type.Gold_Generator, 5, 2);

        Slot ritualSlot1 = new Slot(Card_Type.Ritual, 2, 3);
        Slot ritualSlot2 = new Slot(Card_Type.Ritual, 3, 3);
        Slot ritualSlot3 = new Slot(Card_Type.Ritual, 4, 3);
        Slot ritualSlot4 = new Slot(Card_Type.Ritual, 5, 3);
        Slot ritualSlot5 = new Slot(Card_Type.Ritual, 6, 3);

        Slot overlordSlot = new Slot(Card_Type.Overlord, 4, 4);

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
        return boardArray2D[_y, _x];
    }
}
