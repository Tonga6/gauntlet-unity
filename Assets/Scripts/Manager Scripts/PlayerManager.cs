using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : CharacterManager
{

    [Header("Player Text Displays")]
    public TextMeshProUGUI manaText;
    public TextMeshProUGUI handSizeText;
    public static PlayerManager Instance { get; private set; }

    public CardPile drawPile;
    public CardPile discardPile;

    public HandManager hm;

    
    List<GameObject> playerHand;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Initialise();
        }
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

