using UnityEngine;
using UnityEngine.UI;

public class Currency : MonoBehaviour
{
    private int currency = 1000;
    public Text funds;

    // Start is called before the first frame update
    private void Start()
    {
        funds = GameObject.Find("Funds").GetComponent<Text>();
        funds.text = "Funds: £" + currency;
    }

    // Update is called once per frame
    void UpdateText()
    {
        funds.text = "Funds: £" + currency;
    }

    public int getCurrency()
    {
        return currency;
    }

    public void updateCurrency(int currency)
    {
        this.currency += currency;

        UpdateText();
    }

}
