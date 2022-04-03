using Photon.Pun;
using UnityEngine;
public class CannonSpawner : MonoBehaviour
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

        if (!PhotonNetwork.IsMasterClient)
        {
            P1Cannon = PhotonNetwork.Instantiate(Cannon.name, new Vector3(133, 1, 250), Quaternion.identity, 0);
        }

        if (PhotonNetwork.IsMasterClient)
        {
            P2Cannon = PhotonNetwork.Instantiate(Cannon.name, new Vector3(168, 1, 60), Quaternion.identity, 0);
        }
    }
}
