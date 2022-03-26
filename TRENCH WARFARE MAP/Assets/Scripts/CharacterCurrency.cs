using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCurrency : MonoBehaviour
{
    private float currency = 1000f;
    public Text funds;

    // Start is called before the first frame update
    private void Start()
    {
        funds = GameObject.Find("Funds").GetComponent<Text>();
        funds.text = "Funds: �" + currency;
    }

    // Update is called once per frame
    void UpdateText()
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

        UpdateText();
    }
}
