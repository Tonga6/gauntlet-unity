using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public List<GameObject> hand;

    public void AddToHand(GameObject card)
    {
        Debug.Log("add to hand zone");
        card.GetComponent<RectTransform>().parent = GetComponent<RectTransform>();
        card.GetComponent<RectTransform>().localScale = Vector3.one;
        hand.Add(card);
    }
}
