using UnityEngine;
using UnityEngine.UI;
public class BaseTrigger : MonoBehaviour
{
    public GameObject wall;
    public Text message;
    GameObject money;
    public bool bought;
    string originalText;
    // Update is called once per frame
    void Start()
    {
        money = GameObject.FindGameObjectWithTag("Player");
    }
    //responsible for activating the wall in front of the players base.
    public void activateWall()
    {
        /* Checks if the player hasn't already bought the item and if they have
           enough money. If true then subtract 500 and activate. Otherwise provide
           error message.*/
        if (bought == false &&
        money.GetComponent<Currency>().getCurrency() >= 500)
        {
            bought = true;
            wall.SetActive(true);
            money.GetComponent<Currency>().updateCurrency(-500);
        }
        else
        {
            message.text = "Unavailable!";
            message.gameObject.SetActive(true);
        }
    }
}
