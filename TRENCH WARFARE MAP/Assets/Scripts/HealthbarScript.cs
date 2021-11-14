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
        health -= damage;
        healthbar.fillAmount = health / initialHealth;
    }
}
