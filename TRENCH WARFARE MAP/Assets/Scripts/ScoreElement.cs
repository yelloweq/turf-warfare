using TMPro;
using UnityEngine;

public class ScoreElement : MonoBehaviour
{

    public TMP_Text usernameText;
    public TMP_Text winsText;
    public TMP_Text rank;

    public void NewScoreElement(string _rank, string _username, int _wins)
    {
        rank.text = _rank;
        usernameText.text = _username;
        winsText.text = _wins.ToString();
    }
}
