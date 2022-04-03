using UnityEngine;
using UnityEngine.UI;

public class Initializer : MonoBehaviour
{
    public Text playerName;

    public void setUpName(string name)
    {
        playerName.text = name;
    }

}
