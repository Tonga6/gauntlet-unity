using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class GameManager : MonoBehaviour, IDropHandler
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

    [Header("Card Attributes")]
    public bool canMag = true;
    public int adjustVar;
    public int sunkVar;
    public Vector3 magScale;
    public Vector3 cardScale;

    [Header("Hand Zone Attributes")]
    public int handSinkVar;

    [Header("Sequence Zone Attributes")]
    public int seqAdjustVar;
    public SequenceManager playerSeq;

    public turnPhase phase;
    public int turnCount = 0;
    public TextMeshProUGUI buttonText;
    public TextMeshProUGUI bannerText;
    public GameObject banner;
    private void Awake()
    {
        StartCoroutine(Initialise());
    }
    IEnumerator Initialise()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            yield return new WaitForSeconds(1);
        playerDrawPile.Populate(playerCards);

        enemyActionDrawPile.Populate(enemyActionCards);
        enemyReactionDrawPile.Populate(enemyReactionCards);

        playerSeq = GameObject.FindGameObjectWithTag("PlayerSequenceBoard").GetComponent<SequenceManager>();
        StartPhase();
        }
    }

    public IEnumerator NextPhase()
    {
        if (phase == turnPhase.END)
            phase = turnPhase.START;

        else
            phase++;
        if(phase == turnPhase.ENEMY || phase == turnPhase.PLAYER)
        {
        yield return new WaitForSeconds(2);

        bannerText.text = phase.ToString() + " PHASE";
        banner.SetActive(true);
        yield return new WaitForSeconds(2);
        banner.SetActive(false);
        }
        

        switch (phase) // use upcast, where 0 - first, 1 - second...
        {
            case (turnPhase.START):
                StartPhase();
                break;
            case (turnPhase.ENEMY):
                EnemyPhase();
                
                break;
            case (turnPhase.PLAYER):
                PlayerPhase();
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
        turnCount++;
        if(EnemyManager.Instance != null)
            StartCoroutine(NextPhase());
    }
    void EnemyPhase()
    {
        EnemyManager.Instance.EnemyPhase();
        StartCoroutine(NextPhase());
    }
    void PlayerPhase()
    {
        PlayerManager.Instance.StartPhase();
    }
    public void EndPhase()
    {
        PlayerManager.Instance.ClearBoard();
        PlayerManager.Instance.ClearHand();
        StartCoroutine(NextPhase());
    }

    #region IBeginDropHandler implementation
    public void OnDrop(PointerEventData data)
    {
        if (data != null)
        {
            GameObject card = data.pointerDrag;
            //if (sm.CompareTag("PlayerSequenceBoard"))
            //{
            //    if (card.GetComponent<BaseCard>().isMoving)
            //    {
            //        if (PlayerManager.Instance.PlayCard(card))
            //        {
            //            sm.NewCard(card);
            //            sm.ActivateCards();
            //        }
            //    }

            //}

        }
    }
    #endregion
}

public enum turnPhase{
    START,
    ENEMY,
    PLAYER,
    END
}
