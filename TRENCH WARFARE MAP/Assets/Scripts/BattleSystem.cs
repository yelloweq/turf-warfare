using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BattleSystem : MonoBehaviour
{
  public GameObject Player1Cannon;
  public GameObject Player2Cannon;
  public bool currentPlayer;

  public Component p1Controller;
  public Component p2Controller;
  public DrawProjection p1Projection;
  public DrawProjection p2Projection;

  public CannonController CannonControllerScript;



    void Start()
    {
        //Spawns in cannons when script is first executed
        SetupBattle();
        currentPlayer = true;
    }

    void SetupBattle() {
        //Spawns cannon 1 at given transform position
        GameObject spawnP1Cannon = Instantiate(Player1Cannon, new Vector3(29, 1, 147), Quaternion.identity);
        p1Controller = spawnP1Cannon.GetComponent<CannonController>();
        p1Projection = spawnP1Cannon.GetComponent<DrawProjection>();

        //Spawns cannon 2 at given transform position
        GameObject spawnP2Cannon = Instantiate(Player2Cannon, new Vector3(32, 1, 64), Quaternion.identity);
        p2Controller = spawnP2Cannon.GetComponent<CannonController>();
        p2Projection = spawnP2Cannon.GetComponent<DrawProjection>();


        PlayerTurn();

    }
    void PlayerTurn() {
        //if the current player is active
        if (currentPlayer) {
            //Disable the scripts for cannon 1 and activate scripts for cannon 2
            p1Controller.GetComponent<CannonController>().enabled = false;
            p1Projection.GetComponent<DrawProjection>().enabled = false;
            p2Controller.GetComponent<LineRenderer>().enabled = true;
            p2Controller.GetComponent<CannonController>().enabled = true;
            p1Projection.GetComponent<LineRenderer>().enabled = false;
            p2Projection.GetComponent<DrawProjection>().enabled = true;
        }

        //if the current player is not active
        else
        {
            //Disable the scripts for cannon 2 and activate scripts for cannon 1
            p2Controller.GetComponent<CannonController>().enabled = false;
            p2Projection.GetComponent<DrawProjection>().enabled = false;
            p1Controller.GetComponent<CannonController>().enabled = true;
            p1Projection.GetComponent<DrawProjection>().enabled = true;
            p2Controller.GetComponent<LineRenderer>().enabled = false;
            p1Projection.GetComponent<LineRenderer>().enabled = true;
        }
      }


    public void PlayerSwitch() {
        //inverts the currentPlayer boolean
        currentPlayer = !currentPlayer;
        //runs PlayerTurn to check the new current player
        PlayerTurn();
      }
    }
