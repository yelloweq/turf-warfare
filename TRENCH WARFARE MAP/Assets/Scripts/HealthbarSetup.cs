using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
public class HealthbarSetup : MonoBehaviourPun
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject P1Heathbar;
    [SerializeField]
    private GameObject P2Heathbar;

    [SerializeField]
    private GameObject P1HeathbarText;
    [SerializeField]
    private GameObject P2HeathbarText;

    [SerializeField]
    private GameObject P1HeathbarFill;
    [SerializeField]
    private GameObject P2HeathbarFill;


    void Start()
    {
        P2HeathbarText.GetComponent<Text>().text = "Enemy HP";
        P1HeathbarText.GetComponent<Text>().text = "HP";
    }

    public GameObject GetHealthbar()
    {
        if (photonView.IsMine)
        {
            return P1Heathbar;
        }
        else
        {
            return P2Heathbar;
        }
    }

}
