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
        while (handSize < startHandSize)
        {
            if (drawPile.IsEmpty())
                RefillDrawPile();
            DrawCard();
        }
    }
    public void ClearHand()
    {
        while (hm.GetComponent<HandManager>().hand.Count > 0)
        {
            hm.GetComponent<HandManager>().RemoveFromHand(null);
            handSize--;
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
        if (drawPile.IsEmpty())
            RefillDrawPile();
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
            hm.hand.Remove(card);
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

    public void GainMana(int manaGain)
    {
        Debug.Log("Gain mana");
        currMana += manaGain;
        manaText.text = currMana.ToString();
    }
}

