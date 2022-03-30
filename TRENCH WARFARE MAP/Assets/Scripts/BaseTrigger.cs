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

    public void activateWall()
    {
        if (bought == false && money.GetComponent<CharacterCurrency>().getCurrency() >= 500)
        {
            bought = true;
            wall.SetActive(true);
            money.GetComponent<CharacterCurrency>().updateCurrency(-500);
        }else
        {
            message.text = "Unavailable!";
            message.gameObject.SetActive(true);
        }
    }
}