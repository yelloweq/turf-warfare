using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Photon.Pun;
using System.Collections;
using TMPro;
using UnityEngine;


public class GameExiterTemporary : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update

    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;
    public DatabaseReference DBreference;
    public TMP_Text currentWinsText;

    private TurnTracking turnTracking;
    public GameObject playerLeftScreen;
    void Start()
    {
        turnTracking = GameObject.Find("GameManager").GetComponent<TurnTracking>();
        playerLeftScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        exitGame();
        IncrementWinsC();
        checkIfPlayerLeft();

    }

    private void Awake() // connects to the database
    {
        //Check that everything necessary for Firebase is present
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                InitializeFireBase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    public void InitializeFireBase()
    {
        Debug.Log("Setting up Firebase Auth");
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void exitGame() // Exit game when 'P' is pressed ----- to add when either win/lose screen appears
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Escape pressed");
            Debug.Log("Current room is " + PhotonNetwork.CurrentRoom.Name);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Pressed P");
            if (PhotonNetwork.InRoom == true)
            {
                PhotonNetwork.LeaveRoom();
            }
        }
    }

    public override void OnLeftRoom()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        PhotonNetwork.Disconnect();
        PhotonNetwork.LoadLevel("MainMenu");
    }

    public IEnumerator incrementWins() // Add win when 'B' is pressed ---- to add when the win screen appears
    {
        if (turnTracking.hasWon())
        {
            if (auth.CurrentUser != null)
            {

                var DBTask = DBreference.Child("users").Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).GetValueAsync();

                yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

                if (DBTask.Result != null)
                {
                    DataSnapshot snapshot = DBTask.Result;

                    int currentWins = int.Parse(snapshot.Child("wins").Value.ToString());

                    var DBTask2 = DBreference.Child("users").Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).Child("wins").SetValueAsync(currentWins + 1);

                    yield return new WaitUntil(predicate: () => DBTask2.IsCompleted);
                    Debug.Log("Win added! Check leaderboard.");
                    if (DBTask.Exception != null)
                    {
                        Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
                    }
                }

            }
            else
            {
                print("Currently playing as guest, so win's won't be recorded.");
            }
        }

        yield return new WaitForSeconds(3);
    }

    public void IncrementWinsC() //couroutine to increment win
    {
        StartCoroutine(incrementWins());
    }

    private IEnumerator LoadUserWins()
    {
        //Get the currently logged in user data
        var DBTask = DBreference.Child("users").Child(User.UserId).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else if (DBTask.Result.Value == null)
        {
            //No data exists yet
            currentWinsText.text = "no data found";
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;

            currentWinsText.text = "Current wins: " + snapshot.Child("wins").Value.ToString();

        }
    }

    public void checkIfPlayerLeft()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount < 2)
        {
            playerLeftScreen.SetActive(true);
            exitGame();
        }
    }

}
