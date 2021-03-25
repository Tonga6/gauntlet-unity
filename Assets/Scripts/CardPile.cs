using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPile : MonoBehaviour
{
    
    public GameObject handZone;
    [SerializeField]
    List<GameObject> cards = new List<GameObject>();
    private void Awake()
    {
        ShufflePile();
    }
    public void ShufflePile()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            GameObject card = cards[i];
            int randomIndex = Random.Range(i, cards.Count);
            cards[i] = cards[randomIndex];
            cards[randomIndex] = card;
        }
    }
    public GameObject Pop()
    {
        Debug.Log("Pop, not empty");
        if (!IsEmpty())
        {
            GameObject temp = cards[cards.Count-1];
            cards.RemoveAt(cards.Count - 1);
            return temp;
        }
        return null;
    }

    public bool IsEmpty()
    {
        if (cards.Count == 0)
            return true;
        return false;
    }
}
