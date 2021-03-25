using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SequenceManager : MonoBehaviour
{
    public List<SequenceSlot> slots = new List<SequenceSlot>();
    public List<GameObject> cards = new List<GameObject>();

    private void Awake()
    {
        for (int i = 1; i < slots.Count; i++)
        {
            slots[i].gameObject.GetComponent<SequenceSlot>().enabled = false;
            Image tempImage = slots[i].gameObject.GetComponent<Image>();
            Color temp = tempImage.color;
            temp.a = temp.a/2;
            slots[i].gameObject.GetComponent<Image>().color = temp;
        }
            //slots[0].gameObject.GetComponent<SequenceSlot>().enabled = true;
    }
    public void ActivateCards()
    {
        foreach(SequenceSlot slot in slots)
        {
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
        for (int j = 0; (i < cards.Count && i >= 0) && adjacency != 0;j++)
        {
            Debug.Log("Card played at: " + pivot);
            Debug.Log("Card at: " + i + " has type: " + cards[i].GetComponent<BaseCard>().cardType);
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
        cards.Add(card);
        //Set next sequence slot active
        if (cards.Count < slots.Count)
        {

            slots[cards.Count].gameObject.GetComponent<SequenceSlot>().enabled = true;
            Image tempImage = slots[cards.Count].gameObject.GetComponent<Image>();
            Color temp = tempImage.color;
            temp.a = 255;
            slots[cards.Count].gameObject.GetComponent<Image>().color = temp;
        }   
    }
}
