using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerTrigger : MonoBehaviour
{
    public GameObject tower;
    public Text message;
    public Currency currency;

    public bool bought;

    string originalText;

    // Start is called before the first frame update
    void Start()
    {
        bought = false;
        originalText = message.text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && bought == false)
        {
            message.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                bought = true;
                tower.SetActive(true);
                currency.updateCurrency(-500);
            }
        }
        else
        {
            message.text = "You have already upgraded!";
            message.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        message.text = originalText;
        message.gameObject.SetActive(false);
    }
}
