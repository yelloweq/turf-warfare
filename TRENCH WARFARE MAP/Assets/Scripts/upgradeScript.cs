using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
public class upgradeScript : MonoBehaviourPun
{

    private BaseHealth userBase;
    private HealthbarScript healthbar;
    private Currency money;
    public Text message;
    string originalText;
    public bool bought;
    private string successMessage;
    private string errorMessage;
    public GameObject wallPosition;
    public GameObject wall;

    private PhotonView PV;
    // Start is called before the first frame update
    void Start()
    {
        money = GameObject.Find("CurrencyManager").GetComponent<Currency>();
        successMessage = "ITEM BOUGHT SUCCESSFULLY";
        errorMessage = "ITEM UNAVAILABLE";
        userBase = GameObject.Find("FriendlyBase").GetComponent<BaseHealth>();
        healthbar = GameObject.Find("P1Healthbar").GetComponent<HealthbarScript>();
        PV = GameObject.Find("FriendlyBase").GetComponent<PhotonView>();
    }

    public void buyHealth()
    {
        if (!userBase)
        {
            userBase = GameObject.Find("FriendlyBase").GetComponent<BaseHealth>();
        }
        if (!money)
        {
            money = GameObject.Find("CurrencyManager").GetComponent<Currency>();
        }

        Debug.Log("money " + money.getCurrency());
        Debug.Log("health" + userBase.GetHealth());
        if (money.getCurrency() >= 500 && userBase.GetHealth() < 1000)
        {
            message.text = successMessage;
            message.color = Color.green;
            message.gameObject.SetActive(true);

            if (userBase.GetHealth() >= 800)
            {
                userBase.restoreHealth();
            }
            else
            {
                userBase.increaseHealth(200);
            }
            money.updateCurrency(-500);   //reduces currency by 500
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
        if (bought == false && money.getCurrency() >= 500)
        {
            message.text = successMessage;
            message.color = Color.green;
            message.gameObject.SetActive(true);

            bought = true;
            money.updateCurrency(-500);
            if (!PV)
            {
                PV = GameObject.Find("FriendlyBase").GetComponent<PhotonView>();
            }
            if (PV.IsMine)
            {
                //Vector3 wallpos = GameObject.Find("FriendlyBase").transform.position + new Vector3(0, 0, 20);
                PhotonNetwork.Instantiate(wall.name, wallPosition.transform.position, wallPosition.transform.rotation, 0);

            }
        }
        else
        {
            message.text = errorMessage;
            message.color = Color.red;
            message.gameObject.SetActive(true);
        }
    }
}