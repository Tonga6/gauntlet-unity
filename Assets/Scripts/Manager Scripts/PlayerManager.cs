using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : CharacterManager
{
    public static PlayerManager Instance { get; private set; }

    [Header("Player Text Displays")]
    public TextMeshProUGUI manaText;
    public TextMeshProUGUI handSizeText;

    [Header("Player Attributes")]
    public int maxMana;
    public int currMana;

    [Header("Player Card Systems")]
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
            currMana = maxMana;
        }
        else
            Destroy(this);
        RefillHand();
        
    }
    public void RefillHand()
    {
        while (handSize < maxHandSize)
        {
            if (drawPile.IsEmpty())
                RefillDrawPile();
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
    public bool PlayCard(GameObject card)
    {

        if (currMana >= card.GetComponent<BaseCard>().manaCost)
        {
            handSize--;
            currMana -= card.GetComponent<BaseCard>().manaCost;
            manaText.text = currMana.ToString();
            sm.NewCard(card);
            EnemyManager.Instance.EnemyReaction();
            return true;
        }
        return false;
    }
    public void StartPhase()
    {
        currMana = maxMana;
        manaText.text = currMana.ToString();
        RefillHand();
    }
    public void ClearBoard()
    {
        sm.ClearSequence();
    }
}

