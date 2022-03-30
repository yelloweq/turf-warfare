using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class upgradeScript : MonoBehaviour
{

    public BaseHealth userBase;
    public HealthbarScript healthbar;
    GameObject money;
    public Text message;
    string originalText;
    public GameObject wall;
    public bool bought;


    // Start is called before the first frame update
    void Start()
    {
        money = GameObject.FindGameObjectWithTag("Player");
    }

    public void buyHealth()
    {
        if (money.GetComponent<CharacterCurrency>().getCurrency() >= 500 && userBase.health < 100)
        {
            message.gameObject.SetActive(false);

            if (userBase.health >= 80)
            {
                userBase.health = 100;
                healthbar.restoreHealth();
            }
            else
            {
                userBase.health += 20;
                healthbar.increaseHealth(20);
            }
            money.GetComponent<CharacterCurrency>().updateCurrency(-500);   //reduces currency by 500
        }
        else     //Otherwise it displays the message 'unavailable'
        {
            message.gameObject.SetActive(true);
        }
    }

    public void activateWall()
    {
        if (bought == false && money.GetComponent<CharacterCurrency>().getCurrency() >= 500)
        {
            message.gameObject.SetActive(false);
            bought = true;
            wall.SetActive(true);
            money.GetComponent<CharacterCurrency>().updateCurrency(-500);
        }
        else
        {
            message.gameObject.SetActive(true);
        }
    }
}
