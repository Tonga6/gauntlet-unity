using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public List<GameObject> playerCards;
    public CardPile playerDrawPile;

    public List<GameObject> enemyCards;
    public CardPile enemyDrawPile;

    public int magScale;
    public int adjustVar;
    
    turnPhase phase;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        playerDrawPile.Populate(playerCards);
    }

    public void NextPhase()
    {
        if (phase == turnPhase.END)
            phase = turnPhase.START;

        else
            phase++;

        switch (phase) // use upcast, where 0 - first, 1 - second...
        {
            case (turnPhase.START):
                StartPhase();
                break;
            case (turnPhase.ENEMY):
                break;
            case (turnPhase.PLAYER):
                break;
            case (turnPhase.END):
                EndPhase();
                break;
            default:
                break;
        }
    }

    void StartPhase()
    {
        //Refill draw pile if empty
        if (PlayerManager.Instance.drawPile.IsEmpty())
            PlayerManager.Instance.RefillDrawPile();

        //Refill hand
        PlayerManager.Instance.RefillHand();
    }
    void EndPhase()
    {
        PlayerManager.Instance.ClearBoard();
    }
}

public enum turnPhase{
    START,
    ENEMY,
    PLAYER,
    END
}