using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreElement : MonoBehaviour
{

  public TMP_Text usernameText;
  public TMP_Text winsText;

  public void NewScoreElement(string _username, int _wins)
  {
    usernameText.text = _username;
    winsText.text = _wins.ToString();
  }
}
