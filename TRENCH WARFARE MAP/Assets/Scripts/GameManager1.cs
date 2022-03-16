using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager1 : MonoBehaviour
{
    public GameObject p1Cannon;
    public GameObject p2Cannon;
    public bool p1Turn;

    public GameObject p1Base;
    public GameObject p2Base;
    double p1Health;
    double p2Health;

    Component p1Controller;
    Component p2Controller;
    DrawProjection p1Projection;
    DrawProjection p2Projection;
    public GameObject p1;
    public GameObject p2;
    public GameObject p1Camera;
    public GameObject p2Camera;
    public Text PersonPlaying;
    public Text P2Money;

    void Start()
    {
        //Gets each player's original/first cannons' details when script is first executed
        p1Controller = p1Cannon.GetComponent<CannonController>();
        p1Projection = p1Cannon.GetComponent<DrawProjection>();
        
        p2Controller = p2Cannon.GetComponent<CannonController>();
        p2Projection = p2Cannon.GetComponent<DrawProjection>();

        //Gets each player's health
        p1Health = p1Base.GetComponent<BaseHealth>().health;
        p2Health = p2Base.GetComponent<BaseHealth>().health;

        p1Turn = true;
        SetupBattle();

        P2Money.gameObject.SetActive(true);//so it only shows when in the offline scene
        
    }

    void Update()
    { 
        CheckIfGameOver(); //or put it into base script, which comes here for the end game method
    }

    void SetupBattle()
    {                
        PlayerTurn();
    }

    void PlayerTurn()
    {
        //if the current player is active
        if (p1Turn)
        {
            PersonPlaying.text = "Player1:"; //Changes text at the top of screen
            EnableP1Cannon();
            EnableP1Movement();
        }

        //if the current player is not active
        else
        {
            PersonPlaying.text = "Player2:";//Changes text at the top of screen
            EnableP2Cannon();
            EnableP2Movement();
        }
    }

    void EnableP1Cannon()
    {
        //Disable the scripts for cannon 2 and activate scripts for cannon 1
        p2Controller.GetComponent<CannonController>().enabled = false;
        p2Projection.GetComponent<DrawProjection>().enabled = false;
        p2Controller.GetComponent<LineRenderer>().enabled = false;

        p1Controller.GetComponent<CannonController>().enabled = true;
        p1Projection.GetComponent<DrawProjection>().enabled = true;
        p1Projection.GetComponent<LineRenderer>().enabled = true;
    }

    void EnableP2Cannon()
    {
        //Disable the scripts for cannon 1 and activate scripts for cannon 2
        p1Controller.GetComponent<CannonController>().enabled = false;
        p1Projection.GetComponent<DrawProjection>().enabled = false;
        p1Projection.GetComponent<LineRenderer>().enabled = false;

        p2Controller.GetComponent<CannonController>().enabled = true;
        p2Projection.GetComponent<DrawProjection>().enabled = true;
        p2Controller.GetComponent<LineRenderer>().enabled = true;
    }

    public void PlayerSwitch()
    {
        //inverts the p1Turn boolean
        p1Turn = !p1Turn;
        //runs PlayerTurn to check the new current player
        PlayerTurn();
    }

    void EnableP1Movement()//Switches to player 1
    {
        p2.GetComponent<CharacterMovement>().enabled = false;//freezes player 2
        p2Camera.GetComponent<Camera>().enabled = false;
        p1.GetComponent<CharacterMovement>().enabled = true;//enables player 1
        p1Camera.GetComponent<Camera>().enabled = true;
    }
    void EnableP2Movement()//Switches to player 2
    {
        p1.GetComponent<CharacterMovement>().enabled = false;//freezes player 1
        p1Camera.GetComponent<Camera>().enabled = false;
        p2.GetComponent<CharacterMovement>().enabled = true;//enables player 2
        p2Camera.GetComponent<Camera>().enabled = true;
    }

    void CheckIfGameOver()
    {
        if(p1Health <= 0 || p2Health <= 0){
            EndOfGame();
        }
    }
    void EndOfGame(){}//Opens ui that says which player won and the options to restart game or back to main menu.
    void Restart(){}//when restart button is clicked
    void Home(){}//when mainmenu button is clicked
}
