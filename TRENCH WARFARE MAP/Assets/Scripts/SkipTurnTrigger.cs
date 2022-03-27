using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SkipTurnTrigger : MonoBehaviour
{
    public Text message;
    Component monney;
    string originalText;
    public BattleSystem gameManager;

    // Start is called before the first frame update
    void Start()
    {
        originalText = message.text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player"){
              
            message.text = "Press 'E' to skip your chance to attack for £500!";
            message.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                monney = other.gameObject.GetComponent<CharacterCurrency>();
                monney.GetComponent<CharacterCurrency>().updateCurrency(+500);
                //implement skip turn
                gameManager.PlayerSwitch();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        message.text = originalText;
        message.gameObject.SetActive(false);
    }
}
