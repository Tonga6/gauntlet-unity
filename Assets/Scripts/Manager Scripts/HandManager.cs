using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public List<GameObject> hand;
    
    private void Awake()
    {
        Vector2 temp = GetComponent<RectTransform>().position;
        temp.y -= GameManager.Instance.handSinkVar;
        GetComponent<RectTransform>().position = temp;
    }
    public void AddToHand(GameObject card)
    {
        RectTransform rt = card.GetComponent<RectTransform>();
        rt.parent = GetComponent<RectTransform>();
        rt.localScale = GameManager.Instance.cardScale;
        Vector2 temp = rt.position;
        temp.y -= GameManager.Instance.sunkVar;
        rt.position = temp;

        hand.Add(card);
    }
    public void RemoveFromHand(GameObject card)
    {
        //Which card removed doesn't matter
        if (card == null)
            card = hand[0];

        Debug.Log("Remove from hand " + card);
        PlayerManager.Instance.discardPile.Push(card);
        hand.Remove(card);
    }
}
