﻿using System.Collections;
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

    public GameObject button;

    
    List<GameObject> playerHand;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Initialise();
            currMana = maxMana;

            Color temp = button.GetComponent<Image>().color;
            temp.a = 0.5f;
            button.GetComponent<Image>().color = temp;

            button.GetComponent<Button>().interactable = false;
        }
        else
            Destroy(this);
        RefillHand();
    }
    public void RefillHand()
    {
        bool temp = true;
        while (handSize < startHandSize && temp == true)
        {
            temp = DrawCard();
        }
    }
    public void ClearHand()
    {
        while (hm.GetComponent<HandManager>().hand.Count > 0)
        {
            hm.GetComponent<HandManager>().SendToDiscard(null);
            handSize--;
        }
    }
    public void RefillDrawPile()
    {
        while (!discardPile.IsEmpty())
        {
            GameObject card = discardPile.Pop();
            card.GetComponent<BaseCard>().MoveTo(drawPile.GetComponent<RectTransform>(),0.6f);
            drawPile.Push(card);
        }
    }
    public bool DrawCard()
    {
        if (drawPile.IsEmpty())
            RefillDrawPile();

        GameObject temp = drawPile.Pop();
        if(temp != null)
        {
            temp.SetActive(true);
            hm.AddToHand(temp);
            handSize++;
            return true;
        }
        return false;
    }
    public bool PlayCard(GameObject card)
    {

        if (currMana >= card.GetComponent<BaseCard>().manaCost)
        {
            handSize--;
            currMana -= card.GetComponent<BaseCard>().manaCost;
            manaText.text = currMana.ToString();
            sm.NewCard(card);
            hm.RemoveFromHand(card);
            EnemyManager.Instance.EnemyReaction();
            return true;
        }
        card.GetComponent<BaseCard>().ResetPos();
        return false;
    }
    public void StartPhase()
    {
        currMana = maxMana;
        manaText.text = currMana.ToString();
        RefillHand();

        //activate button
        Color temp = button.GetComponent<Image>().color;
        temp.a = 1f;
        button.GetComponent<Image>().color = temp;

        button.GetComponent<Button>().interactable = true;

    }
    public void EndPhase()
    {
        Color temp = button.GetComponent<Image>().color;
        temp.a = 0.5f;
        button.GetComponent<Image>().color = temp;

        button.GetComponent<Button>().interactable = false;

        StartCoroutine(GameManager.Instance.NextPhase());
    }
    public void ClearBoard()
    {
        sm.ClearSequence();
    }
    public void GainMana(int manaGain)
    {
        currMana += manaGain;
        manaText.text = currMana.ToString();
    }
}

