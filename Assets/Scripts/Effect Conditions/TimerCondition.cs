using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerCondition : EffectCondition
{
    public int playedTurn;
    public int timer;
    private void Awake()
    {
        playedTurn = GameManager.Instance.turnCount;
        Debug.Log("playedTurn" + playedTurn);
    }
    public override bool checkCondition()
    {
        if (GameManager.Instance.turnCount > timer + playedTurn)
            return true;
        return false;
    }
}
