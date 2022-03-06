using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviourPunCallbacks
{
    
    #region Constants
    // Event: remote player has shot the cannon
    private const byte END_TURN = 1;

    #endregion
    //Issue: MyTurn and Turn are being set to default GameState (first value = Player1Move) on both clients
    public enum GameState { Player1Move, Player2Move, EMPTY, WIN };

    private GameState Winner;
    private GameState Turn;
    private GameState MyTurn;

    public GameState GetCurrentTurn(){
        return Turn;
    }
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Winner = GameState.EMPTY;
            MyTurn = GameState.Player1Move;
            Turn = GameState.Player1Move;
        }
        else
        {
            Winner = GameState.EMPTY;
            MyTurn = GameState.Player2Move;
            Turn = GameState.Player1Move;
        }
        Debug.Log("Winner: " + Winner.ToString() + ", MyTurn: " + MyTurn.ToString()
        + ", CurrentTurn: " + Turn.ToString());

    }

    public Player GetOpponent()
    {
        return PhotonNetwork.CurrentRoom.Players.Values.First(e => !e.IsLocal);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && Turn == MyTurn)
        {
            Debug.Log("my turn: " + MyTurn.ToString());
            Debug.Log("Current turn: " + Turn.ToString());
            EndTurn();
            Debug.Log("Changing turn");
            Debug.Log("my turn: " + MyTurn.ToString());
            Debug.Log("Current turn: " + Turn.ToString());

        }
        //if (photonView.IsMine && Winner != GameState.EMPTY && Input.GetKeyDown(KeyCode.Return))
        //{
        //    // The master player can reset the scene
        //    //PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().buildIndex);
        //    Debug.Log("RETURN TO LOBBY AFTER WINNER CHOSEN");
        //}
        
    }

    public void EndTurn()
    {
        Debug.Log("Ending turn");
        if (Winner != GameState.EMPTY)
        {
            // Game has finished, do nothing
            Debug.Log("Game has ended, cant change turn");
            Debug.Log("Winner = " + Winner.ToString());
            return;
        }
        else if (PhotonNetwork.IsConnected && Turn != MyTurn)
        {
            // We are in an online game, and it's not your turn!
            Debug.Log("cant change someone elses turn");
            return;
        }

        if (photonView.IsMine)
        {
            // Really change the cell
            Turn = GameState.Player2Move;
            Debug.Log("Turn changed");
        }
        else
        {
            Debug.Log("RAISING ENDTURN EVENT");
            // Send the move, but don't change the cell:
            // we do not own the game state.
            PhotonNetwork.RaiseEvent(END_TURN,
                null,
                RaiseEventOptions.Default,
                SendOptions.SendReliable);
        }
        Debug.Log("end of endturn function");
    }

    public void OnEvent(EventData photonEvent)
    {
        if (photonView.IsMine)
        {
            Debug.Log("On event triggered");
            switch (photonEvent.Code)
            {
                case END_TURN:
                    if (Turn == GameState.Player1Move)
                    {
                        Turn = GameState.Player2Move;
                    } 
                    else 
                    {
                        Turn = GameState.Player1Move;
                    }
                    break;
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(this.Winner);
            stream.SendNext(this.Turn);

        }
        else
        {
            this.Winner = (GameState)stream.ReceiveNext();
            this.Turn = (GameState)stream.ReceiveNext();

        }
    }
}