using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BattleSystem : MonoBehaviour
{
  public GameObject Player1Cannon;
  public GameObject Player2Cannon;
//  public GameObject Player2Base;
  public bool currentPlayer;

  public Component p1Controller;
  public Component p2Controller;
  public DrawProjection p1Projection;
  public DrawProjection p2Projection;

  public CannonController CannonControllerScript;



    void Start()
    {

        SetupBattle();
        currentPlayer = true;
    }

    void SetupBattle() {
  //  GameObject spawnP1Base = Instantiate(Player1Base, new Vector3(29, 2, 40), Quaternion.identity);
    GameObject spawnP1Cannon = Instantiate(Player1Cannon, new Vector3(29, 0, 147), Quaternion.identity);
    p1Controller = spawnP1Cannon.GetComponent<CannonController>();
    p1Projection = spawnP1Cannon.GetComponent<DrawProjection>();
  //  p1Renderer = spawnP1Cannon.Fet

  //  GameObject spawnP2Base = Instantiate(Player2Base, new Vector3(30, 2, 166), Quaternion.identity);
    GameObject spawnP2Cannon = Instantiate(Player2Cannon, new Vector3(32, 0, 64), Quaternion.identity);
    p2Controller = spawnP2Cannon.GetComponent<CannonController>();
    p2Projection = spawnP2Cannon.GetComponent<DrawProjection>();


    PlayerTurn();

}
    void PlayerTurn() {
        if (currentPlayer) {
        p1Controller.GetComponent<CannonController>().enabled = false;
        p1Projection.GetComponent<DrawProjection>().enabled = false;
        p2Controller.GetComponent<LineRenderer>().enabled = true;
        p2Controller.GetComponent<CannonController>().enabled = true;
        p1Projection.GetComponent<LineRenderer>().enabled = false;
        p2Projection.GetComponent<DrawProjection>().enabled = true;
        } else {
        p2Controller.GetComponent<CannonController>().enabled = false;
        p2Projection.GetComponent<DrawProjection>().enabled = false;
        p1Controller.GetComponent<CannonController>().enabled = true;
        p1Projection.GetComponent<DrawProjection>().enabled = true;
        p2Controller.GetComponent<LineRenderer>().enabled = false;
        p1Projection.GetComponent<LineRenderer>().enabled = true;
        }
      }


    public void PlayerSwitch() {
        currentPlayer = !currentPlayer;
        PlayerTurn();
        Debug.Log("here");
      }
    }
