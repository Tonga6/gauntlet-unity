using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public CardPile drawPile;
    public CardPile discardPile;

    public GameObject handZone;

    public int maxHandSize;
    public int handSize;
    List<GameObject> playerHand;

    private void Awake()
    {
        while(handSize < maxHandSize && !drawPile.IsEmpty()) { 
            DrawCard();
        }
    }

    public void DrawCard()
    {
        GameObject temp = Instantiate(drawPile.Pop(),drawPile.transform);
        temp.GetComponent<RectTransform>().parent = handZone.GetComponent<RectTransform>();
        //temp.GetComponent<BaseCard>().AnimateToParent(handZone.GetComponent<RectTransform>());    //attempt to animate movement
        handSize++;
    }

    
}
