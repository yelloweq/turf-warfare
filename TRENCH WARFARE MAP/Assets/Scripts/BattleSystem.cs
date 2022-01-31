using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class BattleSystem : MonoBehaviour
{
    public GameObject Cannon;

    private GameObject P1Cannon;
    private GameObject P2Cannon;

    private PhotonView P1PV;
    private PhotonView P2PV;


    void Start()
    {
        //Spawns in cannons when script is first executed
        SetupBattle();

    }

    void SetupBattle()
    {
        //Spawns cannon 1 at given transform position
        if (!PhotonNetwork.IsMasterClient)
        {
           P1Cannon = PhotonNetwork.Instantiate(Cannon.name, new Vector3(133, 1, 250), Quaternion.identity, 0);
        }
        

        //Spawns cannon 2 at given transform position
        if (PhotonNetwork.IsMasterClient)
        {
            P2Cannon = PhotonNetwork.Instantiate(Cannon.name, new Vector3(168, 1, 60), Quaternion.identity, 0);
        }
        

        P1PV = P1Cannon.GetComponent<PhotonView>();
        P2PV = P2Cannon.GetComponent<PhotonView>();


        //TODO: ACTIVATE CANNON AT SPAWN
        //P1Cannon.GetComponent<CannonController>().ActivateCannon();    cant call rpc without photonview on battlesystem
        //this function does not seem to work, as log says false for both cannons after initiated.
    }


}
