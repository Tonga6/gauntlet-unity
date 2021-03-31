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

    [Header("Character Bars")]
    public Slider healthBar;
    public Slider shieldBar;

    [Header("Character Attributes")]
    public int maxHealth;
    public int health;
    public int maxShield; 
    public int shield;

    [Header("Hand Attributes")]
    public int maxHandSize;
    public int handSize;

    [Header("External References")]
    public SequenceManager sm;

    public void Initialise()
    {
        healthText.text = healthText.text.Replace("X", health.ToString());
        healthText.text = healthText.text.Replace("Y", maxHealth.ToString());
        
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;


        shieldText.text = shieldText.text.Replace("X", shield.ToString());
        shieldText.text = shieldText.text.Replace("Y", maxShield.ToString());

        shieldBar.maxValue = maxShield;
        shieldBar.value = 0;
    }

    public void TakeDamage (int damage)
    {
        int oldH = health;
        int oldS = shield;
        if (damage > shield)
        {
            health -= damage - shield;
            shield = 0;
        }
        else
            shield -= damage;

        healthText.text = ReplaceFirst(healthText.text.ToString(), oldH.ToString(), health.ToString());
        healthBar.value = health;
        Debug.Log("Health bar.val = " + healthBar.value);

        shieldText.text = ReplaceFirst(shieldText.text.ToString(), oldS.ToString(), shield.ToString());
        shieldBar.value = shield;
    }

    public void GiveShield(int bonus)
    {
        int oldS = shield;
        this.shield += bonus;
        shieldText.text = ReplaceFirst(shieldText.text.ToString(), oldS.ToString(), shield.ToString());
        shieldBar.value = shield;
    }

    public string ReplaceFirst(string text, string search, string replace)
    {
        int pos = text.IndexOf(search);
        if (pos < 0)
        {
            return text;
        }
        return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
    }
}
