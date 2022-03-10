using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

  public void PlayGame()
  {
    if (PhotonNetwork.LocalPlayer.IsMasterClient) // only lets host to start the game
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
  }


  public void PlayGameOffline()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
  }

  public void QuitGame()
  {
    Application.Quit();
  }
}
