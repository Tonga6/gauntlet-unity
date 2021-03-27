using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public List<GameObject> hand;

    public void AddToHand(GameObject card)
    {
        card.GetComponent<RectTransform>().parent = GetComponent<RectTransform>();
        card.GetComponent<RectTransform>().localScale = Vector3.one;
        hand.Add(card);
    }
}
