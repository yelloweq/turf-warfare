using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreElement : MonoBehaviour
{

    public TMP_Text usernameText;
    public TMP_Text winsText;
    public TMP_Text rank;
    public TMP_Text winsPercentage;

    public void NewScoreElement(string _rank, string _username, int _wins, int _winsPercentage)
    {
        rank.text = _rank;
        usernameText.text = _username;
        winsText.text = _wins.ToString();
        winsPercentage.text = _winsPercentage.ToString();
    }
}
