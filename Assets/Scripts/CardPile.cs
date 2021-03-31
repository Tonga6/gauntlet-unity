using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardPile : MonoBehaviour
{
    
    //public GameObject handZone;
    [SerializeField]
    public List<GameObject> cards = new List<GameObject>();
    public bool isDrawPile = false;

    public TextMeshProUGUI text;

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
        if (!IsEmpty())
        {
            GameObject temp = cards[cards.Count - 1];
            cards.RemoveAt(cards.Count - 1);

            temp.SetActive(true);
            UpdateCount();
            return temp;
        }
        return null;
    }

    public void Push(GameObject card)
    {
        cards.Add(card);
        card.GetComponent<RectTransform>().parent = GetComponent<RectTransform>();
        card.GetComponent<BaseCard>().ResetCard();
        card.SetActive(false);
        UpdateCount();
    }

    public bool IsEmpty()
    {
        if (cards.Count == 0)
            return true;
        return false;
    }
    void UpdateCount()
    {
        text.text = cards.Count.ToString();
    }
    public void Populate(List<GameObject> cards)
    {
        foreach (GameObject card in cards)
        {
            Push(Instantiate(card, this.transform));
        }
        ShufflePile();
    }
}
