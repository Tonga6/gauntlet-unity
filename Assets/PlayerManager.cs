using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    public SequenceManager sm;

    public CardPile drawPile;
    public CardPile discardPile;

    public HandManager hm;

    public int maxHandSize;
    public int handSize;
    List<GameObject> playerHand;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
        RefillHand();
        
    }
    public void RefillHand()
    {
        while (handSize < maxHandSize && !drawPile.IsEmpty())
        {
            DrawCard();
        }
    }
    public void RefillDrawPile()
    {
       while (!discardPile.IsEmpty())
        {
            drawPile.Push(discardPile.Pop());
        }
    }
    public void DrawCard()
    {
        GameObject temp = drawPile.Pop();
        temp.SetActive(true);
        hm.AddToHand(temp);
        
        handSize++;
    }


    public void ClearBoard()
    {
        sm.ClearSequence();
    }
}

