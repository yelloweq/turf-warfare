using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;


public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    GameObject findMatchBtn;
    [SerializeField]   
    GameObject searchingPanel;

    void Start()
    {
        findMatchBtn.SetActive(false);
        searchingPanel.SetActive(false);
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("CONNECTED ON: " + PhotonNetwork.CloudRegion + " Server.");
        //Make both players see the same scene
        PhotonNetwork.AutomaticallySyncScene = true;
        findMatchBtn.SetActive(true);
    }

    public void FindMatch()
    {
        searchingPanel.SetActive(true);
        findMatchBtn.SetActive(false);

        //Try to find lobby
        PhotonNetwork.JoinRandomRoom();
        Debug.Log("Searching for a game.");
    }

    public void StopSearch()
    {
        searchingPanel.SetActive(false);
        findMatchBtn.SetActive(true);

        //Leave Match
        PhotonNetwork.LeaveRoom();
        Debug.Log("Quitting to menu"); 
        
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to find a game, creating new room");
        MakeRoom();
    }

    void MakeRoom()
    {
        //Choose random room name, set up 2 player match
        int randomRoomName = Random.Range(0, 5000);
        RoomOptions roomOptions = 
        new RoomOptions()
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = 2
        };
        PhotonNetwork.CreateRoom("RoomName_" + randomRoomName, roomOptions);
        Debug.Log("Room created, waiting for players...");
    }

    public override void OnPlayerEnteredRoom(Player player)
    {
        //Switch to scene with index 1 when both players connect
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2 && PhotonNetwork.IsMasterClient)
        {
            Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount + "/2 Starting Game");
            PhotonNetwork.LoadLevel(1);
        }
    }
}
