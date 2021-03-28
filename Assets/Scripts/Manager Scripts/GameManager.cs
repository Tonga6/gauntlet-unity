using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Player Deck Setup")]
    public List<GameObject> playerCards;
    public CardPile playerDrawPile;

    [Header("Enemy Deck Setup")]
    public List<GameObject> enemyActionCards;
    public CardPile enemyActionDrawPile;
    public List<GameObject> enemyReactionCards;
    public CardPile enemyReactionDrawPile;

    public bool canMag;
    public int adjustVar;
    public int sunkVar;
    public Vector3 magScale;
    public Vector3 cardScale;

    public turnPhase phase;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        playerDrawPile.Populate(playerCards);
        enemyActionDrawPile.Populate(enemyActionCards);
        StartPhase();
    }

    public void NextPhase()
    {
        if (phase == turnPhase.END)
            phase = turnPhase.START;

        else
            phase++;
        Debug.Log("Next Phase: " + phase);
        switch (phase) // use upcast, where 0 - first, 1 - second...
        {
            case (turnPhase.START):
                StartPhase();
                break;
            case (turnPhase.ENEMY):
                EnemyPhase();
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
        PlayerManager.Instance.StartPhase();
        //NextPhase();
    }
    void EnemyPhase()
    {
        EnemyManager.Instance.EnemyAction();
    }
    void EndPhase()
    {
        PlayerManager.Instance.ClearBoard();
        NextPhase();
    }
}

public enum turnPhase{
    START,
    ENEMY,
    PLAYER,
    END
}