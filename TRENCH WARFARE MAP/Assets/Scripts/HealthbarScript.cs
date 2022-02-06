using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarScript : MonoBehaviour
{
    public Image healthbar;
    public float health;
    public float initialHealth = 100;
    public void damageTaken(int damage)
    {
        //subtract the damage amount from the bases' health
        health -= damage;
        //work out the percentage to fill healthbar
        healthbar.fillAmount = health / initialHealth;
    }
    public void increaseHealth(int damage)
    {
        //subtract the damage amount from the bases' health
        health += damage;
        //work out the percentage to fill healthbar
        healthbar.fillAmount = health / initialHealth;
    }
    public void restoreHealth()
    {
        //subtract the damage amount from the bases' health
        health = 100;
        //work out the percentage to fill healthbar
        healthbar.fillAmount = health / initialHealth;
    }
}
