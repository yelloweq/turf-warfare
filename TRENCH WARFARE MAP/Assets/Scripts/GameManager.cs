using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System.Collections;
using System.Linq;

//TODO: GET CANNON CONTROLLER FOR MASTER AND CLIENT
// USE RESETCANNON FUNC AFTER CALLING PLAYER SWITCH
// DISABLE CANNON AFTER CHANGE OF TURN
public class GameManager : MonoBehaviourPunCallbacks, IPunObservable, IOnEventCallback
{
    
    #region Constants
    // Event: remote player has shot the cannon
    private const byte END_TURN = 1;
    private const byte WIN_MATCH = 2;
    #endregion
    //Issue: MyTurn and Turn are being set to default GameState (first value = Player1Move) on both clients
    public enum GameState { Player1Move, Player2Move, EMPTY, WIN };

    private GameState Winner;
    private GameState Turn;
    private GameState MyTurn;

    private CannonController cannonController;

    public GameState GetCurrentTurn(){
        return Turn;
    }

    public GameState GetMyTurn(){
        return MyTurn;
    }

    private bool CheckTurn()
    {
        return Turn == MyTurn;
    }

    public void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
        
        
    }

    public IEnumerator FindController()
    {
        yield return new WaitForSeconds(2);

        if(PhotonNetwork.IsMasterClient)
        {
            cannonController = GameObject.Find("CannonHost").GetComponent<CannonController>();
            Debug.Log("Looking for CannonHost Controller");
        }
        else
        {
            cannonController = GameObject.Find("CannonClient").GetComponent<CannonController>();
            Debug.Log("Looking for CannonClient Controller");
        }
    }

    public void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
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
            Debug.Log("MyTrun: " + MyTurn.ToString() + " Currently: " +  Turn.ToString() + "Winner: " + Winner.ToString());
            EndTurn();
            Debug.Log("MyTrun: " + MyTurn.ToString() + " Currently: " +  Turn.ToString() + "Winner: " + Winner.ToString());

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
            if (!cannonController){
                cannonController = GameObject.Find("CannonClient").GetComponent<CannonController>();
            }
            StartCoroutine(cannonController.ResetCannon());
            Debug.Log("STARTING COROUTINE: RESET CLIENT CANNON");
        }
        else
        {
            //Debug.Log(">>>>>>>>>>>>>>>>>>>>> END_TURN EVENT <<<<<<<<<<<<<<<<<<");
            // Send event, dont change state:
            object[] content = new object[] {MyTurn};
            PhotonNetwork.RaiseEvent(END_TURN,
                content,
                RaiseEventOptions.Default,
                SendOptions.SendReliable);

                if (!cannonController){
                cannonController = GameObject.Find("CannonHost").GetComponent<CannonController>();
            }
               StartCoroutine(cannonController.ResetCannon());
               Debug.Log("STARTING COROUTINE: RESET HOST CANNON");
           // Debug.Log(">>>>>>>>>>>>>>>>>>>>> EVENT SENT <<<<<<<<<<<<<<<<<<");
        }
    }

    public void OnEvent(EventData photonEvent)
    {
        object[] data = (object[])photonEvent.CustomData;
        if (PhotonNetwork.IsMasterClient)
        {
            switch (photonEvent.Code)
            {
                //CONVERTING data[0] causes InvalidCastException
                //Tried casting Int normally and passing it as GameState but doesnt work
                //The turn does change...

                case END_TURN:
                try
                {
                    if (GameState.Player2Move == (GameState)System.Convert.ToInt32(data[0])){
                        Turn = GameState.Player1Move;
                    }
                    if ((GameState)System.Convert.ToInt32(data[0]) == GameState.Player1Move){
                        Turn = GameState.Player2Move;
                        
                    }
                }
                catch (System.InvalidCastException ex)
                {
                     Debug.Log("Casting data to gamestate err: " + ex);
                }
                    
                    
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
               this.Winner = (GameState)System.Convert.ToInt16(stream.ReceiveNext());
                this.Turn = (GameState)System.Convert.ToInt16(stream.ReceiveNext()); 
            }
            catch (System.InvalidCastException ex)
            {
                 Debug.Log("READING: " + ex);
            }
            

        }
    }
}