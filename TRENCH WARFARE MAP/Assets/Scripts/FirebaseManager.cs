using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using Firebase.Database;
using Firebase.Extensions;
using System.Linq;

public class FirebaseManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject ConnectingUIPanel;
    public GameObject MainMenuUIPanel;
    public GameObject LeaderboardUIPanel;

    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;
    public DatabaseReference DBreference;

    //Login variables
    [Header("Login")]
    public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginField;
    public TMP_Text warningLoginText;
    public TMP_Text confirmLoginText;

    //Register variables
    [Header("Register")]
    public TMP_InputField usernameRegisterField;
    public TMP_InputField emailRegisteredField;
    public TMP_InputField passwordRegisteredField;
    public TMP_InputField passwordRegisterVerifyField;
    public TMP_Text warningRegisterText;
    public TMP_Text confirmRegisterText;

    [Header("Experiment")]
    public InputField createRoomInput;
    public TMP_InputField playerNameInput;
    public InputField joinRoomInput;
    public Text connectionStatusText;
    public TMP_Text WelcomeMessage;
    public GameObject InputName;
    public bool loggedin;
    public TMP_Text currentWinsText;

    [Header("Scoreboard")]
    public GameObject scoreElement;
    public Transform scoreboardContent;


    void Start()
    {
        connectionStatusText.gameObject.SetActive(false);
        PhotonNetwork.AutomaticallySyncScene = true;  //This line will synchronise scenes between all players inside the room
    }

    void Update()
    {
        connectionStatusText.text = PhotonNetwork.NetworkClientState.ToString();
    }

    private void Awake()
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

    //LoginButton function
    public void LoginButton()
    {
        StartCoroutine(Login(emailLoginField.text, passwordLoginField.text));

    }

    //RegisterButton function
    public void RegisterButton()
    {
        StartCoroutine(Register(emailRegisteredField.text, passwordRegisteredField.text, usernameRegisterField.text));
    }

    private IEnumerator Login(string _email, string _password)
    {
        //Login user
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        //Wait until task is complete
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            //If errors are present
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Incorrect email or password!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Please insert your email!";
                    break;
                case AuthError.InvalidEmail:
                    message = "Email is invalid!";
                    break;
                case AuthError.MissingPassword:
                    message = "Password is missing!";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist. Please register :)";
                    break;
            }
            warningLoginText.text = message;
            yield return new WaitForSeconds(3);
            warningLoginText.text = "";
        }
        else
        {
            //Login succesful
            User = LoginTask.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);

            confirmLoginText.text = "Logged in!";
            loggedin = true;
            StartCoroutine(LoadUserWins());

            OnLoginButtonClicked();

        }
    }

    public void clearLoginFields()
    {
        emailLoginField.text = "";
        passwordLoginField.text = "";
        confirmLoginText.text = "";
    }

    public void clearRegisterFields()
    {
        usernameRegisterField.text = "";
        emailRegisteredField.text = "";
        passwordRegisteredField.text = "";
        passwordRegisterVerifyField.text = "";
    }

    private IEnumerator Register(string _email, string _password, string _username)
    {
        if (_username == "")
        {
            warningRegisterText.text = "Please insert your username!";
        }
        else if (passwordRegisteredField.text != passwordRegisterVerifyField.text)
        {
            warningRegisterText.text = "Passwords do not match!!";
        }
        else
        {
            //Register the user
            var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);

            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Registering Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Please insert your email!";
                        break;
                    case AuthError.InvalidEmail:
                        message = "Email is invalid!";
                        break;
                    case AuthError.MissingPassword:
                        message = "Please insert your password!";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email already exists! Please log in.";
                        break;

                }

                warningRegisterText.text = message;
                confirmRegisterText.text = "";
            }
            // else if (checkExistingUsername(_username))
            // {
            //   warningRegisterText.text = "Username already taken!";
            //   yield return new WaitForSeconds(3);
            //   warningRegisterText.text = "";
            // }
            else
            {
                //User created!
                User = RegisterTask.Result;
                if (User != null)
                {
                    UserProfile profile = new UserProfile { DisplayName = _username };
                    var ProfileTask = User.UpdateUserProfileAsync(profile);
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        warningRegisterText.text = "Username  Set Failed!";
                    }
                    else
                    {
                        //Username set
                        Debug.Log("User created!");
                        warningRegisterText.text = "";
                        confirmRegisterText.text = "User created! You can now login :)";
                        yield return new WaitForSeconds(3);
                        confirmRegisterText.text = "";

                        StartCoroutine(UpdateUsernameAuth(usernameRegisterField.text));
                        StartCoroutine(UpdateUsernameDatabase(usernameRegisterField.text, emailRegisteredField.text));

                    }
                }
            }
        }
    }

    public void OnLoginButtonClicked()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Disconnect();
        }
        ActivatePanel(ConnectingUIPanel.name);
        connectionStatusText.gameObject.SetActive(true);
        PhotonNetwork.ConnectUsingSettings();
        WelcomeMessage.gameObject.SetActive(true);
        InputName.SetActive(false);
        WelcomeMessage.text = "Welcome back " + User.DisplayName + "!";
        currentWinsText.gameObject.SetActive(true);
    }

    public void OnPlayAsGuestButtonClicked()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Disconnect();
        }
        ActivatePanel(ConnectingUIPanel.name);
        connectionStatusText.gameObject.SetActive(true);
        PhotonNetwork.ConnectUsingSettings();
        WelcomeMessage.gameObject.SetActive(false);
        playerNameInput.gameObject.SetActive(true);
        currentWinsText.gameObject.SetActive(false);
    }

    public void OnBackButtonClicked()
    {
        auth.SignOut();
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Disconnect();
        }

        ActivatePanel(MainMenuUIPanel.name);

        loggedin = false;
        Debug.Log("LOGGED OUT!");
        clearLoginFields();
        clearRegisterFields();
    }

    public void OnLeaderboardButtonClicked()
    {
        ActivatePanel(LeaderboardUIPanel.name);
    }

    public void ActivatePanel(string panelTobeActivated)
    {
        ConnectingUIPanel.SetActive(ConnectingUIPanel.name.Equals(panelTobeActivated));
        MainMenuUIPanel.SetActive(MainMenuUIPanel.name.Equals(panelTobeActivated));
        LeaderboardUIPanel.SetActive(LeaderboardUIPanel.name.Equals(panelTobeActivated));
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

        string playerName;

        if (loggedin == true)
        {
            playerName = User.DisplayName;
        }
        else
        {
            playerName = playerNameInput.text;

            if (playerName == "")
            {
                playerName = "Player" + Random.Range(100, 1000);
            }
        }

        PhotonNetwork.LocalPlayer.NickName = playerName;

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        roomOptions.IsVisible = isPublic;

        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }

    public void OnJoinRoomButtonClicked(bool isRandom)
    {

        string playerName;
        if (loggedin == true)
        {
            playerName = User.DisplayName;
        }
        else
        {
            playerName = playerNameInput.text;
            if (playerName == "")
            {
                playerName = "Player" + Random.Range(100, 1000);
            }
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
            ActivatePanel(ConnectingUIPanel.name);
            PhotonNetwork.JoinRandomRoom();
        }
    }

    private IEnumerator UpdateUsernameAuth(string _username)
    {
        UserProfile profile = new UserProfile { DisplayName = _username };

        var ProfileTask = User.UpdateUserProfileAsync(profile);

        yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

        if (ProfileTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
        }
        else
        {
            //update succesful
        }
    }

    private IEnumerator UpdateUsernameDatabase(string usernameToAdd, string emailToAdd)
    {
        var DBTask2 = DBreference.Child("users").Child(User.UserId).Child("username").SetValueAsync(usernameToAdd);
        var DBTask3 = DBreference.Child("users").Child(User.UserId).Child("wins").SetValueAsync(0);
        var DBTask4 = DBreference.Child("users").Child(User.UserId).Child("email").SetValueAsync(emailToAdd);

        yield return new WaitUntil(predicate: () => DBTask2.IsCompleted);
        yield return new WaitUntil(predicate: () => DBTask3.IsCompleted);
        yield return new WaitUntil(predicate: () => DBTask4.IsCompleted);

        if (DBTask2.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask2.Exception}");
        }
        else
        {
            //update succesful
        }
        if (DBTask3.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask3.Exception}");
        }
        else
        {
            //update succesful
        }
        if (DBTask4.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask4.Exception}");
        }
        else
        {
            //update succesful
        }

    }

    private IEnumerator LoadScoreboardData()
    {
        //Get all the users data ordered by kills amount
        var DBTask = DBreference.Child("users").OrderByChild("wins").GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;

            //Destroy any existing scoreboard elements
            foreach (Transform child in scoreboardContent.transform)
            {
                Destroy(child.gameObject);
            }

            //Loop through every users UID
            int rank = 1;
            foreach (DataSnapshot childSnapshot in snapshot.Children.Reverse<DataSnapshot>())
            {

                string username = childSnapshot.Child("username").Value.ToString();
                int wins = int.Parse(childSnapshot.Child("wins").Value.ToString());

                //Instantiate new scoreboard elements
                GameObject scoreboardElement = Instantiate(scoreElement, scoreboardContent);
                scoreboardElement.GetComponent<ScoreElement>().NewScoreElement(rank.ToString(), username, wins);
                rank++;
            }


        }
    }

    public void ScoreboardButton()
    {
        StartCoroutine(LoadScoreboardData());
    }

    // public bool checkExistingUsername(string _username)
    // {
    //   var DBTask = DBreference.Child("users").Child("username").GetValueAsync();

    //   DataSnapshot snapshot = DBTask.Result;

    //   foreach (DataSnapshot ds in snapshot.Children)
    //   {
    //     if (ds.Child("username").Value.Equals(_username))
    //     {
    //       return true;
    //     }
    //   }
    //   return false;
    // }

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

    private IEnumerator incrementWins()
    {
        if (loggedin == true)
        {
            var DBTask = DBreference.Child("users").Child(User.UserId).GetValueAsync();

            yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

            if (DBTask.Result != null)
            {
                DataSnapshot snapshot = DBTask.Result;

                int currentWins = int.Parse(snapshot.Child("wins").Value.ToString());

                var DBTask2 = DBreference.Child("users").Child(User.UserId).Child("wins").SetValueAsync(currentWins + 1);

                yield return new WaitUntil(predicate: () => DBTask2.IsCompleted);
                Debug.Log("Win added! Check leaderboard.");
                if (DBTask.Exception != null)
                {
                    Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
                }
                else
                {

                }
            }
        }
        else
        {
            Debug.Log("Not logged in! Leaderboard wins only available for logged in users.");
        }
        StartCoroutine(LoadUserWins());
    }

    public void IncrementWinsButton()
    {
        StartCoroutine(incrementWins());


    }

}





