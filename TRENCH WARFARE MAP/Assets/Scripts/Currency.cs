using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Currency : MonoBehaviour
{
    private float currency = 1000f;
    public Text funds;

    // Start is called before the first frame update
    private void Start()
    {
        // funds = GameObject.GetComponent<Text>();
        funds.text = "Funds: £" + currency;
    }

    // Update is called once per frame
    void Update()
    {
        funds.text = "Funds: £" + currency;
    }

    public float getCurrency()
    {
        return currency;
    }

    public void updateCurrency(float currency)
    {
        this.currency += currency;
    }
}
