using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthbarScript : MonoBehaviour
{
    public Image healthbar;
    public float health = 1000;
    public float initialHealth = 1000;


    public void damageTaken(int damage)
    {
        //subtract the damage amount from the bases' health
        health -= damage;
        //work out the percentage to fill healthbar
        healthbar.fillAmount = health / initialHealth;
    }
    public void increaseHealth(int amount)
    {
        //Adds health to user's base and fills the healthbar 
        health += amount;
        healthbar.fillAmount = health / initialHealth;
    }
    public void restoreHealth()
    {
        //maxs out base's health and healthbar
        health = initialHealth;
        healthbar.fillAmount = 1;
    }
}
