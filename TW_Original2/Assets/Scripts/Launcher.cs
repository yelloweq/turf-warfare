// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;
// using Photon.Pun;
// using Photon.Realtime;
// using TMPro;

// public class Launcher : MonoBehaviourPunCallbacks
// {
//   //   [SerializeField]
//   //   GameObject findMatchBtn;
//   //   [SerializeField]
//   //   GameObject searchingPanel;
//   [SerializeField] TMP_InputField roomNameInputField;
//   [SerializeField] TMP_Text errorText;

//   void Start()
//   {
//     //disable menu buttons until connection to server established
//     // findMatchBtn.SetActive(false);
//     // searchingPanel.SetActive(false);
//     Debug.Log("Connecting to Master.");
//     PhotonNetwork.ConnectUsingSettings();
//   }

//   public override void OnConnectedToMaster()
//   {
//     //Once connected, sync scene for both players
//     // base.OnConnectedToMaster();

//     Debug.Log("Connected to: " + PhotonNetwork.CloudRegion + " Server");
//     PhotonNetwork.JoinLobby();

//     //enable match search
//     // findMatchBtn.SetActive(true);
//   }

//   public override void OnJoinedLobby()
//   {
//     MenuManager.Instance.OpenMenu("MainMenu");
//     Debug.Log("Joined Lobby");
//   }

//   void MakeRoom()
//   {
//     //use random game id, set game options to 2 players
//     int randomRoom = Random.Range(0, 500);
//     RoomOptions roomOptions =
//     new RoomOptions()
//     {
//       IsVisible = true,
//       IsOpen = true,
//       MaxPlayers = 2
//     };

//     PhotonNetwork.CreateRoom("GameID:" + randomRoom, roomOptions);
//     Debug.Log("created room, waiting for players");
//   }

//   public override void OnPlayerEnteredRoom(Player player)
//   {
//     //when both players connect, start game
//     if (PhotonNetwork.CurrentRoom.PlayerCount == 2 && PhotonNetwork.IsMasterClient)
//     {
//       Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount + "/2 Starting game");
//       PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
//     }
//   }

//   public void CancelSearch()
//   {
//     //leave lobby if already created, activate find match button
//     // searchingPanel.SetActive(false);
//     // findMatchBtn.SetActive(true);
//     PhotonNetwork.LeaveRoom();
//     Debug.Log("Search cancelled");
//   }

//   public void MainMenu()
//   {
//     PhotonNetwork.Disconnect();
//     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
//   }

//   public void CreateRoom()

//   {
//     if (string.IsNullOrEmpty(roomNameInputField.text))
//     {
//       return;
//     }
//     PhotonNetwork.CreateRoom(roomNameInputField.text);
//     MenuManager.Instance.OpenMenu("loading");
//   }

//   public override void OnJoinedRoom()
//   {
//     Debug.Log("Joined room");
//     MenuManager.Instance.OpenMenu("room");
//   }

//   public override void OnCreateRoomFailed(short returnCode, string message)
//   {
//     errorText.text = "Room Creation Failed: " + message;
//     MenuManager.Instance.OpenMenu("error");
//   }

// }
