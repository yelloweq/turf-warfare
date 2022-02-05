using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System.Collections.Generic;
using System.Linq;

public enum GameState { Win, Loss, Player1Move, Player2Move, EMPTY };

public class GameManager : MonoBehaviourPunCallbacks
{
    #region Constants
    // Event: remote player has shot the cannon
    public const int EVENT_MOVE = 1;

    #endregion


    private GameState _winner;
    public GameState Winner
    {
        get
        {
            return _winner;
        }
        private set
        {
            _winner = value;

            switch (value)
            {
                case MarkType.O:
                case MarkType.X:
                    string winnerName;
                    if (PhotonNetwork.IsConnected)
                    {
                        winnerName = MyTurn == value
                            ? PhotonNetwork.NickName
                            : GetOpponent().NickName;
                    }
                    else
                    {
                        winnerName = value.ToString();
                    }

                    turnText.text = photonView.IsMine
                        ? $"Winner: {winnerName}! - SPACE to reset, ESC to quit"
                        : $"Winner: {winnerName}! - ESC to quit";
                    break;

                case MarkType.TIE:
                    turnText.text = photonView.IsMine
                        ? "Tied! - SPACE to reset, ESC to quit"
                        : "Tied! - ESC to quit";
                    break;
            }
        }
    }

    private GameState MyTurn;

    void Start()
    {
        if (photonView.IsMine)
        {
            Winner = GameState.EMPTY;
            MyTurn = GameState.Player1Move;
            Turn = GameState.Player1Move;
        }
        else
        {
            MyTurn = GameState.Player2Move;
        }
    }

    void Update()
    {
        if (photonView.IsMine && Winner != GameState.EMPTY && Input.GetKeyDown(KeyCode.Space))
        {
            // The master player can reset the scene
            PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().buildIndex);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}