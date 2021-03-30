using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance;
    public List<CharacterManager> targets;
    public CamShake camShake;
    private void Awake()
    {
        if (Instance != null)
            {
                Destroy(this.gameObject);
                return;
            }
        //camShake = Camera.main.GetComponent<CamShake>();
        
        camShake = GameObject.FindGameObjectWithTag("MasterCanvas").GetComponent<CamShake>();
        Instance = this;
        //use for simple targeting
        if (PlayerManager.Instance != null && EnemyManager.Instance != null)
        {
            Instance.targets.Add(PlayerManager.Instance);
            Instance.targets.Add(EnemyManager.Instance);
        }
    }

    public void DealDamage(targetCharacter target, int damage)
    {
        Debug.Log("Deal Damage called to target: " + target);
        camShake.Shake();
        if (target == targetCharacter.ENEMY)
        {
            EnemyManager.Instance.TakeDamage(damage);
        }
        else
            PlayerManager.Instance.TakeDamage(damage);
    }
    public void GiveShield(targetCharacter target, int shield)
    {
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
