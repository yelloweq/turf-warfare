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
    private string successMessage;
    private string errorMessage;


    // Start is called before the first frame update
    void Start()
    {
        money = GameObject.FindGameObjectWithTag("Player");
        successMessage = "ITEM BOUGHT SUCCESSFULLY";
        errorMessage = "ITEM UNAVAILABLE";
    }

    public void buyHealth()
    {
        if (money.GetComponent<CharacterCurrency>().getCurrency() >= 500 && userBase.health < 100)
        {
            message.text = successMessage;
            message.color = Color.green;
            message.gameObject.SetActive(true);

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
            message.text = errorMessage;
            message.color = Color.red;
            message.gameObject.SetActive(true);
        }
    }

    public void activateWall()
    {
        if (bought == false && money.GetComponent<CharacterCurrency>().getCurrency() >= 500)
        {
            message.text = successMessage;
            message.color = Color.green;
            message.gameObject.SetActive(true);

            bought = true;
            wall.SetActive(true);
            money.GetComponent<CharacterCurrency>().updateCurrency(-500);
        }
        else
        {
            message.text = errorMessage;
            message.color = Color.red;
            message.gameObject.SetActive(true);
        }
    }
}