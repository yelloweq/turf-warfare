using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class NetworkManager : MonoBehaviourPunCallbacks
{
  [Header("MainMenu Panel")]
  public GameObject MainMenuUIPanel;
  public GameObject RegisterMenu;
  public GameObject LoginMenu;

  [Header("Connecting Panel")]
  public GameObject ConnectingUIPanel;


  [Header("GameOptions Panel")]
  public GameObject GameOptionsUIPanel;
  public InputField playerNameInput;
  public GameObject WelcomeMessage;


  [Header("Create Room Panel")]
  public GameObject CreateRoomUIPanel;
  public InputField createRoomInput;
  public GameObject joinFailMessage;

  [Header("Join Room Panel")]
  public GameObject JoinRoomUIPanel;
  public InputField joinRoomInput;

  [Header("Inside Room Panel")]
  public GameObject InsideRoomUIPanel;
  public TextMeshProUGUI roomInfoText;
  public GameObject playerListPrefab;
  public GameObject playerListContent;

  public Text connectionStatusText;

  private Dictionary<int, GameObject> playerListGameObjects;

  #region Unity Methods
  void Start()
  {
    connectionStatusText.gameObject.SetActive(false);
    PhotonNetwork.AutomaticallySyncScene = true;  //This line will synchronise scenes between all players inside the room
    ActivatePanel(MainMenuUIPanel.name);          //Activating the Main Menu Panel and deactivating all other panels
  }

  void Update()
  {
    connectionStatusText.text = PhotonNetwork.NetworkClientState.ToString();
  }

  #endregion




  #region Photon Callbacks

  public override void OnConnectedToMaster()
  {
    ActivatePanel(GameOptionsUIPanel.name);
  }

  public override void OnJoinedRoom()
  {
    ActivatePanel(InsideRoomUIPanel.name);
    roomInfoText.text = "Room Name : " + PhotonNetwork.CurrentRoom.Name;

    if (playerListGameObjects == null)
    {
      playerListGameObjects = new Dictionary<int, GameObject>();

    }


    foreach (Player player in PhotonNetwork.PlayerList)
    {
      GameObject playerGameObject = Instantiate(playerListPrefab);
      playerGameObject.transform.parent = playerListContent.transform;
      playerGameObject.transform.localScale = Vector3.one;
      playerGameObject.GetComponent<Initializer>().setUpName(player.NickName);


      playerListGameObjects.Add(player.ActorNumber, playerGameObject);
    }


  }

  public override void OnPlayerEnteredRoom(Player newPlayer)
  {
    GameObject playerGameObject = Instantiate(playerListPrefab);
    playerGameObject.transform.parent = playerListContent.transform;
    playerGameObject.transform.localScale = Vector3.one;
    playerGameObject.GetComponent<Initializer>().setUpName(newPlayer.NickName);


    playerListGameObjects.Add(newPlayer.ActorNumber, playerGameObject);
  }

  public override void OnPlayerLeftRoom(Player otherPlayer)
  {
    Destroy(playerListGameObjects[otherPlayer.ActorNumber].gameObject);
    playerListGameObjects.Remove(otherPlayer.ActorNumber);
  }

  public override void OnLeftRoom()
  {
    ActivatePanel(ConnectingUIPanel.name);

    foreach (GameObject p in playerListGameObjects.Values)
    {
      Destroy(p);

    }
    playerListGameObjects.Clear();
    playerListGameObjects = null; ;


  }

  public override void OnJoinRandomFailed(short returnCode, string message)
  {
    ActivatePanel(CreateRoomUIPanel.name);
    StartCoroutine(ShowErrorMessage());
  }

  public override void OnJoinRoomFailed(short returnCode, string message)
  {
    ActivatePanel(CreateRoomUIPanel.name);
    StartCoroutine(ShowErrorMessage());
  }

  #endregion




  #region UI Methods

  public void OnPlayAsGuestButtonClicked()
  {
    ActivatePanel(ConnectingUIPanel.name);
    connectionStatusText.gameObject.SetActive(true);
    PhotonNetwork.ConnectUsingSettings();
    WelcomeMessage.SetActive(false);
  }

  public void OnCreateRoomButtonClicked(bool isPublic)
  {
    ActivatePanel(ConnectingUIPanel.name);

    string roomName = createRoomInput.text;

    if (string.IsNullOrEmpty(roomName))
    {
      roomName = "Room" + Random.Range(100, 1000);
    }

    print(roomName);



    string playerName = playerNameInput.text;

    if (string.IsNullOrEmpty(playerName))
    {
      playerName = "Player" + Random.Range(100, 1000);
    }
    PhotonNetwork.LocalPlayer.NickName = playerName;

    RoomOptions roomOptions = new RoomOptions();
    roomOptions.MaxPlayers = 2;
    roomOptions.IsVisible = isPublic;

    PhotonNetwork.CreateRoom(roomName, roomOptions);
  }

  public void OnJoinRoomButtonClicked(bool isRandom)
  {


    string playerName = playerNameInput.text;

    if (string.IsNullOrEmpty(playerName))
    {
      playerName = "Player" + Random.Range(100, 1000);
    }
    PhotonNetwork.LocalPlayer.NickName = playerName;


    if (!isRandom)
    {
      string roomName = joinRoomInput.text;
      if (!string.IsNullOrEmpty(roomName))
      {
        ActivatePanel(ConnectingUIPanel.name);
        PhotonNetwork.JoinRoom(roomName);
      }
    }
    else if (isRandom)
    {
      PhotonNetwork.JoinRandomRoom();
    }
  }

  public void OnPlayGameButtonClicked(string levelname)
  {
    if (PhotonNetwork.LocalPlayer.IsMasterClient)
    {
      PhotonNetwork.LoadLevel(levelname);
    }

  }

  public void OnLeaveGameButtonClicked()
  {
    ActivatePanel(GameOptionsUIPanel.name);
    PhotonNetwork.LeaveRoom();
  }

  public void OnBackButtonClicked()
  {
    if (PhotonNetwork.IsConnected)
    {
      PhotonNetwork.Disconnect();
    }

    ActivatePanel(MainMenuUIPanel.name);
  }

  public void OnLoginButtonClicked()
  {
    ActivatePanel(LoginMenu.name);
  }

  public void OnRegisterButtonClicked()
  {
    ActivatePanel(RegisterMenu.name);
  }

  public void OnQuitButtonClicked()
  {
    Application.Quit();
  }


  #endregion



  #region public Methods

  public IEnumerator ShowErrorMessage()
  {
    joinFailMessage.SetActive(true);
    yield return new WaitForSeconds(3);
    joinFailMessage.SetActive(false);
  }

  public void ActivatePanel(string panelTobeActivated)
  {
    //this functions activates only one panel and deactivates all other panels
    ConnectingUIPanel.SetActive(ConnectingUIPanel.name.Equals(panelTobeActivated));
    MainMenuUIPanel.SetActive(MainMenuUIPanel.name.Equals(panelTobeActivated));
    GameOptionsUIPanel.SetActive(GameOptionsUIPanel.name.Equals(panelTobeActivated));
    CreateRoomUIPanel.SetActive(CreateRoomUIPanel.name.Equals(panelTobeActivated));
    JoinRoomUIPanel.SetActive(JoinRoomUIPanel.name.Equals(panelTobeActivated));
    InsideRoomUIPanel.SetActive(InsideRoomUIPanel.name.Equals(panelTobeActivated));
    LoginMenu.SetActive(LoginMenu.name.Equals(panelTobeActivated));
    RegisterMenu.SetActive(RegisterMenu.name.Equals(panelTobeActivated));
  }

  #endregion

}
