using UnityEngine;
using UnityEngine.UI;
public class BaseTrigger : MonoBehaviour
{
    public GameObject wall;
    public Text message;
    Component monney;
    public bool bought;
    string originalText;
    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay(Collider other)
    {
        monney = other.gameObject.GetComponent<CharacterCurrency>();
        if (other.gameObject.tag == "Player" && bought == false && monney.GetComponent<CharacterCurrency>().getCurrency() >= 500)
        {
            message.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                bought = true;
                wall.SetActive(true);
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