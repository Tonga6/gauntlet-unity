using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance;
    public List<CharacterManager> targets;
    private void Awake()
    {
        if (Instance != null)
            {
                Destroy(this.gameObject);
                return;
            }
        Instance = this;
        //use for simple targeting
        if (PlayerManager.Instance != null && EnemyManager.Instance != null)
        {
            Instance.targets.Add(PlayerManager.Instance);
            Instance.targets.Add(EnemyManager.Instance);
            Debug.Log(targets.ToString());
        }
    }

    public void DealDamage(targetCharacter target, int damage)
    {
        Debug.Log("Deal Damage called to target");
        if (target == targetCharacter.ENEMY)
        {
            EnemyManager.Instance.TakeDamage(damage);
        }
        else
            PlayerManager.Instance.TakeDamage(damage);
    }
    public void GiveShield(targetCharacter target, int shield)
    {
        Debug.Log("Give Shield called to target");
        if (target == targetCharacter.ENEMY)
        {
            EnemyManager.Instance.GiveShield(shield);
        }
        else
            PlayerManager.Instance.GiveShield(shield);
    }
}

public enum targetCharacter
{
    PLAYER,
    ENEMY
}
