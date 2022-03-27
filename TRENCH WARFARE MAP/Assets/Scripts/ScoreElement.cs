using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreElement : MonoBehaviour
{

    public TMP_Text usernameText;
    public TMP_Text winsText;
    // public int rank;
    public TMP_Text rank;

    public void NewScoreElement(string _rank, string _username, int _wins)
    {
        rank.text = _rank.ToString();
        usernameText.text = _username;
        winsText.text = _wins.ToString();
    }
}
