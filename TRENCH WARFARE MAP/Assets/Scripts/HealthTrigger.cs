using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthTrigger : MonoBehaviour
{
    public BaseHealth userBase;
    public HealthbarScript healthbar;
    Component monney;
    public Text message;
    string originalText;

    // Start is called before the first frame update
    void Start()
    {       
        originalText = message.text;
    }
    
    
    private void OnTriggerStay(Collider other)
    {
        monney = other.gameObject.GetComponent<CharacterCurrency>();

        if (other.gameObject.tag == "Player" && monney.GetComponent<CharacterCurrency>().getCurrency() >= 500 && userBase.health < 100)//when player has >500 monney and <full health
        {
            message.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E)) //increases health by 20 or max if base has taken less than 20 total damage
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
                monney.GetComponent<CharacterCurrency>().updateCurrency(-500);//reduces currency by 500
            }
        }
        else//Otherwise it displays the message 'unavailable'
        {
            message.text = "Unavailable";
            message.gameObject.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)//when moving away from buy station
    {//resets message to its original incase it was changed to 'unavailable' and unactivates it for next time
        message.text = originalText;
        message.gameObject.SetActive(false);
    }
}
