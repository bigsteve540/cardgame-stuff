using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/////////////////////////////////////////
/// Script is property of Ava Taylor. ///
/// All rights reserved. ////////////////
/////////////////////////////////////////

public class BoardTests : MonoBehaviour
{
    [Range(1,10)]
    public int TestNumber;
    public Card testCard;
    public Vector2 testslotCoords;
    [Space]
    public bool test4Overheal = false;

    Board testBoard = new Board();

    // Start is called before the first frame update
    void OnEnable()
    {
        Slot testSlot = testBoard.FetchSlot((int)testslotCoords.x, (int)testslotCoords.y);
        switch (TestNumber)
        {
            case 1:
                AddCardTest(testSlot);
                break;
            case 2:
                RemoveCardTest(testSlot);
                break;
            case 3:
                TakeDamageTest(testSlot);
                break;
            case 4:
                HealCardTest(testSlot);
                break;
            case 5:
                AddCardToGraveyard(testSlot);
                break;
            case 6:
                RemoveCardFromGraveyard(testSlot);
                break;
        }
    }

    private void AddCardTest(Slot _tester)
    {
        if (_tester.AddCard(testCard))
        {
            Debug.Log("Successfully added card to slot");
            return;
        }
        Debug.Log("Could not add card to slot");
    }

    private void RemoveCardTest(Slot _tester)
    {
        AddCardTest(_tester);
        if (_tester.RemoveCard())
        {
            Debug.Log("Successfully removed card from slot");
            return;
        }
        Debug.Log("Could not remove card from slot");
    }

    private int TakeDamageTest(Slot _tester)
    {
        if(testCard is ICombatable)
        {
            AddCardTest(_tester);
            int newhp = _tester.TakeDamage(test4Overheal ? 99:1, null);
            if (newhp < (testCard as ICombatable).CardHealth)
            {
                Debug.Log($"Successfully dealt damage. Previous HP: {(testCard as ICombatable).CardHealth}, New HP: {newhp}");
            }
            return newhp;
        }
        else
        {
            Debug.LogWarning("test card cannot be dealt damage. Please pick a card with health values.");
        }
        return 0;
    }

    private void HealCardTest(Slot _tester)
    {
        if (testCard is ICombatable)
        {
            int oldhp = TakeDamageTest(_tester);
            int newhp = _tester.Heal(2, null);
            if (oldhp <= newhp)
            {
                Debug.Log($"Successfully healed. Previous HP: {oldhp}, New HP: {newhp}");
            }
        }
        else
        {
            Debug.LogWarning("test card cannot be healed. Please pick a card with health values.");
        }
    }

    private void AddCardToGraveyard(Slot _tester)
    {
        if (_tester.AddCardToGrave(testCard))
        {
            Debug.Log($"Successfully added card to graveyard.");
            return;
        }
        Debug.Log("Failed to add card to graveyard.");
    }

    private void RemoveCardFromGraveyard(Slot _tester)
    {
        AddCardToGraveyard(_tester);
        if (_tester.RemoveCardFromGrave(testCard))
        {
            Debug.Log("Successfully removed card from slot graveyard.");
            return;
        }
        Debug.Log("Failed to remove card from graveyard.");
    }

    private void AddCondition(Slot _tester)
    {
        AddCardTest(_tester);
        //_tester.ApplyCondition();
    }
}
