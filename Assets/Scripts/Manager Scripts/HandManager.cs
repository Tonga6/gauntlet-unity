using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HandManager : MonoBehaviour
{
    public List<GameObject> hand;
    public List<GameObject> containers;
    private void Awake()
    {
        Vector2 temp = GetComponent<RectTransform>().position;
        temp.y -= GameManager.Instance.handSinkVar;
        GetComponent<RectTransform>().position = temp;

        //initialise containers
        for(int i = 0; i < transform.childCount; i++)
        {
            containers.Add(transform.GetChild(i).gameObject);
            transform.GetChild(i).GetComponent<RectTransform>().parent = GetComponent<RectTransform>();
        }
    }
    public void AddToHand(GameObject card)
    {
        hand.Add(card);
        //If called before initialisation call Awake()
        if (containers.Count == 0)
            Awake();

        int i = 0;
        for (; i < hand.Count; i++)
        {
            //If container has no card, place incoming card here
            if (containers[i].transform.childCount == 0)
            {
                card.GetComponent<RectTransform>().parent = containers[i].GetComponent<RectTransform>();
                break;
            }
        }
        card.GetComponent<BaseCard>().MoveTo(containers[i].GetComponent<RectTransform>());
        

    }
    public void RemoveFromHand(GameObject card)
    {
        hand.Remove(card);
    }
    public void SendToDiscard(GameObject card)
    {
        //Which card removed doesn't matter
        if (card == null)
            card = hand[0];

        PlayerManager.Instance.discardPile.Push(card);
        hand.Remove(card);
    }
}
