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
    public float wait;
    public float timer;
    public bool waiting;

    public TextMeshProUGUI text;
    private void Update()
    {
        if (waiting)
            timer -= Time.deltaTime;
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
        if (!IsEmpty())
        {
            GameObject temp = cards[0];
            cards.RemoveAt(0);
            temp.GetComponent<BaseCard>().ResetCard();
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
        if (card.GetComponent<BaseCard>().owner == character.PLAYER)
            card.GetComponent<BaseCard>().MoveTo(GetComponent<RectTransform>(), 0.6f);
        else
            card.GetComponent<RectTransform>().transform.position = GetComponent<RectTransform>().transform.position;

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
