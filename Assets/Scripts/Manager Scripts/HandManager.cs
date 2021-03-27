using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public List<GameObject> hand;
    
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
}
