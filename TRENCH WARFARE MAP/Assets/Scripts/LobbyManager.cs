using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    GameObject findMatchBtn;
    [SerializeField]
    GameObject searchingPanel;

    void Start()
    {
        //disable menu buttons until connection to server established
        findMatchBtn.SetActive(false);
        searchingPanel.SetActive(false);
        Debug.Log("Connecting to server");
        PhotonNetwork.ConnectUsingSettings();
    }
    
    public override void OnConnectedToMaster()
    {
        //Once connected, sync scene for both players
        base.OnConnectedToMaster();

        Debug.Log("Connected to server on: " + PhotonNetwork.CloudRegion + " Server");
        PhotonNetwork.AutomaticallySyncScene = true;
        //enable match search
        findMatchBtn.SetActive(true);
    }
    
    public void FindMatch()
    {
        //show cancel button when looking for a match
        searchingPanel.SetActive(true);
        findMatchBtn.SetActive(false);

        PhotonNetwork.JoinRandomRoom();
        Debug.Log("Searching for a game");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        //make new match when none found
        Debug.Log("room not found, creating room");
        MakeRoom();
    }

    void MakeRoom()
    {
        //use random game id, set game options to 2 players
        int randomRoom = Random.Range(0, 500);
        RoomOptions roomOptions = 
        new RoomOptions()
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = 2
        };

        PhotonNetwork.CreateRoom("GameID:" + randomRoom, roomOptions);
        Debug.Log("created room, waiting for players");
    }

    public override void OnPlayerEnteredRoom(Player player)
    {
        //when both players connect, start game
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2 && PhotonNetwork.IsMasterClient)
        {
            Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount + "/2 Starting game");
            PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    
    public void CancelSearch()
    {
        //leave lobby if already created, activate find match button
        searchingPanel.SetActive(false);
        findMatchBtn.SetActive(true);
        PhotonNetwork.LeaveRoom();
        Debug.Log("Search cancelled");
    }

    public void MainMenu()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
