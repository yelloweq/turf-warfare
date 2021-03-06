using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

//TODO: GET CANNON CONTROLLER FOR MASTER AND CLIENT
// USE RESETCANNON FUNC AFTER CALLING PLAYER SWITCH
// DISABLE CANNON AFTER CHANGE OF TURN
public class TurnTracking : MonoBehaviourPunCallbacks, IPunObservable, IOnEventCallback
{

    #region Constants
    // Event: remote player has shot the cannon
    private const byte END_TURN = 1;
    private const byte GAME_OVER = 2;
    #endregion

    #region Variables
    public enum GameState { Player1Move = 0, Player2Move = 1, EMPTY = 3 };

    private GameState Winner;
    private GameState Turn;
    private GameState MyTurn;

    private CannonController cannonController;
    private GameObject WinScreen;
    private Text WinnerName;

    private GameExiterTemporary gameExiterTemporary;


    #endregion

    #region TurnGettingAndSetting

    public bool hasGameEnded()
    {
        return (Winner != GameState.EMPTY);
    }
    public bool hasWon()
    {
        return (Winner == MyTurn);
    }
    public GameState GetCurrentTurn()
    {
        return Turn;
    }

    public GameState GetMyTurn()
    {
        return MyTurn;
    }

    public bool CheckTurn()
    {
        return Turn == MyTurn;
    }

    public void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public IEnumerator FindController()
    {
        yield return new WaitForSeconds(2);

        cannonController = GameObject.Find("FriendlyCannon").GetComponent<CannonController>();
        Debug.Log("Looking for CannonHost Controller");

    }


    #endregion

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

        WinScreen = GameObject.Find("WinScreen");
        WinnerName = GameObject.Find("WinnerName").GetComponent<Text>();
        WinScreen.SetActive(false);
        gameExiterTemporary = GameObject.Find("GameExiterTemporary").GetComponent<GameExiterTemporary>();

        StartCoroutine(FindController());
    }

    public Player GetOpponent()
    {
        return PhotonNetwork.CurrentRoom.Players.Values.First(e => !e.IsLocal);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && Turn == MyTurn)
        {
            Debug.Log("MyTrun: " + MyTurn.ToString() + " Currently: " + Turn.ToString() + "Winner: " + Winner.ToString());
            EndTurn();
            Debug.Log("MyTrun: " + MyTurn.ToString() + " Currently: " + Turn.ToString() + "Winner: " + Winner.ToString());

        }

        //rely won game to local player
        if (Winner != GameState.EMPTY && !photonView.IsMine)
        {
            WinScreen.SetActive(true);
            WinnerName.text = "Player2";
        }
        //if (photonView.IsMine && Winner != GameState.EMPTY && Input.GetKeyDown(KeyCode.Return))
        //{
        //    // The master player can reset the scene
        //    //PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().buildIndex);
        //    Debug.Log("RETURN TO LOBBY AFTER WINNER CHOSEN");
        //}

    }

    #region EventTracking
    public void EndTurn()
    {
        Debug.Log("============= TURN END ============");
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
            // Change state
            Turn = GameState.Player2Move;
            cannonController = GameObject.Find("FriendlyCannon").GetComponent<CannonController>();
            StartCoroutine(cannonController.ResetCannon());
            Debug.Log("STARTING COROUTINE: RESET CLIENT CANNON");
        }
        else
        {
            Debug.Log(Winner);
            object[] content = new object[] { GameState.Player2Move };
            PhotonNetwork.RaiseEvent(END_TURN,
                content,
                RaiseEventOptions.Default,
                SendOptions.SendReliable);
        }
    }

    public void EndGame()
    {
        Debug.Log(Winner);
        Debug.Log("============= GAME ENDED ============");
        if (photonView.IsMine)
        {
            // player1 base destroyed, set winner to player2
            Winner = GameState.Player2Move;
            WinScreen.SetActive(true);
            WinnerName.text = "Player2";
        }
        else
        {
            // player2 base destroyed, send winner to be set to player1
            object[] content = new object[] { GameState.Player1Move };
            PhotonNetwork.RaiseEvent(GAME_OVER,
                content,
                RaiseEventOptions.Default,
                SendOptions.SendReliable);

            WinScreen.SetActive(true);
            WinnerName.text = "Player1";
        }
        Debug.Log(Winner);
    }

    public void OnEvent(EventData photonEvent)
    {
        object[] data = (object[])photonEvent.CustomData;
        if (PhotonNetwork.IsMasterClient)
        {
            switch (photonEvent.Code)
            {
                case END_TURN:
                    try
                    {
                        if ((int)GameState.Player2Move == (int)data[0])
                        {
                            Turn = GameState.Player1Move;
                            cannonController = GameObject.Find("EnemyCannon").GetComponent<CannonController>();
                            StartCoroutine(cannonController.ResetCannon());
                            Debug.Log("STARTING COROUTINE: RESET HOST CANNON");
                        }
                        break;
                    }
                    catch (System.Exception ex)
                    {
                        Debug.Log("END_TURN ERROR: " + ex);
                        break;
                    }
                case GAME_OVER:
                    try
                    {
                        if ((int)GameState.Player1Move == (int)data[0])
                        {
                            Debug.Log("=================HOST WINS!=========================");
                            WinScreen.SetActive(true);
                            WinnerName.text = "Player1";
                            Winner = GameState.Player1Move;

                        }
                    }
                    catch (System.Exception ex)
                    {
                        Debug.Log("GAME_OVER ERROR: " + ex);
                    }

                    Debug.Log(Winner);
                    break;
            }
        }

        //Debug.Log("MyTurn:   " + MyTurn.ToString() + "   Currently: " +  Turn.ToString() + "    Winner:     " + Winner.ToString());
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            try
            {
                stream.SendNext((int)Winner);
                stream.SendNext((int)Turn);
            }
            catch (System.Exception ex)
            {
                Debug.Log("WRITING: " + ex);
            }


        }
        if (stream.IsReading)
        {
            try
            {
                this.Winner = (GameState)(stream.ReceiveNext());
                this.Turn = (GameState)(stream.ReceiveNext());
            }
            catch (System.InvalidCastException ex)
            {
                Debug.Log("ERROR WHILE TRYING TO RECEIVE GAME TURN: " + ex);
            }


        }
    }
    #endregion
}