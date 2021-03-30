using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : CharacterManager
{
    public static EnemyManager Instance { get; private set; }

    [Header("Enemy Card Systems")]
    public CardPile actionPile;
    public CardPile reactionPile;

    public SequenceManager actionSeq;
    public SequenceManager reactionSeq;

    public Animator animator;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Initialise();
        }
        else
            Destroy(this);

    }

    public void EnemyAction()
    {
        Debug.Log("Enemy Action");
        //Activate action
        if (actionSeq.cards.Count != 0)
        {
            if (actionSeq.cards[0].GetComponent<BaseCard>().cardType == cardType.Attack)
            {
                animator.SetTrigger("isAttacking");
            }

            actionSeq.cards[0].GetComponent<BaseCard>().ActivateEffects();
            actionPile.Push(actionSeq.cards[0]);
            actionSeq.cards.RemoveAt(0);
            actionSeq.slots[0].card = null;

        }

        //Queue next turn's action
        actionSeq.NewCard(actionPile.Pop());
    }
}
