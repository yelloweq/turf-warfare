using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class AuthManager : MonoBehaviour
{

  // public GameObject JoinOrCreateRoom_LoggedIn;
  public GameObject ConnectingUIPanel;
  // public Text playerName;
  public Text connectionStatusText;
  public GameObject MainMenuUIPanel;
  public Text WelcomeMessage;
  public GameObject InputName;

  //Firebase variables
  [Header("Firebase")]
  public DependencyStatus dependencyStatus;
  public FirebaseAuth auth;
  public FirebaseUser User;

  //Login variables
  [Header("Login")]
  public InputField emailLoginField;
  public InputField paswordLoginField;
  public TMP_Text warningLoginText;
  public TMP_Text confirmLoginText;

  //Register variables
  [Header("Register")]
  public InputField usernameRegisterField;
  public InputField emailRegisteredField;
  public InputField passwordRegisteredField;
  public InputField passwordRegisterVerifyField;
  public TMP_Text warningRegisterText;
  public TMP_Text confirmRegisterText;

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
  }

  //LoginButton function
  public void LoginButton()
  {
    StartCoroutine(Login(emailLoginField.text, paswordLoginField.text));
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

      warningLoginText.text = "";
      confirmLoginText.text = "Logged in!";

      OnLoginButtonClicked();
    }
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
            yield return new WaitForSeconds(5);
            confirmRegisterText.text = "";

          }
        }
      }
    }
  }

  public void OnLoginButtonClicked()
  {
    ActivatePanel(ConnectingUIPanel.name);
    connectionStatusText.gameObject.SetActive(true);
    PhotonNetwork.ConnectUsingSettings();
    WelcomeMessage.gameObject.SetActive(true);
    InputName.SetActive(false);
    WelcomeMessage.text = "Welcome back" + User.DisplayName + "!";
  }

  public void OnBackButtonClicked()
  {
    if (PhotonNetwork.IsConnected)
    {
      PhotonNetwork.Disconnect();
    }
    ActivatePanel(MainMenuUIPanel.name);
    auth.SignOut();

  }

  public void ActivatePanel(string panelTobeActivated)
  {
    ConnectingUIPanel.SetActive(ConnectingUIPanel.name.Equals(panelTobeActivated));
    // JoinOrCreateRoom_LoggedIn.SetActive(JoinOrCreateRoom_LoggedIn.name.Equals(panelTobeActivated));
    MainMenuUIPanel.SetActive(MainMenuUIPanel.name.Equals(panelTobeActivated));
  }


}

