﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class SequenceManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    public List<SequenceSlot> slots = new List<SequenceSlot>();
    public List<GameObject> cards = new List<GameObject>();

    [Header("Hover attributes")]
    public float timer;
    public float hoverTime;
    public bool isHovering = false;
    public bool shouldExpand;


    private void Awake()
    {
        ClearSequence();
            //slots[0].gameObject.GetComponent<SequenceSlot>().enabled = true;
    }
    private void Update()
    {
        if (isHovering)
        {
            hoverTime += Time.deltaTime;
            if (hoverTime >= timer)
            {
                shouldExpand= true;
                OnPointerEnter(null);
            }
        }

    }
    public void ActivateCards()
    {
        foreach(SequenceSlot slot in slots)
        {
            Debug.Log(slot.card);
            if (slot.card != null)
                slot.card.GetComponent<BaseCard>().ActivateEffects();
        }
    }
    public bool AdjacencyCheck(GameObject card, List<cardType> typeCombo, int adjacency)
    {
        
        int inc;
        if (adjacency > 0)
            inc = 1;            // Check ensuing cards
        else
            inc = -1;           // Check preceding cards

        int pivot = cards.IndexOf(card);
        int i = pivot + inc;
        
        //If card is reaction, use last played player card as pivot 
        if (card.GetComponent<BaseCard>().owner == targetCharacter.ENEMY)
            i = cards.Count - 1;
        for (int j = 0; (i < cards.Count && i >= 0) && adjacency != 0;j++)
        {
            if (cards[i].GetComponent<BaseCard>().cardType != typeCombo[j])
                return false;

            i += inc;
            adjacency += -1 * inc;  //positive adj means i goes forward away from 0, adj goes backwards to 0. Vice versa
        }
        if (adjacency == 0)
            return true;
        
        return false;
    }

    public void NewCard(GameObject card)
    {
        Debug.Log("new card added to seq: " + card.name);
        cards.Add(card);
        slots[slots.Count - 1].card = card;

        card.GetComponent<RectTransform>().parent = slots[cards.Count-1].GetComponent<RectTransform>();
        card.GetComponent<BaseCard>().isPlayed = true;
        card.transform.position = slots[cards.Count - 1].transform.position;
        
        //Set next sequence slot active
        if (cards.Count < slots.Count)
        {
            slots[cards.Count].gameObject.GetComponent<SequenceSlot>().enabled = true;
            Image tempImage = slots[cards.Count].gameObject.GetComponent<Image>();
            Color temp = tempImage.color;
            temp.a = 1;
            slots[cards.Count].gameObject.GetComponent<Image>().color = temp;
        }
        ActivateCards();
    }

    public void ClearSequence()
    {
        //set slots inactive and slot reference to card null
        for (int i = 1; i < slots.Count; i++)
        {
            slots[i].card = null;
            slots[i].gameObject.GetComponent<SequenceSlot>().enabled = false;

            //Set transparency to half
            Image tempImage = slots[i].gameObject.GetComponent<Image>();
            Color temp = tempImage.color;
            temp.a = 0.5f;
            slots[i].gameObject.GetComponent<Image>().color = temp;
        }
        //set card inactive whil in card pile
        foreach (GameObject card in cards)
        {
            card.GetComponent<BaseCard>().ResetCard();
            PlayerManager.Instance.discardPile.Push(card);
        }
        cards.Clear();
    }
    #region IBeginDropHandler implementation
    public void OnDrop(PointerEventData data)
    {
        if (data != null)
        {
            GameObject card = data.pointerDrag;
                if (card.GetComponent<BaseCard>().isMoving && GameManager.Instance.phase == turnPhase.PLAYER)
                {
                    PlayerManager.Instance.PlayCard(card);
                }
        }
    }
    #endregion

    #region OnPointerEnter
    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        if (this.CompareTag("PlayerSequenceBoard") && shouldExpand)
        {
            isHovering = false;
            Vector2 temp = transform.position;
            temp.y += GameManager.Instance.seqAdjustVar;
            transform.position = temp;
        }
    }
    #endregion

    #region OnPointerExit
    public void OnPointerExit(PointerEventData eventData)
    {
            isHovering = false;
            hoverTime = 0;
            if (this.CompareTag("PlayerSequenceBoard") && shouldExpand)
            {
                shouldExpand = false;
                Vector2 temp = transform.position;
                temp.y -= GameManager.Instance.seqAdjustVar;
                transform.position = temp;
            }

        
    }
    #endregion
}
