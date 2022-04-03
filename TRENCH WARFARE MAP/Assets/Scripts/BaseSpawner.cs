using Photon.Pun;
using UnityEngine;
public class BaseSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject Base;

    private GameObject P1Base;
    private GameObject P2Base;

    [SerializeField]
    private GameObject P1BaseLocation;
    [SerializeField]
    private GameObject P2BaseLocation;

    void Start()
    {
        P1BaseLocation = GameObject.Find("BaseSpawnLocation");
        P2BaseLocation = GameObject.Find("BaseSpawnLocation2");

        //Spawns in cannons when script is first executed
        SpawnBases();
    }

    void SpawnBases()
    {

        if (PhotonNetwork.IsMasterClient)
        {
            P1Base = PhotonNetwork.Instantiate(Base.name, P1BaseLocation.transform.position, Quaternion.identity, 0);
            P1Base.transform.Rotate(0f, 180f, 0f);


        }
        else
        {
            P2Base = PhotonNetwork.Instantiate(Base.name, P2BaseLocation.transform.position, Quaternion.identity, 0);

        }
    }


}
