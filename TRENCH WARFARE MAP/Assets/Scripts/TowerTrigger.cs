using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TowerTrigger : MonoBehaviour
{
    public Text message;
    Component monney;
    public bool bought;
    string originalText;
    private void OnTriggerStay(Collider other)
    {
        monney = other.gameObject.GetComponent<CharacterCurrency>();
        if (other.gameObject.tag == "Player" && monney.GetComponent<CharacterCurrency>().getCurrency() >= 500 && bought == false)
        { // if the trigger was a player that is me
                message.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    bought = true;
                    monney = other.gameObject.GetComponent<CharacterCurrency>();
                    monney.GetComponent<CharacterCurrency>().updateCurrency(-500);
                }
            

        }
        else
        {
            message.text = "Unavailable!";
            message.gameObject.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        message.text = originalText;
        message.gameObject.SetActive(false);
    }
}