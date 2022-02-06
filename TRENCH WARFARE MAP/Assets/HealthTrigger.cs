using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthTrigger : MonoBehaviour
{
    public BaseHealth userBase;
    public HealthbarScript healthbar;
    public Currency userCurrency;
    public Text message;
    string originalText;

    // Start is called before the first frame update
    void Start()
    {       
        originalText = message.text;
    }
    
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" &&  userCurrency.getCurrency() >= 500 && userBase.health < 100)
        {
            message.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                if(userBase.health >= 80)
                {
                    userBase.health = 100;
                    healthbar.restoreHealth();
                }
                else
                {
                    userBase.health += 20;
                    healthbar.increaseHealth(20);
                }
                userCurrency.updateCurrency(-500);
            }
        }
        else
        {
            message.text = "Unavailable";
            message.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        message.text = originalText;
        message.gameObject.SetActive(false);
    }
}
