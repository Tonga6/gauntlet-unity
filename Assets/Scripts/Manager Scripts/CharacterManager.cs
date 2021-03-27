using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CharacterManager : MonoBehaviour
{
    [Header("Character Text Displays")]
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI shieldText;


    [Header("Character Attributes")]
    public int health;
    public int shield;
    public int mana;

    [Header("Hand Attributes")]
    public int maxHandSize;
    public int handSize;

    [Header("External References")]
    public SequenceManager sm;

    public void Initialise()
    {
        healthText.text = health.ToString();
        shieldText.text = shield.ToString();
    }

    public void TakeDamage (int damage)
    {
        health -= damage;
        healthText.text = health.ToString();
    }
    public void GiveShield(int bonus)
    {
        Debug.Log("Give: " + shield + " shield to: " + this.gameObject.name);
        this.shield += bonus;
        shieldText.text = shield.ToString();
        Debug.Log(shield + this.gameObject.name);

    }
}
